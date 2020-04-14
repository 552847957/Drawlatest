namespace LJJSCAD
{
    partial class Childform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Childform));
            try
            {
                this.vdScrollableControl1 = new vdScrollableControl.vdScrollableControl();
            }
            catch
            {
                this.vdScrollableControl1 = new vdScrollableControl.vdScrollableControl();
            }
           // this.vdScrollableControl1 = new vdScrollableControl.vdScrollableControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // vdScrollableControl1
            // 
            this.vdScrollableControl1.BackColor = System.Drawing.SystemColors.Control;
            this.vdScrollableControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.vdScrollableControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.vdScrollableControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.vdScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdScrollableControl1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.vdScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.vdScrollableControl1.Margin = new System.Windows.Forms.Padding(6);
            this.vdScrollableControl1.Name = "vdScrollableControl1";
            this.vdScrollableControl1.ShowLayoutPopupMenu = true;
            this.vdScrollableControl1.Size = new System.Drawing.Size(1172, 572);
            this.vdScrollableControl1.TabIndex = 0;
            this.vdScrollableControl1.Load += new System.EventHandler(this.vdScrollableControl1_Load_1);
            this.vdScrollableControl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.vdScrollableControl1_DragEnter_1);
            this.vdScrollableControl1.MouseEnter += new System.EventHandler(this.vdScrollableControl1_MouseEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // Childform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 572);
            this.Controls.Add(this.vdScrollableControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Childform";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Childform";
            this.Load += new System.EventHandler(this.Childform_Load);
            this.SizeChanged += new System.EventHandler(this.Childform_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        internal vdScrollableControl.vdScrollableControl vdScrollableControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}