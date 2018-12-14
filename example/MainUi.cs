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


        public MainUi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new DebugUi();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            Motion.Axis(X).SetEnabled(true);
            Motion.Axis(Y).SetEnabled(true);

            Motion.Axis(X).ZeroPos();
            Motion.Axis(Y).ZeroPos();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double start = 0;
            double interval = 10;
            int number = 10;
            int err = 0;

            //  err = Motion.Axis(X).AbsMove(start-1);
            //    err = Motion.Axis(X).MotionDone();
            List<double> ch1 = new List<double>();
            List<double> ch2 = new List<double>();

            for (int i = 0; i < number; i++)
            {
                ch1.Add(start + i * interval);
                ch2.Add(start + i * interval + 1);
            }


            err = Motion.Axis(X).TriggerData(0, ch1);

            //    err = Motion.Axis(X).TriggerData(1, ch2);
            //    Console.WriteLine("SetTrigger1, code: " + err);

            //   err = Motion.Axis(X).AbsMove(start+interval*(number-1) + 2);

            //   err = Motion.Axis(X).MotionDone();
            //  err = Motion.Axis(X).TriggerStop(0);
            //    err = Motion.Axis(X).TriggerStop(1);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Motion.Axis(X).TriggerStop(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            const double v_ = 0.2; // m/s
            const double a_ = 2;   // m/s^2
            const int channel_ = 0; //默认用0通道
            const double trigInterval = 6.0;
            const double r = 150; //扫面到半径

            Axis AX = Motion.Axis(X);
            Axis AY = Motion.Axis(Y);


            Point2d pos2d_start = new Point2d();
            Point2d pos2d_end = new Point2d();  
            List<double> trigPos_ = new List<double>();

            int err_ = 0;
            
            int direction_ = 0;
            
            for (int i=0; i<2*r/trigInterval - 1; i++)
            {
                //step0: 计算点位
                direction_ = (i % 2 == 0) ? 1 : -1;
                pos2d_start.y = -r + trigInterval * (i+1);
                pos2d_start.x = -direction_ * Math.Sqrt(r*r - pos2d_start.y * pos2d_start.y);                
                pos2d_end.x = -pos2d_start.x;
                pos2d_end.y = pos2d_start.y + 2*direction_; //Y适当错位，模拟斜着运动扫描（这里模拟走一个圆）
                Console.WriteLine("{0}: ({1}, {2}) -> ({3}, {4})",i, pos2d_start.x, pos2d_start.y, pos2d_end.x, pos2d_end.y);
                //step1: 运动到起点
                //XY移动到起点 AbsMoveOver = AbsMove + MotionDone
                //这里也可以用 Motion.Axis(X).AbsMove();
                if ((err_ = Motion.AbsMoveOver(X, Y, ref pos2d_start)) != 0)
                {
                    MessageBox.Show("Error: " + err_);
                    return;
                }
                //step2: 设置触发位置, 注意设置到时相对量
                double distance = direction_ * trigInterval / 2;
                trigPos_.Clear();
                while ((pos2d_start.x + distance - pos2d_end.x)*direction_<0)
                {
                    trigPos_.Add(distance);
                    distance += direction_*trigInterval;
                }  
                if ((err_ = AX.TriggerData(channel_, trigPos_)) != 0)
                {
                    MessageBox.Show("Error: " + err_);
                    return;
                }          
                
                //或者用线性触发的方式
                
                //step3: 插补运动            
                //LineOver = Line+LineDone
                if ((err_ = Motion.LineOver(X, Y, ref pos2d_end, v_, a_)) != 0)
                {
                    MessageBox.Show("Error: " + err_);
                    return;
                }

                //step4: 停止触发并process images           
                if ((err_ = AX.TriggerStop(channel_)) != 0)
                {
                    MessageBox.Show("Error: " + err_);
                    return;
                }              
            }
        }
    }
}
