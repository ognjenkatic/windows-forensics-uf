using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;
using WinFo.Service.Usage.Win10;
using WinFo.Service.Usage.Win7;
using WinFo.Service.Utility.Esent;

namespace WinFo.Service
{
    /// <summary>
    /// Produces actual factories
    /// </summary>
    public class ServiceFactoryProducer
    {
        public static IServiceFactory GetServiceFactory()
        {
            IServiceFactory serviceFactory = null;
            
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT Version FROM win32_OperatingSystem");
                ManagementObjectCollection moc = mos.Get();

                string version = "unknown";

                foreach (ManagementObject mo in moc)
                {
                    version = Convert.ToString(mo["Version"]);
                }

                if (version.StartsWith("10."))
                {
                    serviceFactory = new Win10ServiceFactory();
                } else if (version.StartsWith("6.1"))
                {
                    serviceFactory = new Win7ServiceFactory();
                } else
                {
                    throw new Exception("Unable to determine OS version.");
                }
                
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return serviceFactory;
        }
    }
}
