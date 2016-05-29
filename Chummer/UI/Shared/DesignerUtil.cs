using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chummer
{
    public static class DesignerUtil
    {
        public static bool InDesignMode()
        {
            try
            {
                return System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";
            }
            catch
            {
                return false;
            }
        }
    }
}
