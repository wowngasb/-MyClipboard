using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
namespace MyClipBoard_Frm
{
    public class ZImage
    {
        /// <summary>
        /// 比较两个图像是否一样。
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <returns></returns>
        public static bool isSameImage(Image img1,Image img2)
        {
            if (img1.Height != img2.Height || img1.Width != img2.Width) return false;
            if (img1.PhysicalDimension != img2.PhysicalDimension) return false;
            if (img1.PixelFormat != img2.PixelFormat) return false;
            Bitmap bmp1 = new Bitmap(img1);
            Bitmap bmp2 = new Bitmap(img2);
            if (bmp1.Size != bmp2.Size) return false;
            for(int x=0;x<bmp1.Width; x++)
            {
                for(int y=0;y<bmp1.Height; y++)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y)) return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 上下拼接两个图片。
        /// </summary>
        /// <param name="img1"></param>
        /// <param name="img2"></param>
        /// <returns></returns>
        public static Image Union(Image img1,Image img2)
        {
            int w = Math.Max(img1.Width, img2.Width);
            int h = img1.Height + img2.Height;
            Bitmap bmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(img1, 0, 0);
            g.DrawImage(img2, 0, img1.Height);
            return bmp;
        }
    }
}
