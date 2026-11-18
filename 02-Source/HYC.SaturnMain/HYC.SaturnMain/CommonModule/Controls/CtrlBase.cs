using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonModule.Entities;

namespace CommonModule.Controls
{
    public partial class CtrlBase : UserControl
    {
        public CtrlBase()
        {
            InitializeComponent();
        }
        public virtual bool SwitchLang<T>(T lang)
        {
            LangHelper.ShiftLang(this, lang);
            return true;
        }
    }
}
