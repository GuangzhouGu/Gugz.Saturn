using System;
using System.Windows.Forms;
using System.Drawing;


namespace DrawTools
{
	/// <summary>
	/// Rectangle tool
	/// </summary>
	class ToolRectangle : DrawTools.ToolObject
	{

		public ToolRectangle()
		{
            //Cursor = new Cursor(GetType(), "Rectangle.cur");
		}

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            Point pointscroll = GetEventPointInArea(drawArea, e);
            DrawRectangle drawRectangle = new DrawRectangle(pointscroll.X, pointscroll.Y, 1, 1);
            drawRectangle.ClientRectangle = drawArea.ClientRectangle;
            drawRectangle.ROIType = drawArea.ActiveRoiType;
            //删除之前的同类似的
            int count = drawArea.GraphicsList.graphicsList.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (drawArea.GraphicsList.graphicsList[i].ROIType == drawArea.ActiveRoiType)
                {
                    drawArea.GraphicsList.graphicsList[i].Selected = true;
                    drawArea.GraphicsList.DeleteSelection();
                }
            }
           
            AddNewObject(drawArea, drawRectangle);
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            Point pointscroll = GetEventPointInArea(drawArea, e);
            drawArea.Cursor = Cursor;

            if ( e.Button == MouseButtons.Left )
            {
                drawArea.GraphicsList[0].MoveHandleTo(pointscroll, 5, drawArea.ClientRectangle);
                drawArea.Refresh();
                drawArea.GraphicsList.Dirty = true;
            }
        }
    }
}
