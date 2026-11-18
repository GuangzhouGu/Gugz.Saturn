using HYC.WindowsControls;
using HYC.WindowsForms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PPI_Vision.SubForms
{
    public partial class FrmContactCamera : Form
    {
        public FrmContactCamera()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Location = new Point(0, 125);
            //Test();
        }
        private void Test()
        {
            bool ret=Directory.Exists(@"D:\Pictures\Group1\1");
            string[] fileNames = null;
            if (ret)
            {
                fileNames=Directory.GetFiles(@"D:\Pictures\Group1\1");
            }
            else
            {
                return;
            }
            Task.Factory.StartNew(()=> {
                while (true)
                {
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        var img = Image.FromFile(fileNames[i]);
                        ContactmageReceived((Bitmap)img);
                        Thread.Sleep(100);
                    }
                 
                }
            });
        }
             
        public void ContactmageReceived(Bitmap bmp)
        {
            try
            {
                if (this.Visible)
                {
                    //if (InvokeRequired && this.IsDisposed == false)
                    //{
                    //    this.Invoke(new System.Action<Bitmap>(ContactmageReceived), bmp);
                    //}
                    //else
                    {
                        pictureBox1.Image = bmp;
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }
     
        Graphics Graphics = null;
        private void DrawCrossLine(PaintEventArgs e)
        {
            try
            {
                Graphics = e.Graphics;
                GrawLine();
            }
            catch (System.Exception)
            {

            }
        }

        private void GrawLine()
        {
            Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Graphics.CompositingQuality = CompositingQuality.GammaCorrected;
            //画

            SizeF size = Graphics.VisibleClipBounds.Size;
            Pen pen = new Pen(Color.SkyBlue, 1);
            pen.DashStyle = DashStyle.Solid;
            Graphics.DrawLine(pen, new PointF(0, size.Height / 2), new PointF(size.Width, size.Height / 2));
            Graphics.DrawLine(pen, new PointF(size.Width / 2, 0), new PointF(size.Width / 2, size.Height));
        }
        private void pbxContactCam_Paint(object sender, PaintEventArgs e)
        {
            DrawCrossLine(e);
        }


       
    }
}
