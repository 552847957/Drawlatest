namespace LJJSCAD.Util.UI
{
    partial class LegendDesign
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lb_LegendTbAndField = new System.Windows.Forms.ListBox();
            this.tb_LegendUnitHeigh = new System.Windows.Forms.TextBox();
            this.tb_LegendColumnNum = new System.Windows.Forms.TextBox();
            this.cb_LegendStyle = new System.Windows.Forms.ComboBox();
            this.cb_LegendPos = new System.Windows.Forms.ComboBox();
            this.chk_ifAddLegend = new System.Windows.Forms.CheckBox();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chk_ifAddLegend);
            this.groupBox7.Controls.Add(this.label25);
            this.groupBox7.Controls.Add(this.label24);
            this.groupBox7.Controls.Add(this.label23);
            this.groupBox7.Controls.Add(this.label22);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.lb_LegendTbAndField);
            this.groupBox7.Controls.Add(this.tb_LegendUnitHeigh);
            this.groupBox7.Controls.Add(this.tb_LegendColumnNum);
            this.groupBox7.Controls.Add(this.cb_LegendStyle);
            this.groupBox7.Controls.Add(this.cb_LegendPos);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(479, 284);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "设计";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 149);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(101, 12);
            this.label25.TabIndex = 9;
            this.label25.Text = "参加图例查询项目";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(228, 115);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 8;
            this.label24.Text = "单位高度";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(95, 115);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 12);
            this.label23.TabIndex = 7;
            this.label23.Text = "列数";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(228, 78);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 6;
            this.label22.Text = "类型";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(95, 78);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 5;
            this.label21.Text = "位置";
            // 
            // lb_LegendTbAndField
            // 
            this.lb_LegendTbAndField.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb_LegendTbAndField.FormattingEnabled = true;
            this.lb_LegendTbAndField.ItemHeight = 12;
            this.lb_LegendTbAndField.Location = new System.Drawing.Point(3, 181);
            this.lb_LegendTbAndField.Name = "lb_LegendTbAndField";
            this.lb_LegendTbAndField.Size = new System.Drawing.Size(473, 100);
            this.lb_LegendTbAndField.TabIndex = 4;
            // 
            // tb_LegendUnitHeigh
            // 
            this.tb_LegendUnitHeigh.Location = new System.Drawing.Point(287, 112);
            this.tb_LegendUnitHeigh.Name = "tb_LegendUnitHeigh";
            this.tb_LegendUnitHeigh.Size = new System.Drawing.Size(78, 21);
            this.tb_LegendUnitHeigh.TabIndex = 3;
            // 
            // tb_LegendColumnNum
            // 
            this.tb_LegendColumnNum.Location = new System.Drawing.Point(130, 112);
            this.tb_LegendColumnNum.Name = "tb_LegendColumnNum";
            this.tb_LegendColumnNum.Size = new System.Drawing.Size(78, 21);
            this.tb_LegendColumnNum.TabIndex = 2;
            // 
            // cb_LegendStyle
            // 
            this.cb_LegendStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_LegendStyle.FormattingEnabled = true;
            this.cb_LegendStyle.Items.AddRange(new object[] {
            "网格",
            "矩形框"});
            this.cb_LegendStyle.Location = new System.Drawing.Point(287, 73);
            this.cb_LegendStyle.Name = "cb_LegendStyle";
            this.cb_LegendStyle.Size = new System.Drawing.Size(83, 20);
            this.cb_LegendStyle.TabIndex = 1;
            // 
            // cb_LegendPos
            // 
            this.cb_LegendPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_LegendPos.FormattingEnabled = true;
            this.cb_LegendPos.Items.AddRange(new object[] {
            "顶部",
            "底部"});
            this.cb_LegendPos.Location = new System.Drawing.Point(130, 73);
            this.cb_LegendPos.Name = "cb_LegendPos";
            this.cb_LegendPos.Size = new System.Drawing.Size(83, 20);
            this.cb_LegendPos.TabIndex = 0;
            // 
            // chk_ifAddLegend
            // 
            this.chk_ifAddLegend.AutoSize = true;
            this.chk_ifAddLegend.Location = new System.Drawing.Point(97, 20);
            this.chk_ifAddLegend.Name = "chk_ifAddLegend";
            this.chk_ifAddLegend.Size = new System.Drawing.Size(96, 16);
            this.chk_ifAddLegend.TabIndex = 12;
            this.chk_ifAddLegend.Text = "是否绘制图例";
            this.chk_ifAddLegend.UseVisualStyleBackColor = true;
            // 
            // LegendDesign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 284);
            this.Controls.Add(this.groupBox7);
            this.Name = "LegendDesign";
            this.Text = "图例";
            this.Shown += new System.EventHandler(this.LegendDesign_Shown);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ListBox lb_LegendTbAndField;
        private System.Windows.Forms.TextBox tb_LegendUnitHeigh;
        private System.Windows.Forms.TextBox tb_LegendColumnNum;
        private System.Windows.Forms.ComboBox cb_LegendStyle;
        private System.Windows.Forms.ComboBox cb_LegendPos;
        private System.Windows.Forms.CheckBox chk_ifAddLegend;
    }
}