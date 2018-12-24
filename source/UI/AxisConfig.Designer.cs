namespace MotionControl
{
    partial class AxisConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_Id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_HomeAcc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_HomeVel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_HomeTimeout = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_CardNo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_Pulse = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_MaxSearch = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBox_Dir = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox_Acc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_Vel = new System.Windows.Forms.TextBox();
            this.textBox_Timeout = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Id
            // 
            this.textBox_Id.Location = new System.Drawing.Point(125, 72);
            this.textBox_Id.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Id.Name = "textBox_Id";
            this.textBox_Id.Size = new System.Drawing.Size(116, 23);
            this.textBox_Id.TabIndex = 0;
            this.textBox_Id.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "轴号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "轴名称";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(125, 19);
            this.textBox_Name.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(116, 23);
            this.textBox_Name.TabIndex = 2;
            this.textBox_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "回零加速度(mm/s²)";
            // 
            // textBox_HomeAcc
            // 
            this.textBox_HomeAcc.Location = new System.Drawing.Point(123, 76);
            this.textBox_HomeAcc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_HomeAcc.Name = "textBox_HomeAcc";
            this.textBox_HomeAcc.Size = new System.Drawing.Size(116, 23);
            this.textBox_HomeAcc.TabIndex = 6;
            this.textBox_HomeAcc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "回零速度(mm/s)";
            // 
            // textBox_HomeVel
            // 
            this.textBox_HomeVel.Location = new System.Drawing.Point(123, 104);
            this.textBox_HomeVel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_HomeVel.Name = "textBox_HomeVel";
            this.textBox_HomeVel.Size = new System.Drawing.Size(116, 23);
            this.textBox_HomeVel.TabIndex = 8;
            this.textBox_HomeVel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "回零超时(s)";
            // 
            // textBox_HomeTimeout
            // 
            this.textBox_HomeTimeout.Location = new System.Drawing.Point(123, 131);
            this.textBox_HomeTimeout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_HomeTimeout.Name = "textBox_HomeTimeout";
            this.textBox_HomeTimeout.Size = new System.Drawing.Size(116, 23);
            this.textBox_HomeTimeout.TabIndex = 10;
            this.textBox_HomeTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_CardNo);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.textBox_Pulse);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox_Name);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_Id);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(247, 134);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // textBox_CardNo
            // 
            this.textBox_CardNo.Location = new System.Drawing.Point(125, 46);
            this.textBox_CardNo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_CardNo.Name = "textBox_CardNo";
            this.textBox_CardNo.Size = new System.Drawing.Size(116, 23);
            this.textBox_CardNo.TabIndex = 6;
            this.textBox_CardNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 17);
            this.label12.TabIndex = 7;
            this.label12.Text = "卡号";
            // 
            // textBox_Pulse
            // 
            this.textBox_Pulse.Location = new System.Drawing.Point(125, 98);
            this.textBox_Pulse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Pulse.Name = "textBox_Pulse";
            this.textBox_Pulse.Size = new System.Drawing.Size(116, 23);
            this.textBox_Pulse.TabIndex = 4;
            this.textBox_Pulse.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 102);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 17);
            this.label10.TabIndex = 5;
            this.label10.Text = "脉冲当量(pls/mm)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_MaxSearch);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.comboBox_Dir);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox_HomeAcc);
            this.groupBox2.Controls.Add(this.textBox_HomeTimeout);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_HomeVel);
            this.groupBox2.Location = new System.Drawing.Point(267, 17);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(247, 162);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "回零设置";
            // 
            // textBox_MaxSearch
            // 
            this.textBox_MaxSearch.Location = new System.Drawing.Point(123, 48);
            this.textBox_MaxSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_MaxSearch.Name = "textBox_MaxSearch";
            this.textBox_MaxSearch.Size = new System.Drawing.Size(116, 23);
            this.textBox_MaxSearch.TabIndex = 17;
            this.textBox_MaxSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 17);
            this.label14.TabIndex = 18;
            this.label14.Text = "最大搜索距离(mm)";
            // 
            // comboBox_Dir
            // 
            this.comboBox_Dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Dir.FormattingEnabled = true;
            this.comboBox_Dir.Items.AddRange(new object[] {
            "正方向",
            "负方向"});
            this.comboBox_Dir.Location = new System.Drawing.Point(123, 15);
            this.comboBox_Dir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox_Dir.Name = "comboBox_Dir";
            this.comboBox_Dir.Size = new System.Drawing.Size(116, 25);
            this.comboBox_Dir.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 17);
            this.label13.TabIndex = 15;
            this.label13.Text = "回零方向";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox_Acc);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox_Vel);
            this.groupBox3.Controls.Add(this.textBox_Timeout);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(14, 162);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(247, 101);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "默认运动设置";
            // 
            // textBox_Acc
            // 
            this.textBox_Acc.Location = new System.Drawing.Point(125, 19);
            this.textBox_Acc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Acc.Name = "textBox_Acc";
            this.textBox_Acc.Size = new System.Drawing.Size(116, 23);
            this.textBox_Acc.TabIndex = 4;
            this.textBox_Acc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 17);
            this.label7.TabIndex = 5;
            this.label7.Text = "加速度(mm/s²)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "运动超时(s)";
            // 
            // textBox_Vel
            // 
            this.textBox_Vel.Location = new System.Drawing.Point(125, 45);
            this.textBox_Vel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Vel.Name = "textBox_Vel";
            this.textBox_Vel.Size = new System.Drawing.Size(116, 23);
            this.textBox_Vel.TabIndex = 6;
            this.textBox_Vel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_Timeout
            // 
            this.textBox_Timeout.Location = new System.Drawing.Point(125, 72);
            this.textBox_Timeout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Timeout.Name = "textBox_Timeout";
            this.textBox_Timeout.Size = new System.Drawing.Size(116, 23);
            this.textBox_Timeout.TabIndex = 10;
            this.textBox_Timeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "最大速度(mm/s)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(419, 230);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 33);
            this.button1.TabIndex = 15;
            this.button1.Text = "保存并退出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AxisConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 266);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AxisConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AxisConfig";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_HomeAcc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_HomeVel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_HomeTimeout;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_Pulse;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox_Acc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_Vel;
        private System.Windows.Forms.TextBox textBox_Timeout;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_CardNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBox_Dir;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_MaxSearch;
    }
}