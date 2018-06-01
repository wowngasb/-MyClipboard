using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MyClipBoard_Frm
{
    public partial class ZFrm_ToolBar : Form
    {
        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        private static int WM_CLIPBOARDUPDATE = 0x031D;
        protected override void DefWndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE && this.WindowState == FormWindowState.Normal && !this.checkBox1.Checked)
            {
                if (Clipboard.ContainsImage())
                {
                    Image img = Clipboard.GetImage();
                    if (img != null)
                    {
                        PictureForm.AddPicture(img);
                    }
                }
            }
            base.DefWndProc(ref m);
        }
        //=============================
        //窗口透明。
        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int LWA_ALPHA = 2;
        private const int LWA_COLORKEY = 1;
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(
        IntPtr hwnd,
        int nIndex,
        uint dwNewLong
        );

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(
        IntPtr hwnd,
        int nIndex
        );

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(
        IntPtr hwnd,
        int crKey,
        int bAlpha,
        int dwFlags
        );

        /// <summary>
        /// 设置窗体具有鼠标穿透效果
        /// </summary>
        public void SetPenetrate(IntPtr Handle, int p)
        {
            uint old = GetWindowLong(Handle, GWL_EXSTYLE);
            SetWindowLong(Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED | old);
            SetLayeredWindowAttributes(Handle, 0, p, LWA_ALPHA);
        }
        public ZFrm_ToolBar()
        {
            PictureForm.MainFrm = this;
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }
        public ZFrm_Picture PictureForm = new ZFrm_Picture();

        public zFrm_Base64 Base64Form = new zFrm_Base64();

        /// <summary>
        /// 刷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                Image img = Clipboard.GetImage();
                if (img != null)
                {
                    PictureForm.AddPicture(img);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PictureForm.RemoveLastPicture();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image img = PictureForm.GetLastPicture();
            if (img != null)
            {
                Clipboard.SetImage(img);
            }
        }
        private Point ptMouseScreen = Point.Empty;
        private void ZFrm_ShowPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ptMouseScreen = Control.MousePosition;
        }

        private void ZFrm_ShowPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (ptMouseScreen.IsEmpty) return;
            Point mousePos = Control.MousePosition;

            int dx = mousePos.X - ptMouseScreen.X;
            int dy = mousePos.Y - ptMouseScreen.Y;
            this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            ptMouseScreen = mousePos;
            PictureForm.FitImage();
        }

        private void ZFrm_ShowPicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ptMouseScreen = Point.Empty;
        }

        private void ZFrm_Base64_Load(object sender, EventArgs e)
        {
            Base64Form.FormBorderStyle = FormBorderStyle.None;
            Base64Form.MaximizeBox = false;
            Base64Form.MinimizeBox = false;
            Base64Form.TopMost = true;
            Base64Form.ShowInTaskbar = false;
            Base64Form.Hide();
        }

        private void ZFrm_ShowPicture_Load(object sender, EventArgs e)
        {
            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 9 / 10, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height / 10);
            PictureForm.FormBorderStyle = FormBorderStyle.None;
            PictureForm.MaximizeBox = false;
            PictureForm.MinimizeBox = false;
            PictureForm.TopMost = true;
            PictureForm.ShowInTaskbar = false;
            PictureForm.Hide();
            //int x = this.Location.X - PictureForm.Width;
            //int y = this.Location.Y;
            //PictureForm.Location = new Point(x, y);
            this.Width = button1.Width + 2;

            //PictureForm.Show();
            AddClipboardFormatListener(this.Handle);
            SetPenetrate(PictureForm.Handle, 210);
        }

        private void ZFrm_ToolBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            RemoveClipboardFormatListener(this.Handle);
            Debug.WriteLine("111");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PictureForm.Hide();
            this.WindowState = FormWindowState.Minimized;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "隐")
            {
                PictureForm.Hide();
                PictureForm.isAllowHide = true;
                button5.Text = "显";
            }
            else
            {
                PictureForm.isAllowHide = false;
                Image img = PictureForm.GetLastPicture();
                if (img != null)
                {
                    PictureForm.FitImage(img);
                }
                button5.Text = "隐";
            }
        }
        /// <summary>
        /// 拼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            PictureForm.UnionImage();
        }
        /// <summary>
        /// 退
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ZFrm_ToolBar_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Image img = PictureForm.GetLastPicture();
                if (img != null)
                    PictureForm.FitImage(img);
                if (!timer1.Enabled)
                {
                    timer1.Interval = 333;
                    timer1.Start();
                }
            }else
            {
                timer1.Stop();
            }
        }
        bool isOutside = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int x=System.Windows.Forms.Cursor.Position.X;
            int y=System.Windows.Forms.Cursor.Position.Y;
            Rectangle r;
            if (isOutside)
            {
                r = new Rectangle(PictureForm.Location ,PictureForm.Size);
            }else
            {
                r = new Rectangle(PictureForm.Location, PictureForm.Size);
            }
            if(r.Contains(x,y)&&isOutside)
            {
                isOutside = false;
                SetPenetrate(PictureForm.Handle, 20);
            }else if(!r.Contains(x,y)&&!isOutside)
            {
                isOutside = true;
                SetPenetrate(PictureForm.Handle, 210);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "B")
            {
                Base64Form.Show();
                Base64Form.MainFrm = this;
                button8.Text = "H";
                if (Clipboard.ContainsImage())
                {
                    Image img = Clipboard.GetImage();
                    img = img != null ? img : PictureForm.GetLastPicture();
                    if(img != null ){
                        Base64Form.SetPicture(img);
                    }
                }
            } else
            {
                Base64Form.Hide();
                button8.Text = "B";
            }

        }
    }
}
