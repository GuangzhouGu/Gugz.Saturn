using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DFS.DataFile
{
    public abstract class DFSDataFile
    {
        /// <summary>
        /// 将内容写入本地文件
        /// </summary>
        /// <param name="filename">文件名</param>
        public void WriteFile(string filename)
        {
            using (FileStream fsWrite = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                string str = GetContent();
                byte[] buffer = Encoding.Default.GetBytes(str);
                fsWrite.Write(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// 通过反射，获取属性值
        /// </summary>
        /// <param name="obj">类的实例</param>
        /// <param name="headers">DFS标题行</param>
        /// <param name="contents">DFS内容行</param>
        protected static void GetReflectionValues(object obj, string[] headers, string[] contents)
        {
            Type type = obj.GetType();
            var props = type.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo pi = props[i];
                for (int j = 0; j < headers.Length; j++)
                {
                    if (pi.Name.ToUpper() == headers[j].ToUpper())
                    {
                        if (pi.PropertyType.FullName == "System.Int32")
                        {
                            int val = 0;
                            int.TryParse(contents[j], out val);
                            pi.SetValue(obj, val, null);
                        }
                        else
                        {
                            pi.SetValue(obj, contents[j], null);
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取DFS文本内容
        /// </summary>
        /// <returns>内容</returns>
        public abstract string GetContent();
        /// <summary>
        /// 从本地文件转换成DFS数据类
        /// </summary>
        /// <param name="fileName">本地文件名</param>
        public abstract void ConvertFromFile(string fileName);
        /// <summary>
        /// 获取NG数据
        /// </summary>
        /// <returns></returns>
        //public abstract string GetNGLOG(string PanelID,int index);
    }
}
