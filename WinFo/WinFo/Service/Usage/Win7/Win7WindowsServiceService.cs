using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win7
{
    /// <summary>
    /// Service reponsable for fetching information about the services on the system
    /// </summary>
    public class Win7WindowsServiceService : IWindowsServiceService
    {
        private static string _SERVICE_SEARCH_STRING = "SELECT * FROM Win32_Service";

        /// <summary>
        /// Gets a list of services
        /// </summary>
        /// <returns>The list of services</returns>
        public List<WindowsService> GetServices()
        {
            List<WindowsService> services = new List<WindowsService>();

            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_SERVICE_SEARCH_STRING);
                ManagementObjectCollection moc = mos.Get();

                foreach(ManagementObject mo in moc)
                {
                    WindowsService ws = new WindowsService();
                    ws.Caption = Convert.ToString(mo["Caption"]);
                    ws.Description = Convert.ToString(mo["Description"]);
                    ws.DisplayName = Convert.ToString(mo["DisplayName"]);
                    ws.DoesAcceptPause = Convert.ToBoolean(mo["AcceptPause"]);
                    ws.DoesAcceptStop = Convert.ToBoolean(mo["AcceptStop"]);
                    ws.IsDesktopInteract = Convert.ToBoolean(mo["DesktopInteract"]);
                    ws.IsStarted = Convert.ToBoolean(mo["Started"]);
                    ws.Name = Convert.ToString(mo["Name"]);
                    ws.PathName = Convert.ToString(mo["PathName"]);
                    ws.Pid = Convert.ToUInt32(mo["ProcessId"]);
                    ws.StartMode = Convert.ToString(mo["StartMode"]);
                    ws.StartName = Convert.ToString(mo["StartName"]);
                    ws.State = Convert.ToString(mo["State"]);
                    ws.Status = Convert.ToString(mo["Status"]);
                    ws.Type = Convert.ToString(mo["ServiceType"]);

                    services.Add(ws);
                }


            } catch (Exception xc)
            {
                MyDebugger.Instance.LogMessage(xc, DebugVerbocity.Exception);
            }

            return services;
        }
    }
}
