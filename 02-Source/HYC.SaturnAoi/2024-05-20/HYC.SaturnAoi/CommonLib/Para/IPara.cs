using HYC.HTPCBase.Extension.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Para
{
   public interface IPara
    {
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        string ParaName { get; set; }
        IPara LoadPara();
        bool SavePara();
        bool IsShow { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        ParaViewBase ParaViewBase { get; set; }
    }
}
