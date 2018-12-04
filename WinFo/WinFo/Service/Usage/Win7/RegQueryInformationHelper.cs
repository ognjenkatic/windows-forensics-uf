using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win7
{
    public class RegQueryInformationHelper
    {
        [DllImport("advapi32.dll", CallingConvention = CallingConvention.Winapi)]
        extern private static int RegQueryInfoKey(
            SafeRegistryHandle handle,
            StringBuilder lpClass,
            int lpcbClass,
            int lpReserved,
            int lpcSubKeys,
            int lpcbMaxSubKeyLen,
            int lpcbMaxClassLen,
            int lpcValues,
            int lpcbMaxValueNameLen,
            int lpcbMaxValueLen,
            int lpcbSecurityDescriptor,
            out long lpftLastWriteTime);

        public DateTime GetLastWritten(RegistryKey registryKey)
        {
            DateTime lastWrite = DateTime.MinValue;
            try
            {
                long lastWriteLong = 0;
                if (RegQueryInfoKey(registryKey.Handle, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, out lastWriteLong) == 0)
                {
                    lastWrite = DateTime.FromFileTime(lastWriteLong);
                }
                else
                {
                    lastWrite = DateTime.MinValue;
                }
            } catch (Exception xc)
            {
                MyDebugger.Instance.LogMessage(xc, DebugVerbocity.Exception);
            }

            return lastWrite;
        }
    }
}
