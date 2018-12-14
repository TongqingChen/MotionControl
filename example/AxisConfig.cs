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
    public partial class AxisConfig : Form
    {
        private Axis axis = new Axis();
        public AxisConfig(int i)
        {
            InitializeComponent();
            axis = Motion.Axis(i);
            this.Text = string.Format("轴{0}: {1}", axis.id, axis.name);
            this.textBox_Id.Text = axis.id.ToString();
            this.textBox_Name.Text = axis.name;
            this.textBox_Pulse.Text = axis.plsPerMm.ToString();

            this.comboBox_Home.SelectedIndex = (int)axis.homePara.mode;
            this.textBox_HomeAcc.Text = axis.homePara.acc.ToString();
            this.textBox_HomeVel.Text = axis.homePara.vel.ToString();
            this.textBox_Offset.Text = axis.homePara.offset.ToString();
            this.textBox_HomeTimeout.Text = axis.homePara.maxSeconds.ToString();

            this.textBox_Acc.Text = axis.motionPara.acc.ToString();
            this.textBox_Vel.Text = axis.motionPara.maxVel.ToString();
            this.textBox_Timeout.Text = axis.motionPara.maxSeconds.ToString();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                axis.id = (short)Convert.ToInt32(this.textBox_Id.Text);
                axis.name = this.textBox_Name.Text;
                axis.plsPerMm = Convert.ToDouble(this.textBox_Pulse.Text);
                axis.homePara.mode = (HomeMode)(this.comboBox_Home.SelectedIndex);

                axis.homePara.acc = Convert.ToDouble(this.textBox_HomeAcc.Text);
                axis.homePara.vel = Convert.ToDouble(this.textBox_HomeVel.Text);
                axis.homePara.offset = Convert.ToDouble(this.textBox_Offset.Text);
                axis.homePara.maxSeconds = Convert.ToDouble(this.textBox_HomeTimeout.Text);

                axis.motionPara.acc = Convert.ToDouble(this.textBox_Acc.Text);
                axis.motionPara.maxVel = Convert.ToDouble(this.textBox_Vel.Text);
                axis.motionPara.maxSeconds = Convert.ToDouble(this.textBox_Timeout.Text);
                Motion.SaveAxesPara();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
