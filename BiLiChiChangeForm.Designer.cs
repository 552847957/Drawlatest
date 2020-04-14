namespace LJJSCAD
{
    partial class BiLiChiChangeForm
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
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbXCOORD = new System.Windows.Forms.TextBox();
            this.tbYCOORD = new System.Windows.Forms.TextBox();
            this.btnBiLIChiOK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "设定x轴比例尺";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(80, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 18);
            this.label9.TabIndex = 10;
            this.label9.Text = "设定y轴比例尺";
            // 
            // tbXCOORD
            // 
            this.tbXCOORD.Location = new System.Drawing.Point(287, 63);
            this.tbXCOORD.Name = "tbXCOORD";
            this.tbXCOORD.Size = new System.Drawing.Size(100, 28);
            this.tbXCOORD.TabIndex = 11;
            // 
            // tbYCOORD
            // 
            this.tbYCOORD.Location = new System.Drawing.Point(287, 169);
            this.tbYCOORD.Name = "tbYCOORD";
            this.tbYCOORD.Size = new System.Drawing.Size(100, 28);
            this.tbYCOORD.TabIndex = 12;
            // 
            // btnBiLIChiOK
            // 
            this.btnBiLIChiOK.Location = new System.Drawing.Point(561, 56);
            this.btnBiLIChiOK.Name = "btnBiLIChiOK";
            this.btnBiLIChiOK.Size = new System.Drawing.Size(85, 38);
            this.btnBiLIChiOK.TabIndex = 13;
            this.btnBiLIChiOK.Text = "确定";
            this.btnBiLIChiOK.UseVisualStyleBackColor = true;
            this.btnBiLIChiOK.Click += new System.EventHandler(this.btnBiLIChiOK_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(519, 162);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 39);
            this.button1.TabIndex = 14;
            this.button1.Text = "默认比例尺";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BiLiChiChangeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 253);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnBiLIChiOK);
            this.Controls.Add(this.tbYCOORD);
            this.Controls.Add(this.tbXCOORD);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Name = "BiLiChiChangeForm";
            this.Text = "设定比例尺";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbXCOORD;
        private System.Windows.Forms.TextBox tbYCOORD;
        private System.Windows.Forms.Button btnBiLIChiOK;
        private System.Windows.Forms.Button button1;
    }
}