using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionControl;

namespace MotionExample
{
    public partial class MainUi : Form
    {
        const int X = 0;
        const int Y = 1;
        const string folder_ = @"d:\motion";
        const bool online_ = true;


        public MainUi()
        {
            InitializeComponent();
        }

        private void MainUi_Load(object sender, EventArgs e)
        {
            //初始化，包含加载运动参数、建立板卡通信、配置板卡等（实际应用中不应该放在窗体Load事件中，而是放在程序初始化部分）
            int err_ = 0;
            if ((err_ = Motion.InitSystem(folder_, online_)) != 0)
            {
                MessageBox.Show("Motion 初始化失败! code: " + err_.ToString());
                return;
            }
        }

        private void MainUi_FormClosed(object sender, FormClosedEventArgs e)
        {
            //释放资源，关闭板卡通讯（实际应该放在程序结束前，注意如果异常终止程序而未调用释放资源操作，可能导致板卡一直被占用到情况，需要重启计算机解决）
            Motion.DiscardSystem();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //弹出调试界面
            var frm = new DebugUi();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //运动测试
            const int posNum_ = 5;
            double[] posX = new double[posNum_] { 0, 100, 100, 0, 0 };
            double[] posY = new double[posNum_] { 0, 0, 100, 100, 0 };
            int times_ = Convert.ToInt32(numericUpDown_Times.Value);

            while (times_-- > 0)
            {
                for (int i = 0; i < posNum_; i++)
                {
                    Motion.Axis(X).AbsMove(posX[i]);
                    Motion.Axis(Y).AbsMove(posY[i]);

                    Motion.Axis(X).MotionDone();
                    Motion.Axis(Y).MotionDone();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {            
            Motion.Axis(X).ZeroPos();
            Motion.Axis(Y).ZeroPos();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //点表模式触发
            double start = 80;
            double interval = -10;
            int number = 10;
            int err = 0;

            var ch0 = new List<double>();
            var ch1 = new List<double>();

            for (int i = 0; i < number; i++)
            {
                ch0.Add(start + i * interval);
                ch1.Add(start + i * interval + 1);
            }
            err = Motion.Axis(X).TriggerData(ch0, ch1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //线性模式触发
            double start = -10;
            double interval = 10;
            int number = 10;
            int err = 0;
            err = Motion.Axis(X).TriggerLinear(0, start, interval, number);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Motion.Axis(X).TriggerStop();
        }

        private void button6_Click(object sender, EventArgs e)
        {            
            double v_ = 200; // mm/s
            double a_ = 2000;   // mm/s^2

            double trigInterval = 3.0;
            double xc = 125, yc = 160; //扫描圆心
            double r = 100; //扫描区域半径

            Axis AX = Motion.Axis(X);
            Axis AY = Motion.Axis(Y);
            Point2i axes = new Point2i(X, Y);
            double offset = -0.2; //两通道触发间隔


            Point2d pos2d_start = new Point2d();
            Point2d pos2d_end = new Point2d(); 

            int err_ = 0;
            
            int direction_ = 0;

            bool use_tirgger_data = false;
            
            for (int i=0; i<2*r/trigInterval - 1; i++)
            {
                //step0: 计算点位
                direction_ = (i % 2 == 0) ? 1 : -1;
                pos2d_start.y = yc -r + trigInterval * (i+1);
                pos2d_start.x = xc - direction_ * Math.Sqrt(r*r - (pos2d_start.y-yc) * (pos2d_start.y-yc));                
                pos2d_end.x = 2*xc - pos2d_start.x;
                pos2d_end.y = pos2d_start.y - 0.3*direction_; //Y适当错位，模拟斜着运动扫描（这里模拟走一个圆）
                Console.WriteLine("{0}: ({1}, {2}) -> ({3}, {4})",i, pos2d_start.x, pos2d_start.y, pos2d_end.x, pos2d_end.y);
                //step1: 运动到起点
                //XY移动到起点 AbsMoveOver = AbsMove + MotionDone
                //这里也可以用 Motion.Axis(X).AbsMove();
                if ((err_ = Motion.AbsMoveOver(axes, pos2d_start)) != 0)
                {
                    MessageBox.Show("Error: " + err_);
                    return;
                }
                //step2: 设置触发位置
                double triggerPos_ = pos2d_start.x + 0.5 * direction_; //第一个位置和当前位置相距一毫米
                //位置列表，绝对位置，单位mm
                if (use_tirgger_data)
                {
                    var trigPos0_ = new List<double>();
                    while ((pos2d_end.x - triggerPos_) * direction_ > 0)
                    {
                        trigPos0_.Add(triggerPos_);     //通道0触发光源
                        triggerPos_ += direction_ * trigInterval;
                    }
                    if ((err_ = AX.TriggerData(trigPos0_, direction_*offset)) != 0)
                    {
                        MessageBox.Show("Error: " + err_);
                        return;
                    }
                }
                else //等间隔触发，绝对位置，单位mm
                {
                    int triggerCnt_ = (int)((Math.Abs(pos2d_end.x - pos2d_start.x) - 1.0) / trigInterval); //保证最后一个点至少与起点相距0.5mm
                    if ((err_ = AX.TriggerLinear(triggerPos_, direction_*trigInterval, triggerCnt_, direction_*offset)) != 0) //触发相机和光源
                    {
                        MessageBox.Show("Error: " + err_);
                        return;
                    }
                }

                //step3: 插补运动            
                //LineOver = Line+LineDone
                if ((err_ = Motion.LineOver(axes, pos2d_end, v_, a_)) != 0)
                {
                    MessageBox.Show("Error: " + err_);
                    return;
                }

                //step4: 停止触发并process images           
                if ((err_ = AX.TriggerStop()) != 0)
                {
                    MessageBox.Show("Error: " + err_);
                    return;
                }              
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            button8.Text = timer1.Enabled ? "停止触发" : "连续触发";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Motion.Axis(X).SoftwareTrigger(1);
            System.Threading.Thread.Sleep(1);
            Motion.Axis(X).SoftwareTrigger(0);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //轴上电使能
            Motion.Axis(X).SetEnabled(true);
            Motion.Axis(Y).SetEnabled(true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Motion.Axis(X).Home();
            Motion.Axis(Y).Home();
        }
    }
}
