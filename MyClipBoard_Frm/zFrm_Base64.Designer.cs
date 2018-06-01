using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace MyClipBoard_Frm
{
    partial class zFrm_Base64
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(2, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(294, 217);
            this.textBox1.TabIndex = 0;
            // 
            // zFrm_Base64
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 219);
            this.Controls.Add(this.textBox1);
            this.Name = "zFrm_Base64";
            this.Text = "Base64";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;

        public ZFrm_ToolBar MainFrm = null;

        public void SetPicture(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            ImageCodecInfo[] ImageCodecInfoArray = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegImageCodecInfo = null;
    for (int i = 0; i < ImageCodecInfoArray.Length; i++)
            {
                if (ImageCodecInfoArray[i].FormatDescription.Equals("JPEG"))
                {
                    jpegImageCodecInfo = ImageCodecInfoArray[i];
                    break;
                }
            }

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            myEncoder = Encoder.Quality;
            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            myEncoderParameters = new EncoderParameters(1);

            MemoryStream msJpg = new MemoryStream();
            myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp.Save(msJpg, jpegImageCodecInfo, myEncoderParameters);

            byte[] arrJpg = new byte[msJpg.Length];
            msJpg.Position = 0;
            msJpg.Read(arrJpg, 0, (int)msJpg.Length);
            msJpg.Close();
            var textJpg = Convert.ToBase64String(arrJpg);

            MemoryStream msPng = new MemoryStream();
            bmp.Save(msPng, System.Drawing.Imaging.ImageFormat.Png);
            byte[] arrPng = new byte[msPng.Length];
            msPng.Position = 0;
            msPng.Read(arrPng, 0, (int)msPng.Length);
            msPng.Close();
            var textPng = Convert.ToBase64String(arrPng);

            textBox1.Text = textJpg.Length < textPng.Length ? "data:image/jpeg;base64," + textJpg : "data:image/png;base64," + textPng;
        }
    }
}