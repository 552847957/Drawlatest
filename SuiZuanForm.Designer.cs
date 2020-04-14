namespace LJJSCAD
{
    partial class SuiZuanForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuiZuanForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbDrillRadius = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSetWhole = new System.Windows.Forms.Button();
            this.btnRemoveLine = new System.Windows.Forms.Button();
            this.秒 = new System.Windows.Forms.Label();
            this.tbInterval = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_LineRoadDesign = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.vdCommandLine1 = new VectorDraw.Professional.vdCommandLine.vdCommandLine();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbMoBan = new System.Windows.Forms.ComboBox();
            this.tbStepLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbEnd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbStart = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbJingHao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnSuiZuan = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPause);
            this.panel1.Controls.Add(this.btnSuiZuan);
            this.panel1.Controls.Add(this.tbDrillRadius);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.btnSetWhole);
            this.panel1.Controls.Add(this.btnRemoveLine);
            this.panel1.Controls.Add(this.秒);
            this.panel1.Controls.Add(this.tbInterval);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.lb_LineRoadDesign);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.vdCommandLine1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cmbMoBan);
            this.panel1.Controls.Add(this.tbStepLength);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbEnd);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbStart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbJingHao);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1620, 121);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tbDrillRadius
            // 
            this.tbDrillRadius.Location = new System.Drawing.Point(1356, 43);
            this.tbDrillRadius.Name = "tbDrillRadius";
            this.tbDrillRadius.Size = new System.Drawing.Size(100, 28);
            this.tbDrillRadius.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1353, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 18);
            this.label11.TabIndex = 23;
            this.label11.Text = "设置钻头半径";
            // 
            // btnSetWhole
            // 
            this.btnSetWhole.Location = new System.Drawing.Point(963, 75);
            this.btnSetWhole.Name = "btnSetWhole";
            this.btnSetWhole.Size = new System.Drawing.Size(145, 31);
            this.btnSetWhole.TabIndex = 22;
            this.btnSetWhole.Text = "设定归一化数值";
            this.btnSetWhole.UseVisualStyleBackColor = true;
            this.btnSetWhole.Click += new System.EventHandler(this.btnSetWhole_Click);
            // 
            // btnRemoveLine
            // 
            this.btnRemoveLine.Location = new System.Drawing.Point(820, 75);
            this.btnRemoveLine.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveLine.Name = "btnRemoveLine";
            this.btnRemoveLine.Size = new System.Drawing.Size(123, 31);
            this.btnRemoveLine.TabIndex = 21;
            this.btnRemoveLine.Text = "删除线道";
            this.btnRemoveLine.UseVisualStyleBackColor = true;
            this.btnRemoveLine.Click += new System.EventHandler(this.btnRemoveLine_Click);
            // 
            // 秒
            // 
            this.秒.AutoSize = true;
            this.秒.Location = new System.Drawing.Point(680, 81);
            this.秒.Name = "秒";
            this.秒.Size = new System.Drawing.Size(62, 18);
            this.秒.TabIndex = 9;
            this.秒.Text = "秒一次";
            // 
            // tbInterval
            // 
            this.tbInterval.Location = new System.Drawing.Point(592, 75);
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(82, 28);
            this.tbInterval.TabIndex = 5;
            this.tbInterval.Text = "3";
            this.tbInterval.TextChanged += new System.EventHandler(this.tbInterval_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(407, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "设定随钻几秒进行一次";
            // 
            // lb_LineRoadDesign
            // 
            this.lb_LineRoadDesign.FormattingEnabled = true;
            this.lb_LineRoadDesign.HorizontalScrollbar = true;
            this.lb_LineRoadDesign.ItemHeight = 18;
            this.lb_LineRoadDesign.Location = new System.Drawing.Point(1114, 22);
            this.lb_LineRoadDesign.Name = "lb_LineRoadDesign";
            this.lb_LineRoadDesign.Size = new System.Drawing.Size(233, 76);
            this.lb_LineRoadDesign.TabIndex = 19;
            this.lb_LineRoadDesign.SelectedIndexChanged += new System.EventHandler(this.lb_LineRoadDesign_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(788, 35);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 18);
            this.label9.TabIndex = 18;
            this.label9.Text = "m";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(581, 32);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 18);
            this.label8.TabIndex = 17;
            this.label8.Text = "m";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(385, 35);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "m";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // vdCommandLine1
            // 
            this.vdCommandLine1.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.vdCommandLine1.EnablePopupForm = true;
            this.vdCommandLine1.Location = new System.Drawing.Point(914, 139);
            this.vdCommandLine1.Margin = new System.Windows.Forms.Padding(6);
            this.vdCommandLine1.MaxNumberOfCommandsShown = 10;
            this.vdCommandLine1.Name = "vdCommandLine1";
            this.vdCommandLine1.PopupBackColor = System.Drawing.Color.White;
            this.vdCommandLine1.PopupFormFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.vdCommandLine1.PopupFormShowBelow = false;
            this.vdCommandLine1.PopupFormShowIcons = true;
            this.vdCommandLine1.PopupFormWidth = 250;
            this.vdCommandLine1.PopuphighlightColor = System.Drawing.SystemColors.Highlight;
            this.vdCommandLine1.ProcessKeyMessages = true;
            this.vdCommandLine1.ShowPopupFormPerigram = true;
            this.vdCommandLine1.Size = new System.Drawing.Size(88, 32);
            this.vdCommandLine1.TabIndex = 8;
            this.vdCommandLine1.UserTextString = "";
            this.vdCommandLine1.Visible = false;
            this.vdCommandLine1.Load += new System.EventHandler(this.vdCommandLine1_Load);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(828, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "请选择模板";
            // 
            // cmbMoBan
            // 
            this.cmbMoBan.FormattingEnabled = true;
            this.cmbMoBan.Location = new System.Drawing.Point(932, 29);
            this.cmbMoBan.Name = "cmbMoBan";
            this.cmbMoBan.Size = new System.Drawing.Size(176, 26);
            this.cmbMoBan.TabIndex = 6;
            this.cmbMoBan.SelectedIndexChanged += new System.EventHandler(this.cmbMoBan_SelectedIndexChanged);
            // 
            // tbStepLength
            // 
            this.tbStepLength.Location = new System.Drawing.Point(683, 25);
            this.tbStepLength.Margin = new System.Windows.Forms.Padding(4);
            this.tbStepLength.Name = "tbStepLength";
            this.tbStepLength.Size = new System.Drawing.Size(97, 28);
            this.tbStepLength.TabIndex = 3;
            this.tbStepLength.Text = "30";
            this.tbStepLength.TextChanged += new System.EventHandler(this.tbStepLength_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(630, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "步长";
            // 
            // tbEnd
            // 
            this.tbEnd.Location = new System.Drawing.Point(477, 25);
            this.tbEnd.Margin = new System.Windows.Forms.Padding(4);
            this.tbEnd.Name = "tbEnd";
            this.tbEnd.Size = new System.Drawing.Size(96, 28);
            this.tbEnd.TabIndex = 3;
            this.tbEnd.Text = "2600";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(434, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "终点";
            // 
            // tbStart
            // 
            this.tbStart.Location = new System.Drawing.Point(281, 25);
            this.tbStart.Margin = new System.Windows.Forms.Padding(4);
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(96, 28);
            this.tbStart.TabIndex = 3;
            this.tbStart.Text = "2500";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "起点";
            // 
            // tbJingHao
            // 
            this.tbJingHao.Location = new System.Drawing.Point(73, 22);
            this.tbJingHao.Margin = new System.Windows.Forms.Padding(4);
            this.tbJingHao.Name = "tbJingHao";
            this.tbJingHao.Size = new System.Drawing.Size(148, 28);
            this.tbJingHao.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "井号";
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 10000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1620, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // btnSuiZuan
            // 
            this.btnSuiZuan.Location = new System.Drawing.Point(1488, 22);
            this.btnSuiZuan.Name = "btnSuiZuan";
            this.btnSuiZuan.Size = new System.Drawing.Size(100, 33);
            this.btnSuiZuan.TabIndex = 26;
            this.btnSuiZuan.Text = "随钻";
            this.btnSuiZuan.UseVisualStyleBackColor = true;
            this.btnSuiZuan.Click += new System.EventHandler(this.btnSuiZuan_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(1488, 68);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(100, 38);
            this.btnPause.TabIndex = 27;
            this.btnPause.Text = "暂停随钻";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(350, 18);
            this.label10.TabIndex = 20;
            this.label10.Text = "注意：开始随钻后，比例尺和模板不能更改";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // SuiZuanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1620, 620);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SuiZuanForm";
            this.Text = "随钻";
            this.Load += new System.EventHandler(this.SuiZuanForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbJingHao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbStepLength;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Timer timer1;
        
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbInterval;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cmbMoBan;
        private VectorDraw.Professional.vdCommandLine.vdCommandLine vdCommandLine1;
        private System.Windows.Forms.Label 秒;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox lb_LineRoadDesign;
        private System.Windows.Forms.Button btnRemoveLine;
        public System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnSetWhole;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox tbDrillRadius;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnSuiZuan;
        private System.Windows.Forms.Label label10;
    }
}