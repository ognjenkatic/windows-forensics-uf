using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Configuration.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the network adapters
    /// </summary>
    public class Win7NetworkAdapterService : INetworkAdapterService
    {
        #region fields
        private static string _NETWORK_ADAPTER_SEARCH_STRING = "SELECT IPEnabled, IPAddress, Caption, InterfaceIndex, DefaultIPGateway, DHCPEnabled, " +
            "DHCPLeaseObtained, DHCPLeaseExpires, DHCPServer, DNSDomain, DNSHostName, MACAddress," +
            "MTU, ServiceName FROM Win32_NetworkAdapterConfiguration";
        #endregion

        #region methods
        /// <summary>
        /// Gets a list of the network adapters in use
        /// </summary>
        /// <returns>The list of network adapters</returns>
        public List<NetworkAdapter> GetNetworkAdapters()
        {
            List<NetworkAdapter> networkAdapters = new List<NetworkAdapter>();
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_NETWORK_ADAPTER_SEARCH_STRING);
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    bool ipEnabled = Convert.ToBoolean(mo["IPEnabled"]);
                    string[] ipAddress = (string[])mo["IPAddress"];

                    if (ipEnabled && ipAddress != null)
                    {
                        NetworkAdapter na           = new NetworkAdapter();
                        na.Caption                  = Convert.ToString(mo["Caption"]);
                        na.Index                    = Convert.ToUInt32(mo["InterfaceIndex"]);
                        na.DefaultIPGateway         = (string[])mo["DefaultIPGateway"];
                        na.DhcpEnabled              = Convert.ToBoolean(mo["DHCPEnabled"]);
                        
                        if (na.DhcpEnabled)
                        {
                            string lo = Convert.ToString(mo["DHCPLeaseObtained"]);
                            string le = Convert.ToString(mo["DHCPLeaseExpires"]);

                            na.DhcpLeaseExpires = ManagementDateTimeConverter.ToDateTime(le);
                            na.DhcpLeaseObtained = ManagementDateTimeConverter.ToDateTime(lo);
                        }
                        na.DhcpServer       = Convert.ToString(mo["DHCPServer"]);
                        na.DnsDomain        = Convert.ToString(mo["DNSDomain"]);
                        na.DnsHostName      = Convert.ToString(mo["DNSHostName"]);
                        na.IpAddress        = ipAddress;
                        na.IpEnabled        = ipEnabled;
                        na.MacAddress       = Convert.ToString(mo["MACAddress"]);
                        na.Mtu              = Convert.ToUInt32(mo["MTU"]);
                        na.ServiceName      = Convert.ToString(mo["ServiceName"]);

                        networkAdapters.Add(na);
                    }
                }



            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return networkAdapters;
        }
        #endregion
    }
}
