using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;
using WinFo.Service.Utility.IP;

namespace WinFo.Service.Usage.Win7
{
    /// <summary>
    /// Service responsable for fetching information about te ARP table
    /// </summary>
    public class Win7ARPTableService : IARPTableService
    {
        /// <summary>
        /// Gets a list of ARP table entries
        /// </summary>
        /// <returns>The list of ARP table entries</returns>
        public List<ARPEntry> GetARPEntries()
        {
            List<ARPEntry> arpEntries = new List<ARPEntry>();

            try
            {
                IPHelperNetTableWrapper dsa = new IPHelperNetTableWrapper();
                MIB_IPNETROW[] table = dsa.GetIPNetRows();

                for (int index = 0; index < table.Length; index++)
                {
                    IPAddress ip = new IPAddress(table[index].dwAddr);
                    byte[] macBytes = new byte[] { table[index].mac0, table[index].mac1, table[index].mac2, table[index].mac3, table[index].mac4, table[index].mac5 };
                    string physicalAddress = string.Join(":", macBytes.Select(s => s.ToString("X2")));

                    ARPEntry ae = new ARPEntry();
                    ae.InternetAddress = ip.ToString();
                    ae.PhysicalAddress = physicalAddress;
                    ae.InterfaceIndex = table[index].dwIndex;

                    arpEntries.Add(ae);
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }
            return arpEntries;
        }
    }
}
