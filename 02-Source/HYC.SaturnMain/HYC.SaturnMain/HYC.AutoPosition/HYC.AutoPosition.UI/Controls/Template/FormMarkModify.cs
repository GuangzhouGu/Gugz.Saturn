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
    public partial class FormMarkModify : Form
    {
        public Image image;
        private LayerControl imageLayer;
        private LayerControl drawLayer;
        private int moveSpeed = 1;
        private Point currentCrossCenterPoint;
        private Point originCrossCenterPoint;

        public FormMarkModify()
        {
            
            InitializeComponent();
            checkBoxZoom1.Checked = true;
            cmbSpeed.SelectedIndex = 0;
        }
        
        private void checkBoxZoom_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox currentSelectCheckBox = (CheckBox)sender;
            foreach (Control c in groupBoxZoom.Controls)
            {
                if (c == currentSelectCheckBox)
                {
                    ((CheckBox)c).Checked = true;
                }
                else {
                    ((CheckBox)c).Checked = false;
                }
            }
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
                imageLayer.Dock = DockStyle.Fill;
                imageLayer.Controls.Add(drawLayer);
                imageLayer.Parent = panelImageViewer;
                imageLayer.Image = image;
                
                currentCrossCenterPoint = new Point((int)(imageLayer.RealSize().Width / 2), (int)(imageLayer.RealSize().Height / 2));
                originCrossCenterPoint = currentCrossCenterPoint;
                drawLayer.DrawCenterLine(new Pen(Color.SkyBlue,2));
                this.Invalidate();
            }
        }

        public Point GetOffset()
        {
            return new Point(currentCrossCenterPoint.X - originCrossCenterPoint.X, currentCrossCenterPoint.Y - originCrossCenterPoint.Y);
        }
        private enum MoveDir
        {
            Up = 0,
            Right = 1,
            Down,
            Left
        }

        
        private void btnMove_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            MoveDir mvoeDir = (MoveDir)Convert.ToInt32(btn.Tag);
            switch (mvoeDir)
            {
                case MoveDir.Up: currentCrossCenterPoint.Y -= moveSpeed; break;
                case MoveDir.Right: currentCrossCenterPoint.X += moveSpeed; break;
                case MoveDir.Down: currentCrossCenterPoint.Y += moveSpeed; break;
                case MoveDir.Left: currentCrossCenterPoint.X -= moveSpeed; break;
                default:break;
            }
            drawLayer.Clear(Color.Transparent);
            drawLayer.DrawCrossLine(new Pen(Color.SkyBlue),currentCrossCenterPoint);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void cmbSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool ret = Int32.TryParse(cmbSpeed.Text,out moveSpeed);
            if (ret == false)
            {
                moveSpeed = 1;
                cmbSpeed.SelectedIndex = 0;
            }
        }
    }
}
