namespace MyClipBoard_Frm
{
    partial class ZFrm_Picture
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
            this.SuspendLayout();
            // 
            // ZFrm_Picture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ZFrm_Picture";
            this.Text = "ZFrm_Picture";
            this.Load += new System.EventHandler(this.ZFrm_Picture_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ZFrm_Picture_Paint);
            this.MouseEnter += new System.EventHandler(this.ZFrm_Picture_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ZFrm_Picture_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}