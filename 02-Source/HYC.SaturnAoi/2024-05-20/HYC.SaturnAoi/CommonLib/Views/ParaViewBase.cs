using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.HTPCBase.Extension.Views
{
    public partial class ParaViewBase : Form
    {
        public ParaViewBase()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public virtual bool Save()
        {
            return true;
        }
        /// <summary>
        /// 是否自带保存按钮
        /// </summary>
        public bool IsHaveSaveBtn { get; set; }
    }
}
