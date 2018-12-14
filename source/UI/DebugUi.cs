using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionControl
{
    /// <summary>
    /// 调试窗口类，使用时需要实例化
    /// </summary>
    public partial class DebugUi : Form
    {
        private int axisId_ = 0;
        private int dioId_ = 0;
        private Bitmap _bitGreen;//绿色状态单色图
        private Bitmap _bitGray;//无状态单色图
        private Bitmap _bitRed;//红色状态单色图
        private Bitmap _bitMagent; //
        private Bitmap _GenColorImage(Color color)
        {
            Bitmap bitmap_color = new Bitmap(dataGridView_Axis.Columns[5].Width / 2, dataGridView_Axis.RowTemplate.Height / 2);
            Graphics g = Graphics.FromImage(bitmap_color);
            SolidBrush brush_Color = new SolidBrush(color);
            g.FillRectangle(brush_Color, 0, 0, bitmap_color.Width, bitmap_color.Height);
            return bitmap_color;
        }

        private void _showDebugInfo(int code, string info)
        {
            Color color_ = (code == 0) ? Color.DarkBlue : Color.Red;
            this.toolStripStatusLabel_Time.ForeColor = color_;
            this.toolStripStatusLabel_Code.ForeColor = color_;
            this.toolStripStatusLabel_Info.ForeColor = color_;

            this.toolStripStatusLabel_Time.Text = System.DateTime.Now.ToLongTimeString();
            this.toolStripStatusLabel_Code.Text = code.ToString();
            if (tabControl1.SelectedIndex == 0)
            {
                this.toolStripStatusLabel_Info.Text = string.Format("Axis{0}: {1}, {2}", axisId_, Motion.Axis(axisId_).name, info);
            }
            else
            {
                this.toolStripStatusLabel_Info.Text = string.Format("DIO{0}: {1}, {2}", dioId_, Motion.Dio(dioId_).name, info);
            }
        }

        private void _showDebugInfo(int code, object sender)
        {
            _showDebugInfo(code, string.Format("【{0}】 clicked!", ((Control)sender).Text));
        }
        /// <summary>
        /// 调试窗口，使用时需要实例化
        /// </summary>
        public DebugUi()
        {
            InitializeComponent();
            this.toolStripStatusLabel_Time.Text = System.DateTime.Now.ToLongTimeString();
            int i, j = 0;
            //轴
            this.dataGridView_Axis.Rows.Clear();
            this.dataGridView_Axis.Rows.Add(Motion.AxisNum);

            _bitGreen = _GenColorImage(Color.Green);
            _bitGray = _GenColorImage(Color.LightGray);
            _bitRed = _GenColorImage(Color.Red);
            _bitMagent = _GenColorImage(Color.DarkMagenta);           
            for (i = 0; i < Motion.AxisNum; i++)
            {
                j = 0;                
               // this.dataGridView_Axis.Rows[i].HeaderCell.Value = i.ToString();
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = i.ToString();
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = Motion.Axis(i).name;
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = "0.000";
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = "0.000";

                this.dataGridView_Axis.Rows[i].Cells[10].Value = "配置";
            }

            //IO
            if (Motion.DioNum <= 0)
            {
                this.tabPage2.Parent = null;
                return;
            }            
            this.dataGridView_IO.Rows.Clear();
            this.dataGridView_IO.Rows.Add(Motion.DioNum);            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int j = 0;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    for (int i = 0; i < Motion.AxisNum; i++)
                    {
                        j = 2;
                        MotionIO mio_ = Motion.Axis(i).MIO;
                        this.dataGridView_Axis.Rows[i].Cells[j++].Value = Motion.Axis(i).CmdPos.ToString("F3");
                        this.dataGridView_Axis.Rows[i].Cells[j++].Value = Motion.Axis(i).FbkPos.ToString("F3");
                        this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.NEL ? _bitMagent : _bitGray;
                        this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.PEL ? _bitMagent : _bitGray;
                        this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.Moving ? _bitGray : _bitGreen;
                        this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.Alarm ? _bitRed : _bitGray;
                        this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.Enabled ? _bitGreen : _bitGray;
                        this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.Enabled;
                    }
                    break;
                case 1:
                    for (int i = 0; i < Motion.DioNum; i++)
                    {
                        j = 5;
                        this.dataGridView_IO.Rows[i].Cells[j++].Value = Motion.Dio(i).Bit? _bitGray : _bitGreen;
                        this.dataGridView_IO.Rows[i].Cells[j++].ReadOnly = !Motion.Dio(i).ouput;
                    }
                    break;
                default:
                    break;
            }
        }

        private void dataGridView_Axis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= Motion.AxisNum)
                {
                    return;
                }                
                switch (e.ColumnIndex)
                {
                    case 9:
                        bool cmd_ = Convert.ToBoolean(this.dataGridView_Axis.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue);
                        int err = Motion.Axis(e.RowIndex).SetEnabled(cmd_);
                        _showDebugInfo(err, (cmd_?"Servo ON!":"Servo OFF!"));
                        break;
                    case 10:
                        AxisConfig a = new AxisConfig(e.RowIndex);
                        a.ShowDialog(this);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            int err = Motion.Axis(axisId_).ClearAlarm();
            _showDebugInfo(err, sender);
        }

        private void button_Home_Click(object sender, EventArgs e)
        {
            int err = Motion.Axis(axisId_).Home();
            _showDebugInfo(err, sender);
        }

        private void button_AMove_Click(object sender, EventArgs e)
        {
            double ratio_ = Convert.ToDouble(this.numericUpDown_ratio.Value);
            double pos_ = Convert.ToDouble(this.numericUpDown_Pos.Value);
            int err = Motion.Axis(axisId_).AbsMove(pos_, ratio_);
            _showDebugInfo(err, sender);
        }

        private void dataGridView_Axis_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_Axis.CurrentRow.Index < 0 || dataGridView_Axis.CurrentRow.Index >= Motion.AxisNum)
            {
                splitContainer1.Panel2.Enabled = false;
                return;
            }
            splitContainer1.Panel2.Enabled = true;
            axisId_ = dataGridView_Axis.CurrentRow.Index;
            label_Name.Text = Motion.Axis(axisId_).name;
        }

        private void button_RMoveP_Click(object sender, EventArgs e)
        {
            double ratio_ = Convert.ToDouble(this.numericUpDown_ratio.Value);
            double dist_ = Convert.ToDouble(this.numericUpDown_Dis.Value);
            int err = Motion.Axis(axisId_).RelMove(dist_, ratio_);
            _showDebugInfo(err, sender);
        }

        private void button_RMoveN_Click(object sender, EventArgs e)
        {
            double ratio_ = Convert.ToDouble(this.numericUpDown_ratio.Value);
            double dist_ = Convert.ToDouble(this.numericUpDown_Dis.Value);
            int err = Motion.Axis(axisId_).RelMove(-dist_, ratio_);
            _showDebugInfo(err, sender);
        }

        private void button_Trigger0_Click(object sender, EventArgs e)
        {
            int err = Motion.Axis(axisId_).SoftwareTrigger(0);
            _showDebugInfo(err, sender);
        }

        private void button_Trigger1_Click(object sender, EventArgs e)
        {
            int err = Motion.Axis(axisId_).SoftwareTrigger(1);
            _showDebugInfo(err, sender);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                int j = 0;
                for (int i = 0; i < Motion.DioNum; i++)
                {
                    j = 1;
                    Motion.Dio(i).name = this.dataGridView_IO.Rows[i].Cells[j++].Value.ToString();
                    Motion.Dio(i).cardId = (short)Convert.ToInt32(this.dataGridView_IO.Rows[i].Cells[j++].Value);
                    Motion.Dio(i).bitNo = (short)Convert.ToInt32(this.dataGridView_IO.Rows[i].Cells[j++].Value);
                    Motion.Dio(i).ouput = Convert.ToBoolean(this.dataGridView_IO.Rows[i].Cells[j++].Value);
                }
                Motion.SaveDioPara();
                _showDebugInfo(0, "全部IO信息保存成功！");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_IO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= Motion.DioNum)
                {
                    return;
                }
                switch (e.ColumnIndex)
                {
                    case 6:
                        if (Motion.Dio(e.RowIndex).ouput)
                        {
                            bool cmd_ = Convert.ToBoolean(this.dataGridView_IO.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue);

                            Motion.Dio(e.RowIndex).Bit = cmd_;
                            _showDebugInfo(0, (cmd_ ? "Set ouput ON!" : "Set ouput OFF!"));
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_IO_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView_IO.CurrentRow.Index < 0 || dataGridView_IO.CurrentRow.Index >= Motion.DioNum)
            {                
                return;
            }
            dioId_ = dataGridView_IO.CurrentRow.Index;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                int j = 0;
                for (int i = 0; i < Motion.DioNum; i++)
                {
                    j = 0;
                    this.dataGridView_IO.Rows[i].Cells[j++].Value = i.ToString();
                    this.dataGridView_IO.Rows[i].Cells[j++].Value = Motion.Dio(i).name;
                    this.dataGridView_IO.Rows[i].Cells[j++].Value = Motion.Dio(i).cardId.ToString();
                    this.dataGridView_IO.Rows[i].Cells[j++].Value = Motion.Dio(i).bitNo.ToString();
                    this.dataGridView_IO.Rows[i].Cells[j++].Value = Motion.Dio(i).ouput;
                }
            }
        }
    }
}
