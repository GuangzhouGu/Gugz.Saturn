
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DrawTools
{
    public partial class DrawArea : UserControl, DocumentDirtyObserver
    {

        public bool EnableDrawCenterCross = true;

        #region Constructor
        private Bitmap image;
        private Size preClientSize = Size.Empty;
        public DrawArea()
        {
            InitializeComponent();
            initialized = false;
        }

        #endregion Constructor

        #region Enumerations

        public enum DrawToolType
        {
            Pointer,
            Rectangle,
            Ellipse,
            Line,
            Polygon,
            NumberOfDrawTools
        };

        #endregion

        #region Members

        private GraphicsList graphicsList;    // list of draw objects
        // (instances of DrawObject-derived classes)

        private DrawToolType activeTool;      // active drawing tools
        private Tool[] tools;                 // array of tools

        // Information about owner form
        private DrawToolControl owner;
        private ContextMenuStrip m_ContextMenu;

        private UndoManager undoManager;

        private bool initialized;

        #endregion

        #region Properties

        /// <summary>
        /// Reference to the owner form
        /// </summary>
        public DrawToolControl Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        /// <summary>
        /// Active drawing tool.
        /// </summary>
        public DrawToolType ActiveTool
        {
            get
            {
                return activeTool;
            }
            set
            {
                activeTool = value;
            }
        }

        public int ActiveRoiType;

        /// <summary>
        /// List of graphics objects.
        /// </summary>
        public GraphicsList GraphicsList
        {
            get
            {
                return graphicsList;
            }
            set
            {
                graphicsList = value;
                if (this.GraphicsList != null)
                {
                    this.AdjustRendering();
                    this.graphicsList.AddObserver(this);
                }
            }
        }

        /// <summary>
        /// Return True if Undo operation is possible
        /// </summary>
        public bool CanUndo
        {
            get
            {
                if (undoManager != null)
                {
                    return undoManager.CanUndo;
                }

                return false;
            }
        }

        /// <summary>
        /// Return True if Redo operation is possible
        /// </summary>
        public bool CanRedo
        {
            get
            {
                if (undoManager != null)
                {
                    return undoManager.CanRedo;
                }

                return false;
            }
        }


        #endregion

        #region Other Functions
        private ImageAttributes imgAtb;
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="owner"></param>
        public void Initialize(DrawToolControl owner)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            // Keep reference to owner form
            this.Owner = owner;
            // set default tool
            activeTool = DrawToolType.Pointer;
            GraphicsList = new GraphicsList();

            // Create undo manager
            undoManager = new UndoManager(GraphicsList);

            // create array of drawing tools
            tools = new Tool[(int)DrawToolType.NumberOfDrawTools];
            tools[(int)DrawToolType.Pointer] = new ToolPointer();
            tools[(int)DrawToolType.Rectangle] = new ToolRectangle();
            tools[(int)DrawToolType.Ellipse] = new ToolEllipse();
            tools[(int)DrawToolType.Line] = new ToolLine();
            tools[(int)DrawToolType.Polygon] = new ToolPolygon();
            AdjustRendering();

            this.initialized = true;
            //GraphicsList.UnselectAll();
            //Rectangle clientRect = this.ClientRectangle;
            //drawBackGround = new DrawBackground(clientRect);
            //GraphicsList.Add(drawBackGround);
            //image = new Bitmap("D:\\Mark\\left.bmp");


            float[][] mtxItens = {
            new float[] {1,0,0,0,0},
            new float[] {0,1,0,0,0},
            new float[] {0,0,1,0,0},
            new float[] {0,0,0,1,0},
            new float[] {0,0,0,0,1}};
            ColorMatrix colorMatrix = new ColorMatrix(mtxItens);
            imgAtb = new ImageAttributes();
            imgAtb.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        }

        /// <summary>
        /// 更新背景
        /// </summary>
        /// <param name="_image"></param>
        public void SetBackgroundImage(Bitmap _image)
        {
            image = _image;
            Refresh();
        }

        /// <summary>
        /// 获取背景图
        /// </summary>
        public Bitmap GetBackgroundImage()
        {
            return image;
        }

        /// <summary>
        /// Add command to history.
        /// </summary>
        public void AddCommandToHistory(Command command)
        {
            undoManager.AddCommandToHistory(command);
        }

        /// <summary>
        /// Clear Undo history.
        /// </summary>
        public void ClearHistory()
        {
            undoManager.ClearHistory();
        }

        /// <summary>
        /// Undo
        /// </summary>
        public void Undo()
        {
            undoManager.Undo();
            Refresh();
        }

        /// <summary>
        /// Redo
        /// </summary>
        public void Redo()
        {
            undoManager.Redo();
            Refresh();
        }


        public void IsDirty(GraphicsList gList)
        {
            AdjustRendering();
        }

        /// <summary>
        /// Set dirty flag (file is changed after last save operation)
        /// </summary>
        public void SetDirty()
        {

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveToStream(string fileName)
        {
            ROIRectangles roiRectangles = new ROIRectangles();
            roiRectangles.LoadFromGraphics(this);
            roiRectangles.Serialize(fileName);
        }

        /// <summary>
        /// 保存当前截图
        /// </summary>
        public void SaveCapture(string fileName)
        {
            Bitmap imageToSave = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(imageToSave);
            PaintEventArgs pe = new PaintEventArgs(g, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            this.InvokePaintBackground(this, pe);
            this.InvokePaint(this, pe);
            imageToSave.Save(fileName);
            g.Dispose();
            imageToSave.Dispose();

        }

        /// <summary>
        /// 保存原始图片
        /// </summary>
        public void SaveOrigin(string fileName)
        {
            this.image.Save(fileName);
        }
        /// <summary>
        /// Right-click handler
        /// </summary>
        /// <param name="e"></param>
        private void OnContextMenu(MouseEventArgs e)
        {
            // Change current selection if necessary

            Point point = new Point(Math.Abs(this.AutoScrollPosition.X) + e.X, Math.Abs(this.AutoScrollPosition.Y) + e.Y);

            int n = GraphicsList.Count;
            DrawObject o = null;

            for (int i = 0; i < n; i++)
            {
                if (GraphicsList[i].HitTest(point) == 0)
                {
                    o = GraphicsList[i];
                    break;
                }
            }

            if (o != null)
            {
                if (!o.Selected)
                    GraphicsList.UnselectAll();

                // Select clicked object
                o.Selected = true;
            }
            else
            {
                GraphicsList.UnselectAll();
            }

            Refresh();      // in the case selection was changed

            // Show context menu.
            // Context menu items are filled from owner form Edit menu items.
            m_ContextMenu = new ContextMenuStrip();

            int nItems = owner.ContextParent.DropDownItems.Count;

            // Read Edit items and move them to context menu.
            // Since every move reduces number of items, read them in reverse order.
            // To get items in direct order, insert each of them to beginning.
            for (int i = nItems - 1; i >= 0; i--)
            {
                m_ContextMenu.Items.Insert(0, owner.ContextParent.DropDownItems[i]);
            }

            // Show context menu for owner form, so that it handles items selection.
            // Convert pointscroll from this window coordinates to owner's coordinates.
            point.X += this.Left;
            point.Y += this.Top;

            Point org = new Point(e.X, e.Y);
            m_ContextMenu.Show(owner, org);

            Owner.SetStateOfControls();  // enable/disable menu items

            // Context menu is shown, but owner's Edit menu is now empty.
            // Subscribe to context menu Closed event and restore items there.
            m_ContextMenu.Closed += delegate (object sender, ToolStripDropDownClosedEventArgs args)
            {
                if (m_ContextMenu != null)      // precaution
                {
                    nItems = m_ContextMenu.Items.Count;

                    for (int k = nItems - 1; k >= 0; k--)
                    {
                        owner.ContextParent.DropDownItems.Insert(0, m_ContextMenu.Items[k]);
                    }
                }
            };
        }

        #endregion

        #region Event Handlers
        /// <summary>
        /// Draw graphic objects and 
        /// group selection rectangle (optionally)
        /// </summary>
        private Graphics g;
        private void DrawArea_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.GammaCorrected;
            //画
            if (image != null)
                g.DrawImage(image, ClientRectangle, 0.0f, 0.0f, image.Width, image.Height, GraphicsUnit.Pixel, imgAtb);
            if (EnableDrawCenterCross)
            {
                SizeF size = g.VisibleClipBounds.Size;
                Pen pen = new Pen(Color.SkyBlue, 2);
                pen.DashStyle = DashStyle.DashDotDot;
                g.DrawLine(pen, new PointF(0, size.Height / 2), new PointF(size.Width, size.Height / 2));
                g.DrawLine(pen, new PointF(size.Width / 2, 0), new PointF(size.Width / 2, size.Height));
            }

            if (graphicsList != null)
            {
                graphicsList.Draw(e.Graphics);
            }
            //brush.Dispose();
        }

        /// <summary>
        /// Mouse down.
        /// Left button down event is passed to active tool.
        /// Right button down event is handled in this class.
        /// </summary>
        private void DrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                tools[(int)activeTool].OnMouseDown(this, e);
            else if (e.Button == MouseButtons.Right)
                OnContextMenu(e);
        }

        /// <summary>
        /// Mouse move.
        /// Moving without button pressed or with left button pressed
        /// is passed to active tool.
        /// </summary>
        private void DrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (!initialized)
                return;
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
                tools[(int)activeTool].OnMouseMove(this, e);
            else
                this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Mouse up event.
        /// Left button up event is passed to active tool.
        /// </summary>
        private void DrawArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                tools[(int)activeTool].OnMouseUp(this, e);
        }

        #endregion Event Handlers

        void AdjustRendering()
        {
            Invalidate();
        }

        /// <summary>
        /// 区域Size变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawArea_SizeChanged(object sender, EventArgs e)
        {
            if (preClientSize.IsEmpty)
            {
                preClientSize = this.ClientSize;
            }
            else
            {
                if ((graphicsList != null) && ClientSize.Width != 1 && ClientRectangle.Height != 1)
                {
                    SizeF scale = new SizeF(this.ClientSize.Width / (float)preClientSize.Width, this.ClientSize.Height / (float)preClientSize.Height);
                    graphicsList.Resize(scale, ClientRectangle);
                    preClientSize = this.ClientSize;
                }
            }
            Rectangle clientRect = this.ClientRectangle;

        }
    }
}
