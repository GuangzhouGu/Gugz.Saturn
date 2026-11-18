using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonModule.Entities
{
    class CommonHelper
    {
        public static bool IsDesingMode()
        {
            bool result = false;
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                result = true;
            }
            else if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                result = true;
            }
            return result;
        }
    }
}
