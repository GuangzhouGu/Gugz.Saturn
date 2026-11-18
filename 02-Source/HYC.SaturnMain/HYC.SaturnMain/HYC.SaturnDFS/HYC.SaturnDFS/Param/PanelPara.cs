using HYC.HTPCBase.Extension.Para;
using HYC.HTPCBase.Extension.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnDFS.Param
{
    public class PanelPara : ParaBase, IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public string ParaName { get; set; } = "Panel参数";
        [Category("Glass配置参数"), Description("玻璃X方向尺寸，单位毫米")]
        public float GlassSizeX { get; set; }
        [Category("Glass配置参数"), Description("玻璃Y方向尺寸，单位毫米")]
        public float GlassSizeY { get; set; }
        [Category("Panel配置参数"), Description("一块玻璃有几列")]
        public int CellCountX { get; set; }
        [Category("Panel配置参数"), Description("一块玻璃有几行")]
        public int CellCountY { get; set; }

        [Category("Panel配置参数"), Description("PanelX方向尺寸，单位毫米")]
        public float CellSizeX { get; set; }
        [Category("Panel配置参数"), Description("PanelY方向尺寸，单位毫米")]
        public float CellSizeY { get; set; }
        [Category("Panel配置参数"), Description("Panel距离玻璃边沿X方向相对位置，单位毫米")]
        public float CellEdgeOffsetX { get; set; }
        [Category("Panel配置参数"), Description("Panel距离玻璃边沿Y方向相对位置，单位毫米")]
        public float CellEdgeOffsetY { get; set; }
        [Category("Panel配置参数"), Description("每列Cell的X方向间距，单位毫米")]
        public float CellDistanceX { get; set; }
        [Category("Panel配置参数"), Description("每列Cell的Y方向间距，单位毫米")]
        public float CellDistanceY { get; set; }

        [Category("Panel配置参数"), Description("每列Cell的X方向间隔，单位毫米")]
        public float CellIntervalX { get; set; }
        [Category("Panel配置参数"), Description("每行Cell的Y方向间隔，单位毫米")]
        public float CellIntervalY { get; set; }
        [Category("Panel配置参数"), Description("第6行和第7行，第13行和第14行CellY方向间隔，单位毫米")]
        public float CellIntervalY2 { get; set; }

        [Category("GlassID配置参数"), Description("PanelID1 X方向坐标，单位毫米")]
        public double GlassID1X { get; set; }
        [Category("GlassID配置参数"), Description("PanelID1 Y方向坐标，单位毫米")]
        public double GlassID1Y { get; set; }
        [Category("GlassID配置参数"), Description("PanelID2 X方向坐标，单位毫米")]
        public double GlassID2X { get; set; }
        [Category("GlassID配置参数"), Description("PanelID2 Y方向坐标，单位毫米")]
        public double GlassID2Y { get; set; }

        [Category("Panel配置参数"), Description("Glass右下角十字Mark的X方向坐标，单位毫米")]
        public double RightMarkOffsetX { get; set; }
        [Category("Panel配置参数"), Description("Glass右下角十字Mark的Y方向坐标，单位毫米")]
        public double RightMarkOffsetY { get; set; }
        [Category("Panel配置参数"), Description("Glass右上角十字Mark的X方向坐标，单位毫米")]
        public double RightMarkOffsetX2 { get; set; }
        [Category("Panel配置参数"), Description("Glass右上角十字Mark的Y方向坐标，单位毫米")]
        public double RightMarkOffsetY2 { get; set; }
        [Category("Panel配置参数"), Description("Panel Y方向排列方式，例：7X6X7")]
        public string PanelSort { get; set; }
        [Category("Panel配置参数"), Description("GlassMark和Pad的距离")]
        public double MarkPadDis { get; set; }
        [Category("Panel配置参数"), Description("RX和TX距离")]
        public double RxTxOffset { get; set; }
        public IPara LoadPara()
        {
            return Load(this);
        }
        public bool SavePara()
        {
            return Save(this);
        }
    }
}
