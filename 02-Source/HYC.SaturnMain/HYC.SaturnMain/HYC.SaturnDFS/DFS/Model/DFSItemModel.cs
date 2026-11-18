using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace DFS.Model
{
    //[XmlInclude(typeof(PNL_INF))]
    //[XmlInclude(typeof(PNL_DFT))]
  
    public abstract class DFSItemModel
    {
        public string GetHeader()
        {
            Type type = this.GetType();
            PropertyInfo[] props = type.GetProperties();
            return GetHeaderStart()+ string.Join("^", props.Select(p => p.Name));
        }

        public string GetContent()
        {
            PropertyInfo[] props = this.GetType().GetProperties();
            IEnumerable<string> items = props.Select(p =>
            {
                object val = p.GetValue(this, null);
                if (val != null)
                {
                    if (string.IsNullOrEmpty(val.ToString()) == false)
                        return val.ToString();
                    else
                        return "***";
                }
                else
                {
                    return "***";
                }
            });
            return GetContentStart() + string.Join("^", items);
        }

        protected abstract string GetHeaderStart();
        protected abstract string GetContentStart();
    }
}
