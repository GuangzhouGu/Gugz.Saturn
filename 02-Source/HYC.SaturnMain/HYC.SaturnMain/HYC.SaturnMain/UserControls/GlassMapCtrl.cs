using HYC.HTLog;
using HYC.HTPCBase.Extension.Entities;
using HYC.HTPCBase.Extension.Recipe;
using HYC.SaturnMain.Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnMain.UserControls
{
    public partial class GlassMapCtrl : UserControl
    {
        public GlassMapCtrl()
        {
            InitializeComponent();
            Init();
        }
        float GlassCut = 30, Zoom = 1F, EdgeDistance = 2F, RatioX = 1F, RatioY = 1F, ZoomOld = 1.0F, CenterRadius = 2.5F, NGCoorRadius = 2.0F, CenterX, CenterY;
        public Image Img = null;
        private Graphics Gp = null;
        public Color BackGroudColor = Color.White;
        public RectangleF DrawRect;
        /// <summary>
        /// 表格的左上角
        /// </summary>
        public PointF LeftTop = new PointF(0, 0);

        private bool LeftButtonPress = false;

        private new PointF MousePosition = new PointF(0, 0);

        private RectangleF[,] Panels;
        private PanelStatus[,] PanelStatuss;

        string[] Heads = new string[] { "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH",  "AJ", "AK", "AL", "AM", "AN", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ" };
        PanelPara PanelPara = null;
        private void GlassMap_Load(object sender, EventArgs e)
        {
            ParaManager.OnParaChange += ParaManager_OnParaChange;
        }
        private void Init()
        {
            try
            {
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
                this.MouseWheel += this_MouseWheel;
                this.MouseMove += this_MouseMove;
                this.MouseDown += this_MouseDown;
                this.MouseUp += this_MouseUp;
                PanelPara = RecipeManager.GetPara(ParaType.Panel) as PanelPara;
                Panels = new RectangleF[PanelPara.CellCountX, PanelPara.CellCountY];
                PanelStatuss = new PanelStatus[PanelPara.CellCountX, PanelPara.CellCountY];
                for (int i = 0; i < PanelPara.CellCountX; i++)
                {
                    for (int j = 0; j < PanelPara.CellCountY; j++)
                    {
                        PanelStatuss[i, j] = new PanelStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("GlassMapCtrl").Fatal(ex.ToString());
            }
        }
        private void ParaManager_OnParaChange(string obj)
        {
            switch (obj)
            {
                case "PanelPara":
                    {
                        PanelPara = RecipeManager.GetPara(ParaType.Panel) as PanelPara;
                        break;
                    }
            }

        }
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Zoom = 1F;
            LeftTop = new PointF(0, 0);
            Reset();
            this.Refresh();
        }
        private void ZoomFunc(float zoom)
        {
            ZoomOld = Zoom;
            Zoom = zoom;
            var zoomNew = zoom;
            var zoomOld = ZoomOld;
            var deltaZoomNewToOld = zoomNew / zoomOld;
            var zero = LeftTop;
            zero.X = MousePosition.X - (MousePosition.X - zero.X) * deltaZoomNewToOld;
            zero.Y = MousePosition.Y - (MousePosition.Y - zero.Y) * deltaZoomNewToOld;
            LeftTop = zero;
        }
        private void this_MouseWheel(object sender, MouseEventArgs e)
        {
            var delta = e.Delta;
            if (Math.Abs(delta) < 10)
            {
                return;
            }
            var mousePosition = new PointF();
            mousePosition.X = e.X;
            mousePosition.Y = e.Y;
            ZoomOld = Zoom;

            if (delta < 0)
            {
                Zoom -= FetchStep(delta);
            }
            else if (delta > 0)
            {
                Zoom += FetchStep(delta);
            }
            var zoomNew = Zoom;
            var zoomOld = ZoomOld;
            var deltaZoomNewToOld = zoomNew / zoomOld;
            //计算零点
            //任意比例下的鼠标点与零点的距离deltaPO1都等于鼠标点与零点在(0,0)且比例为1时的距离deltaPO乘以缩放比例zoom，
            //于是有deltaPO1=deltaPO*zoom,
            //记在第1种缩放比例zoom1下有deltaPO1=deltaPO*zoom1，
            //记在第2种缩放比例zoom2下有deltaPO2=deltaPO*zoom2，
            //将上面两式相比则有deltaPO2/deltaPO1=zoom2/zoom1，令deltaZoomNewToOld=zoom2/zoom1则有deltaPO2/deltaPO1=deltaZoomNewToOld
            //又鼠标点与零点的距离deltaPO=P(x,y)-O(x,y)，代入得
            //O.x2=P.x-(P.x-O.x1)*deltaZoomNewToOld;
            //O.y2=P.y-(P.y-O.y1)*deltaZoomNewToOld;
            var zero = LeftTop;
            zero.X = mousePosition.X - (mousePosition.X - zero.X) * deltaZoomNewToOld;
            zero.Y = mousePosition.Y - (mousePosition.Y - zero.Y) * deltaZoomNewToOld;
            LeftTop = zero;
            this.Refresh();
        }
        /// <summary>
        /// 获取缩放的步进
        /// </summary>
        /// <returns></returns>
        private float FetchStep(float delta)
        {
            if (Zoom == 1)
            {
                return delta > 0 ? 1 : 0.05f;
            }
            else
            {
                return Zoom >= 1 ? 1 : 0.05f;
            }
        }
        private void this_MouseMove(object sender, MouseEventArgs e)
        {
            var offsetX = e.X - MousePosition.X;
            var offsetY = e.Y - MousePosition.Y;
            if (LeftButtonPress)
            {
                LeftTop.X += offsetX;
                LeftTop.Y += offsetY;
                MousePosition.X = e.X;
                MousePosition.Y = e.Y;
                this.Refresh();
            }
        }
        private void this_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MousePosition.X = e.X;
                MousePosition.Y = e.Y;
                LeftButtonPress = true;
                this.Cursor = Cursors.Hand;
            }
            else if (e.Button == MouseButtons.Right)
            {
                MousePosition.X = e.X;
                MousePosition.Y = e.Y;
            }
        }
        private void this_MouseUp(object sender, MouseEventArgs e)
        {
            LeftButtonPress = false;
            this.Cursor = Cursors.Default;
        }
        private void GlassMap_SizeChanged(object sender, EventArgs e)
        {
            if (ClassHelper.IsDesingMode() == false)
            {
                Reset();
            }
        }

      
        public void Draw(bool isInit=false)
        {
            try
            {
                DrawRect = new RectangleF(LeftTop.X + EdgeDistance, LeftTop.Y + EdgeDistance, (Width - 2 * EdgeDistance) * Zoom, (Height - 2 * EdgeDistance) * Zoom);
                Gp.FillRectangle(Brushes.LightYellow,DrawRect);
                if (isInit)
                {
                    Panels = new RectangleF[PanelPara.CellCountX, PanelPara.CellCountY];
                    PanelStatuss = new PanelStatus[PanelPara.CellCountX, PanelPara.CellCountY];
                    for (int i = 0; i < PanelPara.CellCountX; i++)
                    {
                        for (int j = 0; j < PanelPara.CellCountY; j++)
                        {
                            PanelStatuss[i, j] = new PanelStatus { Status = PanelStatusEnum.Wait, CoorX = new double[5], CoorY = new double[5] };
                        }
                    }
                }
                Gp.DrawLines(new Pen(Brushes.Black), new PointF[] { new PointF(DrawRect.X, DrawRect.Y), new PointF(DrawRect.X + DrawRect.Width - GlassCut * Zoom * RatioX, DrawRect.Y), new PointF(DrawRect.X + DrawRect.Width, DrawRect.Y + GlassCut * Zoom * RatioY), new PointF(DrawRect.X + DrawRect.Width, DrawRect.Y + DrawRect.Height), new PointF(DrawRect.X, DrawRect.Y + DrawRect.Height), new PointF(DrawRect.X, DrawRect.Y) });
                CenterX = DrawRect.X + DrawRect.Width / 2;
                CenterY = DrawRect.Y + DrawRect.Height / 2;
                DrawPanels(IsHideNum);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("GlassMapCtrl").Fatal(ex.ToString());
            }
        }
        bool IsHideNum = false;
        private void hideNumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsHideNum = !IsHideNum;
            DrawPanels(IsHideNum);
            this.Refresh();
        }

        private void GlassMap_Paint(object sender, PaintEventArgs e)
        {
            if (Img == null)
            {
                Img = new Bitmap(this.Width, this.Height);
            }
            if (Gp == null)
            {
                Gp = Graphics.FromImage(Img);
            }
            Gp.Clear(this.BackColor);
            Draw();
            e.Graphics.DrawImage(Img, new Point(0, 0));
        }

        private void x50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomFunc(5F);
            this.Refresh();
        }

        private void x100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomFunc(10F);
            this.Refresh();
        }

        private void x200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomFunc(20F);
            this.Refresh();
        }

        private void x500ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomFunc(50F);
            this.Refresh();
        }

        private void DrawPanels(bool isHideNum=false)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(DrawPanels),isHideNum);
            }
            else
            {
                try
                {
                    string[] sortStr = PanelPara.PanelSort.Split('X');
                    int[] sort = sortStr.Select(p => int.Parse(p)).ToArray();
                    for (int i = 0; i < PanelPara.CellCountX; i++)
                    {
                        RectangleF[] temp = new RectangleF[PanelPara.CellCountY];
                        for (int j = 0; j < PanelPara.CellCountY; j++)
                        {
                            RectangleF panel = new RectangleF();
                            switch (sort.Length)
                            {
                                case 2:
                                    {
                                        if (j < sort[0])
                                        {
                                            panel = new RectangleF(DrawRect.X + (PanelPara.CellEdgeOffsetX + i * (PanelPara.CellIntervalX + PanelPara.CellSizeX)) * Zoom * RatioX, DrawRect.Y + (PanelPara.CellEdgeOffsetY + j * (PanelPara.CellIntervalY + PanelPara.CellSizeY)) * Zoom * RatioY, PanelPara.CellSizeX * Zoom * RatioX, PanelPara.CellSizeY * Zoom * RatioY);
                                        }
                                        else if (j >= sort[0])
                                        {
                                            panel = new RectangleF(DrawRect.X + (PanelPara.CellEdgeOffsetX + i * (PanelPara.CellIntervalX + PanelPara.CellSizeX)) * Zoom * RatioX, DrawRect.Y + PanelPara.CellIntervalY2 * Zoom * RatioY + (PanelPara.CellEdgeOffsetY + j * (PanelPara.CellIntervalY + PanelPara.CellSizeY)) * Zoom * RatioY, PanelPara.CellSizeX * Zoom * RatioX, PanelPara.CellSizeY * Zoom * RatioY);
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        if (j < sort[0])
                                        {
                                            panel = new RectangleF(DrawRect.X + (PanelPara.CellEdgeOffsetX + i * (PanelPara.CellIntervalX + PanelPara.CellSizeX)) * Zoom * RatioX, DrawRect.Y + (PanelPara.CellEdgeOffsetY + j * (PanelPara.CellIntervalY + PanelPara.CellSizeY)) * Zoom * RatioY, PanelPara.CellSizeX * Zoom * RatioX, PanelPara.CellSizeY * Zoom * RatioY);
                                        }
                                        else if (j >= sort[0] && j < sort[0] + sort[1])
                                        {
                                            panel = new RectangleF(DrawRect.X + (PanelPara.CellEdgeOffsetX + i * (PanelPara.CellIntervalX + PanelPara.CellSizeX)) * Zoom * RatioX, DrawRect.Y + PanelPara.CellIntervalY2 * Zoom * RatioY + (PanelPara.CellEdgeOffsetY + j * (PanelPara.CellIntervalY + PanelPara.CellSizeY)) * Zoom * RatioY, PanelPara.CellSizeX * Zoom * RatioX, PanelPara.CellSizeY * Zoom * RatioY);
                                        }
                                        else
                                        {
                                            panel = new RectangleF(DrawRect.X + (PanelPara.CellEdgeOffsetX + i * (PanelPara.CellIntervalX + PanelPara.CellSizeX)) * Zoom * RatioX, DrawRect.Y + PanelPara.CellIntervalY2 * 2 * Zoom * RatioY + (PanelPara.CellEdgeOffsetY + j * (PanelPara.CellIntervalY + PanelPara.CellSizeY)) * Zoom * RatioY, PanelPara.CellSizeX * Zoom * RatioX, PanelPara.CellSizeY * Zoom * RatioY);
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        break;
                                    }
                                default:
                                    break;
                            }
                            if (PanelStatuss[i, j] != null)
                            {
                                FillRect(i, j, panel);
                            }
                            string str = string.Empty;
                            if (isHideNum == false)
                            {
                                str = Heads[j] + (i + 1).ToString("00");
                            }
                            Gp.DrawString(str, new Font("微软雅黑", 9 * Zoom), new SolidBrush(Color.Black), panel);
                            Gp.DrawString(PanelStatuss[i, j].Text, new Font("微软雅黑", 8 * Zoom, FontStyle.Bold), new SolidBrush(Color.Black), panel.X+panel.Width/5, panel.Y + panel.Height / 3);
                           temp[j] = panel;
                            Panels[i, j] = panel;
                        }
                        Gp.DrawRectangles(new Pen(Brushes.Black), temp);
                    }

                    //绘制Glass中心点
                    float centerRadius = CenterRadius * Zoom;
                    Gp.DrawEllipse(new Pen(Color.White), (float)(CenterX - centerRadius), (float)(CenterY - centerRadius), centerRadius * 2, centerRadius * 2);

                    #region 绘制不良点
                    for (int i = 0; i < PanelPara.CellCountX; i++)
                    {
                        for (int j = 0; j < PanelPara.CellCountY; j++)
                        {
                            float radius = NGCoorRadius * Zoom;
                            for (int m = 0; m < PanelStatuss[i, j].CoorX.Length; m++)
                            {
                                if (PanelStatuss[i, j].CoorX[m] != 0 && PanelStatuss[i, j].CoorY[m] != 0)
                                {
                                    //绘制不良点
                                    Gp.DrawEllipse(new Pen(Color.Cyan), CenterX + (float)PanelStatuss[i, j].CoorX[m] * Zoom * RatioX - radius, CenterY - (float)PanelStatuss[i, j].CoorY[m] * Zoom * RatioY - radius, radius * 2, radius * 2);
                                    //绘制不良点坐标
                                    Gp.DrawString(PanelStatuss[i, j].CoorX[m].ToString() + "\n" + PanelStatuss[i, j].CoorY[m].ToString(), new Font("微软雅黑", 5 * Zoom), new SolidBrush(Color.Blue), new PointF(CenterX + (float)PanelStatuss[i, j].CoorX[m] * Zoom * RatioX - radius, CenterY - (float)PanelStatuss[i, j].CoorY[m] * Zoom * RatioY));
                                }

                            }
                        }
                    } 
                    #endregion

                }
                catch (Exception ex)
                {
                    LogManager.GetLogger("GlassMapCtrl").Fatal(ex.ToString());
                }
            }
    
        }
        private void FillRect(int colNum,int cellNum, RectangleF cell)
        {
            try
            {
                var oneCell = GetOnePanelStatus(colNum, cellNum);
                Gp.FillRectangle(new SolidBrush(GetColor(oneCell.Status)), cell);
                var ngCount = oneCell.NGList.Count;
                var ngRectWidth = cell.Width / 5;
                RectangleF ngRect = RectangleF.Empty;
                for (int i = 0; i < ngCount; i++)
                {                  
                    if (ngRect== RectangleF.Empty)
                    {
                        ngRect = new RectangleF();
                    }
                    ngRect.X = cell.X + ngRectWidth * i;
                    ngRect.Y = cell.Y;
                    ngRect.Width = ngRectWidth;
                    ngRect.Height = cell.Height;
                    Gp.FillRectangle(new SolidBrush(GetColor(oneCell.NGList[i])), ngRect);
                    Gp.DrawRectangles(new Pen(GetColor(oneCell.NGList[i])), new RectangleF[] { ngRect });
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("GlassMapCtrl").Fatal(ex.ToString());
            }
        }
      
        public void RefreshPanelSts()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(RefreshPanelSts));
            }
            else
            {
                this.Refresh();
            }
        }
     
        public virtual void Reset()
        {
            DrawRect = new RectangleF(LeftTop.X + EdgeDistance, LeftTop.Y + EdgeDistance, (Width - 2 * EdgeDistance) * Zoom, (Height - 2 * EdgeDistance) * Zoom);
            RatioX = Convert.ToSingle(DrawRect.Width) / PanelPara.GlassSizeX;
            RatioY = Convert.ToSingle(DrawRect.Height) / PanelPara.GlassSizeY;
        }

        private Color GetColor(PanelStatusEnum status)
        {
            Color ret = Color.Khaki;
            switch (status)
            {
                case PanelStatusEnum.Wait:
                    ret = Color.Gold;
                    break;
                case PanelStatusEnum.Detecting:
                    ret = Color.SkyBlue;
                    break;
                case PanelStatusEnum.OK:
                    ret = Color.LimeGreen;
                    break;
                case PanelStatusEnum.UpstreamNG:
                    ret = Color.Red;
                    break;
                case PanelStatusEnum.OpenNG:
                    ret = Color.DeepPink;
                    break;
                case PanelStatusEnum.ShortNG:
                    ret = Color.LightCoral;
                    break;
                case PanelStatusEnum.AoiNG:
                    ret = Color.Violet;
                    break;
                case PanelStatusEnum.MuraNG:
                    ret = Color.MediumSlateBlue;
                    break;
                case PanelStatusEnum.SquintMuraNG:
                    ret = Color.DarkViolet;
                    break;
                case PanelStatusEnum.MultiAngleNG:
                    ret = Color.Firebrick;
                    break;
                case PanelStatusEnum.NG:
                    ret = Color.White;
                    break;
            }
            return ret;
        }
        public PanelStatus GetOnePanelStatus(int colNum,int cellNum)
        {
            return PanelStatuss[colNum,cellNum];
        }
        public PanelStatus[,] GetPanelStatuses()
        {
            return PanelStatuss;
        }
    }
    public class PanelStatus
    {   
        /// <summary>
        /// 0：等待，1：检测中，2：OK，3：NG，4：Repair，5：Skip
        /// </summary>
        public PanelStatusEnum Status { get; set; }
        public List<PanelStatusEnum> NGList { get; set; }
        public double[] CoorX { get; set; }
        public double[] CoorY { get; set; }
        public PanelStatus()
        {
            Status = PanelStatusEnum.Wait;
           CoorX = new double[5];
            CoorY = new double[5];
            NGList = new List<PanelStatusEnum>();
        }
        public string Text { get; set; } = string.Empty;
    }
    public enum PanelStatusEnum
    {
        Wait,
        Detecting,
        OK,
        UpstreamNG,
        OpenNG,
        ShortNG,
        AoiNG,
        MuraNG,
        SquintMuraNG,
        MultiAngleNG,
        NG,
    }

}
