using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;
using WinFo.Service.Utility.Esent;
using WinFo.Service.Utility.IP;
using System.Security.Principal;

namespace WinFo.Service.Usage.Win10
{
    //based on information from https://www.sans.org/cyber-security-summit/archives/file/summit-archive-1492184583.pdf
    public class Win10SRUMService : ISRUMService
    {
        private static string _NETWORK_CONNECTIVITY_TABLE_NAME = "{DD6636C4-8929-4683-974E-22C046A43763}";
        private static string _APPLICATION_RESOURCE_USAGE_TABLE_NAME = "{D10CA2FE-6FCF-4F6D-848E-B2E99266FA89}";
        private static string _ID_MAP_TABLE_NAME = "SruDbIdMapTable";

        public List<SRUMApplicationResourceUsageDataEntry> GetSRUMApplicationResourceUsageDataEntries(string filePath)
        {
            filePath = filePath ?? @"C:\Windows\SysNative\sru\SRUDB.dat";

            List<SRUMApplicationResourceUsageDataEntry> entries = new List<SRUMApplicationResourceUsageDataEntry>();

            try
            {
                EsentReader er = new EsentReader(filePath);
           
                List<EsentTableRow> idMapRows = er.GetRows(_ID_MAP_TABLE_NAME);
                Dictionary<int, byte[]> idToEntity = new Dictionary<int, byte[]>();

                foreach (EsentTableRow row in idMapRows)
                {
                    if (row.Columns[0].Value != null && row.Columns[1].Value != null)
                    {
                        idToEntity.Add((int)row.Columns[1].Value, (byte[])row.Columns[0].Value);
                    }


                }

                List<EsentTableRow> appRows = er.GetRows(_APPLICATION_RESOURCE_USAGE_TABLE_NAME);

                foreach (EsentTableRow row in appRows)
                {
                    SRUMApplicationResourceUsageDataEntry sarude = new SRUMApplicationResourceUsageDataEntry();
                    sarude.BackgroundBytesRead = (Int64)row.Columns[2].Value;
                    sarude.BackgroundBytesWritten = (Int64)row.Columns[3].Value;
                    sarude.BackgroundContextSwitches = (Int32)row.Columns[4].Value;
                    sarude.BackgroundCycleTime = (Int64)row.Columns[5].Value;
                    sarude.BackgroundNumberOfFlushes = (Int32)row.Columns[6].Value;
                    sarude.BackgroundNumReadOperations = (Int32)row.Columns[7].Value;
                    sarude.BackgroundNumWriteOperations = (Int32)row.Columns[8].Value;
                    sarude.FaceTime = (Int64)row.Columns[9].Value;
                    sarude.ForegroundBytesRead = (Int64)row.Columns[10].Value;
                    sarude.ForegroundBytesWritten = (Int64)row.Columns[11].Value;
                    sarude.ForegroundContextSwitches = (Int32)row.Columns[12].Value;
                    sarude.ForegroundCycleTime = (Int64)row.Columns[13].Value;
                    sarude.ForegroundNumberOfFlushes = (Int32)row.Columns[14].Value;
                    sarude.ForegroundNumReadOperations = (Int32)row.Columns[15].Value;
                    sarude.ForegroundNumWriteOperations = (Int32)row.Columns[16].Value;
                    //column 0
                    sarude.AppName = idToEntity.Keys.Contains((int)row.Columns[0].Value) ? Encoding.Unicode.GetString(idToEntity[(int)row.Columns[0].Value]).TrimEnd('\0') : "Unknown";
                    //column 17
                    sarude.Timestamp = (DateTime)row.Columns[17].Value;
                    //TO-DO this is for testing only, fix to match generally
                    sarude.Timestamp = sarude.Timestamp.ToLocalTime();
                    //column 18
                    byte[] binarySid = idToEntity.Keys.Contains((int)row.Columns[18].Value) ? idToEntity[(int)row.Columns[18].Value] : null;

                    if (binarySid != null)
                    {
                        SecurityIdentifier si = new SecurityIdentifier(binarySid, 0);
                        sarude.SID = si.ToString();
                    }
                    
                    
                    entries.Add(sarude);

                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entries;
        }

        public List<SRUMNetworkConnectivityEntry> GetSRUMNetworkConnectivityEntries(string filePath)
        {
            filePath = filePath ?? @"C:\Windows\SysNative\sru\SRUDB.dat";

            List<SRUMNetworkConnectivityEntry> entries = new List<SRUMNetworkConnectivityEntry>();
            

            try
            {
                EsentReader er = new EsentReader(filePath);

                List<EsentTableRow> idMapRows = er.GetRows(_ID_MAP_TABLE_NAME);
                Dictionary<int, byte[]> idToEntity = new Dictionary<int, byte[]>();

                foreach (EsentTableRow row in idMapRows)
                {
                    if (row.Columns[0].Value != null && row.Columns[1].Value != null)
                    {
                        idToEntity.Add((int)row.Columns[1].Value, (byte[])row.Columns[0].Value);
                    }


                }

                List<EsentTableRow> rows = er.GetRows(_NETWORK_CONNECTIVITY_TABLE_NAME);

                foreach(EsentTableRow row in rows)
                {
                    SRUMNetworkConnectivityEntry snce = new SRUMNetworkConnectivityEntry();
                    snce.Timestamp = (DateTime)row.Columns[7].Value;
                    snce.Timestamp = snce.Timestamp.ToLocalTime();
                    snce.InterfaceIndex = IPInterfaceLUIDHelper.GetInterfaceIndexFromLuid((long)row.Columns[4].Value);
                    snce.ConectedStartTime = DateTime.FromFileTime((long)row.Columns[3].Value);
                    snce.ConnectedTime = (int)row.Columns[2].Value;
                    //TO-DO parse actual profile name from registry
                    snce.L2ProfileId = (int)row.Columns[6].Value;

                    entries.Add(snce);
                }

            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entries;
        }
    }
}
