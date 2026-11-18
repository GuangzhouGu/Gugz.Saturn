using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace DrawTools
{
    public partial class DrawToolControl : UserControl
    {
        #region Members
        public int ActiveRoiType = 1;
        private string argumentFile = "";   // file name from command line
        #endregion

        #region Properties

        /// <summary>
        /// File name from the command line
        /// </summary>
        public string ArgumentFile
        {
            get
            {
                return argumentFile;
            }
            set
            {
                argumentFile = value;
            }
        }

        public DrawToolControl()
        {
            InitializeComponent();
            drawArea.Location = new System.Drawing.Point(0, 0);
            drawArea.Size = new System.Drawing.Size(10, 10);
            drawArea.Owner = null; // this;
            drawArea.Initialize(this);
            ResizeDrawArea();
            Application.Idle += delegate (object o, EventArgs a)
            {
                SetStateOfControls();
            };
            foreach (ToolStripItem item in menuStrip.Items)
            {
                if (item.GetType() == typeof(ToolStripMenuItem))
                {
                    ((ToolStripMenuItem)item).DropDownOpened += MainForm_DropDownOpened;
                }
            }
        }

        /// <summary>
        /// 初始化ROI
        /// </summary>
        /// <param name="fileName"></param>
        public void InitROI(string recipeFileName)
        {
            //
            this.drawArea.GraphicsList.Clear();
            ROIRectangles.LoadToGraphics(this.drawArea, recipeFileName);
            Refresh();
            drawArea.SetDirty();
        }

        /// <summary>
        /// 保存ROI区域
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            this.drawArea.SaveToStream(fileName);
        }

        public List<ROIRectangle> GetROIs()
        {
            ROIRectangles roiRects = new ROIRectangles();
            roiRects.LoadFromGraphics(this.drawArea);
            return roiRects.RoiRects;
        }
        private void DrawToolControl_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 更新背景
        /// </summary>
        /// <param name="image"></param>
        public void SetBackgroundImage(Bitmap image)
        {
            this.Invoke(new Action(()=>{
                drawArea.SetBackgroundImage(image);
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBackgroundImage()
        {
            return this.drawArea.GetBackgroundImage();
        }
        /// <summary>
        /// 双击放大和缩小
        /// </summary>
        /// <param name="eventHandler"></param>
        public void OnDoubleClickEvent(EventHandler eventHandler)
        {
            this.drawArea.DoubleClick += eventHandler;
        }

        /// <summary>
        /// Popup menu item (File, Edit ...) is opened.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_DropDownOpened(object sender, EventArgs e)
        {
            // Reset active tool to pointer.
            // This prevents bug in rare case when non-pointer tool is active, user opens
            // main main menu and after this clicks in the drawArea. MouseDown event is not
            // raised in this case (why ??), and MouseMove event works incorrectly.
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
        }
        /// <summary>
        /// Set draw area to all form client space except toolbar
        /// </summary>
        private void ResizeDrawArea()
        {
            Rectangle rect = this.ClientRectangle;

            drawArea.Left = rect.Left;
            drawArea.Top = rect.Top + menuStrip.Height + toolStrip.Height;
            drawArea.Width = rect.Width;
            drawArea.Height = rect.Height - menuStrip.Height - toolStrip.Height;
        }

        /// <summary>
        /// Get reference to Edit menu item.
        /// Used to show context menu in DrawArea class.
        /// </summary>
        /// <value></value>
        public ToolStripMenuItem ContextParent
        {
            get
            {
                return editToolStripMenuItem;
            }
        }

        public void SetStateOfControls()
        {
            // Select active tool
            toolStripButtonPointer.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Pointer);
            toolStripButtonRectangle.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Rectangle);
            toolStripButtonEllipse.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Ellipse);
            toolStripButtonLine.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Line);
            toolStripButtonPencil.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Polygon);

            pointerToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Pointer);
            rectangleToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Rectangle);
            ellipseToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Ellipse);
            lineToolStripMenuItem.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Line);

            if (drawArea.GraphicsList != null)
            {
                bool objects = (drawArea.GraphicsList.Count > 0);
                bool selectedObjects = (drawArea.GraphicsList.SelectionCount > 0);

                // Edit operations
                deleteToolStripMenuItem.Enabled = selectedObjects;
                deleteAllToolStripMenuItem.Enabled = objects;
                selectAllToolStripMenuItem.Enabled = objects;
                unselectAllToolStripMenuItem.Enabled = objects;
                moveToFrontToolStripMenuItem.Enabled = selectedObjects;
                moveToBackToolStripMenuItem.Enabled = selectedObjects;

            }

            // Undo, Redo
            undoToolStripMenuItem.Enabled = drawArea.CanUndo;
            toolStripButtonUndo.Enabled = drawArea.CanUndo;

            redoToolStripMenuItem.Enabled = drawArea.CanRedo;
            toolStripButtonRedo.Enabled = drawArea.CanRedo;
        }

        public void EnableToolStrip(bool val)
        {
            toolStrip.Enabled = val;
        }
        public void EnableDraw(bool val)
        {
            toolStripButtonRectangle.Enabled = val;
        }

        /// <summary>
        /// 是能CentorCross
        /// </summary>
        /// <param name="val"></param>
        public void EnableCenterCross(bool val)
        {
            drawArea.EnableDrawCenterCross = val;
            drawArea.Invalidate();
        }

        /// <summary>
        /// 保存截图
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveCapture(string fileName)
        {
            this.drawArea.SaveCapture(fileName);
        }

        /// <summary>
        /// 保存原始图片
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveOrigin(string fileName)
        {
            this.drawArea.SaveOrigin(fileName);
        }

        #region Toolbar Event Handlers
        private void toolStripButtonPointer_Click(object sender, EventArgs e)
        {
            CommandPointer();
        }

        private void toolStripButtonRectangle_Click(object sender, EventArgs e)
        {
            CommandRectangle();
        }

        private void toolStripButtonEllipse_Click(object sender, EventArgs e)
        {
            CommandEllipse();
        }

        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            CommandLine();
        }

        private void toolStripButtonPencil_Click(object sender, EventArgs e)
        {
            CommandPolygon();
        }

        private void toolStripButtonAbout_Click(object sender, EventArgs e)
        {
            CommandAbout();
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            CommandUndo();
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            CommandRedo();
        }
        /// <summary>
        /// Set Pointer draw tool
        /// </summary>
        public void CommandPointer()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Pointer;
        }

        /// <summary>
        /// Set Rectangle draw tool
        /// </summary>
        public void CommandRectangle()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Rectangle;
            drawArea.ActiveRoiType = ActiveRoiType;
        }

        /// <summary>
        /// Set Ellipse draw tool
        /// </summary>
        private void CommandEllipse()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Ellipse;
        }

        /// <summary>
        /// Set Line draw tool
        /// </summary>
        private void CommandLine()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Line;
        }

        /// <summary>
        /// Set Polygon draw tool
        /// </summary>
        private void CommandPolygon()
        {
            drawArea.ActiveTool = DrawArea.DrawToolType.Polygon;
        }

        /// <summary>
        /// Show About dialog
        /// </summary>
        private void CommandAbout()
        {
           
        }
        #endregion Toolbar Event Handlers

        #region Menu Event Handlers
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.GraphicsList.SelectAll();
            drawArea.Refresh();

        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawArea.GraphicsList.UnselectAll();
            drawArea.Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandDelete command = new CommandDelete(drawArea.GraphicsList);

            if (drawArea.GraphicsList.DeleteSelection())
            {
                drawArea.SetDirty();
                drawArea.Refresh();
                drawArea.AddCommandToHistory(command);
            }
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandDeleteAll command = new CommandDeleteAll(drawArea.GraphicsList);

            if (drawArea.GraphicsList.Clear())
            {
                drawArea.SetDirty();
                drawArea.Refresh();
                drawArea.AddCommandToHistory(command);
            }
        }

        private void moveToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawArea.GraphicsList.MoveSelectionToFront())
            {
                drawArea.SetDirty();
                drawArea.Refresh();
            }

        }

        private void moveToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawArea.GraphicsList.MoveSelectionToBack())
            {
                drawArea.SetDirty();
                drawArea.Refresh();
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawArea.GraphicsList.ShowPropertiesDialog(drawArea))
            {
                drawArea.SetDirty();
                drawArea.Refresh();
            }
        }

        private void pointerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandPointer();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandRectangle();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandEllipse();
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandLine();
        }

        private void pencilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandPolygon();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandUndo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandRedo();
        }
        /// <summary>
        /// Undo
        /// </summary>
        private void CommandUndo()
        {
            drawArea.Undo();
        }

        /// <summary>
        /// Redo
        /// </summary>
        private void CommandRedo()
        {
            drawArea.Redo();
        }

        #endregion
        #endregion Menu Event Handlers

    }
}
