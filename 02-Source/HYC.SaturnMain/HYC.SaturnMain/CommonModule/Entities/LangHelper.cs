using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonModule.Entities
{
    public class LangHelper
    {
        public static void ShiftLang<T>(Control control, T lang)
        {
            switch (lang.ToString())
            {
                case "En":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); ;
                    break;
                case "Zh":
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("zh-CN");
                    break;
                case "Jp":
                    break;
                default:
                    break;
            }
            ComponentResourceManager res = new ComponentResourceManager(control.GetType());
            Loading(control, res);
        }
        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="resources">语言资源</param>
        private static void Loading(Control control, ComponentResourceManager resources)
        {
            #region MyRegion
            //if (control is MenuStrip)
            //{
            //    //将资源与控件对应
            //    resources.ApplyResources(control, control.Name, Thread.CurrentThread.CurrentCulture);
            //    MenuStrip ms = (MenuStrip)control;
            //    if (ms.Items.Count > 0)
            //    {
            //        foreach (ToolStripMenuItem c in ms.Items)
            //        {
            //            //遍历菜单
            //            Loading(c, resources);
            //        }
            //    }
            //}
            //if (control is StatusStrip)
            //{
            //    //将资源与控件对应
            //    resources.ApplyResources(control, control.Name, Thread.CurrentThread.CurrentCulture);
            //    StatusStrip ts = (StatusStrip)control;

            //    foreach (ToolStripItem c in ts.Items)
            //    {
            //        //遍历菜单
            //        resources.ApplyResources(c, c.Name, Thread.CurrentThread.CurrentCulture);
            //    }
            //} 
            #endregion
            try
            {
                if (control is DataGridView)
                {
                    DataGridView grid = (DataGridView)control;
                    if (grid.Columns.Count > 0)
                    {
                        foreach (DataGridViewColumn c in grid.Columns)
                        {
                            resources.ApplyResources(c, c.Name, Thread.CurrentThread.CurrentCulture);
                        }
                    }
                }

                foreach (Control c in control.Controls)
                {
                    try
                    {
                        resources.ApplyResources(c, c.Name, Thread.CurrentThread.CurrentCulture);
                    }
                    catch (Exception ex)
                    { }

                    if (c.HasChildren)
                    {
                        Loading(c, resources);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public enum LangType
    {
        /// <summary>
        /// 英文
        /// </summary>
        En,
        /// <summary>
        /// 中文
        /// </summary>
        Zh,
        /// <summary>
        /// 日文
        /// </summary>
        Jp,
    }
}
