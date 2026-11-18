using HYC.HTLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYC.SaturnAoi
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isNotRuned;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, "HYC.SaturnAoi", out isNotRuned);
            if (isNotRuned)
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FrmMain());
                }
                catch (Exception ex)
                {
                    LogManager.GetLogger("Program").Fatal(ex);
                    MessageBox.Show($"程序异常，异常信息：{ex.ToString()}!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("程序已启动!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
