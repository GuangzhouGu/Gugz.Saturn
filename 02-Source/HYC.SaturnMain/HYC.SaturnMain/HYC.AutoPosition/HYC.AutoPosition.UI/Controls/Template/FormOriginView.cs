using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.AutoPosition.UI.Controls
{
    public partial class FormOriginView : Form
    {
        private LayerControl imageLayer;
        private LayerControl drawLayer;
        public Image image;
        public FormOriginView()
        {
            InitializeComponent();
        }
        public void InitSize(Size size)
        {
            pnlOriginView.Size = size;
            if (size.Width >= 1024)
            {
                size.Width = 1024;
            }
            if (size.Height >= 1024)
            {
                size.Height = 1024;
            }
            this.Size = size;
        }
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                imageLayer = new LayerControl(image.Width, image.Height, "image");
                drawLayer = new LayerControl(image.Width, image.Height, "draw");
                drawLayer.Location = new Point(0, 0);
                imageLayer.Location = new Point(0, 0);
                drawLayer.Dock = DockStyle.Fill;
                drawLayer.MouseDown += OnMouseDown;
                imageLayer.Dock = DockStyle.Fill;
                imageLayer.Controls.Add(drawLayer);
                imageLayer.Parent = pnlOriginView;
                imageLayer.Image = image;
                DrawRuler(image.Size);
                //drawLayer.DrawCenterLine(new Pen(Color.SkyBlue));
                this.Invalidate();
            }
        }
        /// <summary>
        /// 画卡尺
        /// </summary>
        /// <param name="size"></param>
        public void DrawRuler(Size size)
        {
            Pen pen = new Pen(Color.Red);
            int step = 10;
            int centerX = size.Width / 2;
            int centerY = size.Height / 2;
            int index = 0;
            int offset = 0;
            for (int i = centerX; i >= 0; i = i - step)
            {
                offset = index % 10 == 0 ? 6 : 3;
                drawLayer.DrawLine(pen, new Point(i, centerY - offset), new Point(i, centerY + offset));
                index++;
            }
            index = 0;
            for (int i = centerX; i <= size.Width; i = i + step)
            {
                offset = index % 10 == 0 ? 6 : 3;
                drawLayer.DrawLine(pen, new Point(i, centerY - offset), new Point(i, centerY + offset));
                index++;
            }
            index = 0;
            for (int i = centerY; i >= 0; i = i - step)
            {
                offset = index % 10 == 0 ? 6 : 3;
                drawLayer.DrawLine(pen, new Point(centerX- offset, i), new Point(centerX+ offset, i));
                index++;
            }
            index = 0;
            for (int i = centerY; i <= size.Height; i = i + step)
            {
                offset = index % 10 == 0 ? 6 : 3;
                drawLayer.DrawLine(pen, new Point(centerX - offset, i), new Point(centerX + offset, i));
                index++;
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs args)
        {
            drawLayer.Invalidate();
        }
    }
}
