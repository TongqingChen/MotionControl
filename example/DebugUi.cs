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

namespace MotionUi
{
    public partial class DebugUi : Form
    {
        private int axisId_ = 0;
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
        public DebugUi()
        {
            InitializeComponent();
            int i, j = 0;
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int j = 0;
            for(int i=0; i<Motion.AxisNum;i++)
            {
                j = 2;
                MotionIO mio_ = Motion.Axis(i).MIO;
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = Motion.Axis(i).CmdPos.ToString();
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = Motion.Axis(i).FbkPos.ToString();
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.NEL ? _bitMagent : _bitGray;
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.PEL ? _bitMagent : _bitGray;
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.Moving ? _bitGray : _bitGreen;
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.Alarm ? _bitRed : _bitGray;
                this.dataGridView_Axis.Rows[i].Cells[j++].Value = mio_.Enabled ? _bitGreen : _bitGray;
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
                        Motion.Axis(e.RowIndex).SetEnabled(cmd_);
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
            Motion.Axis(axisId_).ClearAlarm();
        }

        private void button_Home_Click(object sender, EventArgs e)
        {
            Motion.Axis(axisId_).Home();
        }

        private void button_AMove_Click(object sender, EventArgs e)
        {
            double ratio_ = Convert.ToDouble(this.numericUpDown_ratio.Value);
            double pos_ = Convert.ToDouble(this.numericUpDown_Pos.Value);
            Motion.Axis(axisId_).AbsMove(pos_, ratio_);
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
            Motion.Axis(axisId_).RelMove(dist_, ratio_);
        }

        private void button_RMoveN_Click(object sender, EventArgs e)
        {
            double ratio_ = Convert.ToDouble(this.numericUpDown_ratio.Value);
            double dist_ = Convert.ToDouble(this.numericUpDown_Dis.Value);
            Motion.Axis(axisId_).RelMove(-dist_, ratio_);
        }
    }
}
