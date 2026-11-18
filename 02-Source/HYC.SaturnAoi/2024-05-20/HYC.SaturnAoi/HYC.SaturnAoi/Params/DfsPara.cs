using CommonLib.Para;
using HYC.HTPCBase.Extension.Views;
using HYC.SaturnAoi.SubForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYC.SaturnAoi.Params
{
    public class DfsPara : ParaBase, IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public bool IsShow { get; set; } = true;
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public ParaViewBase ParaViewBase { get; set; } = new FrmDfsSet();
        public string ParaName { get; set; } = "DFS参数";
        [Category("DFS参数"), Description("是否上传文件")]
        public bool IsUploadFile { get; set; }
        [Category("DFS参数"), Description("是否上传图片")]
        public bool IsUploadImg { get; set; }

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
