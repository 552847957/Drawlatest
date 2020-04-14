namespace LJJSCAD
{
    partial class SheDingMoBanForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMOBAN = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择画图模板";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmbMOBAN
            // 
            this.cmbMOBAN.FormattingEnabled = true;
            this.cmbMOBAN.Location = new System.Drawing.Point(135, 90);
            this.cmbMOBAN.Name = "cmbMOBAN";
            this.cmbMOBAN.Size = new System.Drawing.Size(210, 26);
            this.cmbMOBAN.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(172, 165);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(133, 57);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SheDingMoBanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 284);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbMOBAN);
            this.Controls.Add(this.label1);
            this.Name = "SheDingMoBanForm";
            this.Text = "设定画图模板";
            this.Load += new System.EventHandler(this.SheDingMoBanForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMOBAN;
        private System.Windows.Forms.Button btnOK;
    }
}