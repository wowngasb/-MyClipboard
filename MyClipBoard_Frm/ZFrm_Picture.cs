using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClipBoard_Frm
{
    public partial class ZFrm_Picture : Form
    {
        public ZFrm_Picture()
        {
            InitializeComponent();
        }

        private void ZFrm_Picture_Paint(object sender, PaintEventArgs e)
        {
            if (m_lstImage.Count != 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
                e.Graphics.DrawImage(m_lstImage[m_lstImage.Count - 1], 0, 0);
            }
        }
        public List<Image> m_lstImage = new List<Image>();
        public void AddPicture(Image img)
        {
            if(m_lstImage.Count>0)
            {
                Image img2 = m_lstImage[m_lstImage.Count - 1];
                if (ZImage.isSameImage(img, img2)) return;//相同图片。
            }
            if (m_lstImage.Count > 32) m_lstImage.RemoveAt(0);
            m_lstImage.Add(img);
            FitImage(img);
            this.Invalidate();
        }
        public void RemoveLastPicture()
        {
            if(m_lstImage.Count>0)
            {
                m_lstImage.RemoveAt(m_lstImage.Count - 1);
                if (m_lstImage.Count > 0)
                {
                    FitImage(m_lstImage[m_lstImage.Count - 1]);
                    this.Invalidate();
                }else
                {
                    this.Hide();
                }
            }
        }
        public Image GetLastPicture()
        {
            if (m_lstImage.Count > 0)
            {
                return m_lstImage[m_lstImage.Count - 1];
            }
            return null;
        }
        public bool isAllowHide { get; set; } = false;
        public void FitImage(Image img=null)
        {
            int x;
            if (img != null)
                x = MainFrm.Location.X - img.Width - 1;
            else
                x = MainFrm.Location.X - this.Width;
            if (x < 0) x = MainFrm.Location.X + MainFrm.Width + 1;
            int y = MainFrm.Location.Y ;
            this.Location = new Point(x, y);
            if (img != null)
            {
                this.Width = img.Width + 1;
                this.Height = img.Height + 1;
            }
            if(img!=null&&MainFrm.WindowState==FormWindowState.Normal&&!isAllowHide)
                this.Show();
        }
        public void UnionImage()
        {
            if(m_lstImage.Count>1)
            {
                Image img2 = m_lstImage[m_lstImage.Count - 1];
                Image img1 = m_lstImage[m_lstImage.Count - 2];
                m_lstImage.RemoveRange(m_lstImage.Count - 2, 2);
                Image img = ZImage.Union(img1, img2);
                m_lstImage.Add(img);
                FitImage(img);
            }
        }
        public ZFrm_ToolBar MainFrm = null;

        private void ZFrm_Picture_MouseEnter(object sender, EventArgs e)
        {
            //MainFrm.SetPenetrate(this.Handle, 50);
        }

        private void ZFrm_Picture_MouseLeave(object sender, EventArgs e)
        {
            //MainFrm.SetPenetrate(this.Handle, 210);
        }

        private void ZFrm_Picture_Load(object sender, EventArgs e)
        {

        }
    }
}
