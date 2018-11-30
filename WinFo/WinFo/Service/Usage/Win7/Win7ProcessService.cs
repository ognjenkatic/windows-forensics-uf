using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;
using System.Diagnostics;
using System.Management;

namespace WinFo.Service.Usage.Win7
{
    public class Win7ProcessService : IProcessService
    {
        private static string _PROCESS_SEARCH_STRING = "SELECT * FROM Win32_Process";


        private Dictionary<uint,List<TCPConnection>> GetPidToConnectionsDictionary()
        {
            Dictionary<uint, List<TCPConnection>> pidToConnections = new Dictionary<uint, List<TCPConnection>>();
            try
            {
                IPHelperWrapper iphw = new IPHelperWrapper();
                List<MIB_TCPROW_OWNER_PID> cns = iphw.GetAllTCPv4Connections();

                foreach(MIB_TCPROW_OWNER_PID cn in cns)
                {
                    TCPConnection con = new TCPConnection();

                    Array.Reverse(cn.localPort);
                    Array.Reverse(cn.remotePort);

                    byte[] la = BitConverter.GetBytes(cn.localAddr);
                    byte[] ra = BitConverter.GetBytes(cn.remoteAddr);

                    con.LocalIP = Convert.ToInt32(la[0]) + "." + Convert.ToUInt32(la[1]) + "" +
                        "." + Convert.ToUInt32(la[2]) + "." + Convert.ToUInt32(la[3]);

                    con.RemoteIP = Convert.ToInt32(ra[0]) + "." + Convert.ToUInt32(ra[1]) + "" +
                        "." + Convert.ToUInt32(ra[2]) + "." + Convert.ToUInt32(ra[3]);

                    con.State =  (TCPConnectionState)cn.state;
                    con.LocalPort = BitConverter.ToUInt16(cn.localPort, 2);
                    con.RemotePort = BitConverter.ToUInt16(cn.remotePort, 2);
                    if (!pidToConnections.ContainsKey(cn.owningPid))
                    {
                        pidToConnections.Add(cn.owningPid, new List<TCPConnection>());
                    }

                    pidToConnections[cn.owningPid].Add(con);
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return pidToConnections;
        }
        public List<Model.Usage.Process> GetProcesses()
        {
            List<Model.Usage.Process> processes = new List<Model.Usage.Process>();

            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_PROCESS_SEARCH_STRING);
                ManagementObjectCollection moc = mos.Get();

                foreach(ManagementObject mo in moc)
                {
                    ManagementBaseObject ownerData = mo.InvokeMethod("GetOwner", null, null);
                    
                    Model.Usage.Process pr = new Model.Usage.Process();

                    pr.ReadAmount = Convert.ToUInt64(mo["ReadTransferCount"]);
                    pr.WriteAmount = Convert.ToUInt64(mo["WriteTransferCount"]);
                    pr.Domain = Convert.ToString(ownerData["User"]);
                    pr.User = Convert.ToString(ownerData["Domain"]);
                    pr.CommandLine = Convert.ToString(mo["CommandLine"]);
                    pr.ReadCount = Convert.ToUInt64(mo["ReadOperationCount"]);
                    pr.WriteCount = Convert.ToUInt64(mo["WriteOperationCount"]);
                    pr.ParentPid = Convert.ToUInt32(mo["ParentProcessId"]);
                    pr.Pid = Convert.ToUInt32(mo["ProcessId"]);
                    pr.StartTime = ManagementDateTimeConverter.ToDateTime(Convert.ToString(mo["CreationDate"]));
                    pr.PhysicalMemory = Convert.ToUInt64(mo["WorkingSetSize"]);
                    pr.PagedMemorySize = Convert.ToUInt64(mo["PageFileUsage"]);
                    pr.FileName = Convert.ToString(mo["ExecutablePath"]);
                    pr.PeakPhysicalMemory = Convert.ToUInt64(mo["PeakWorkingSetSize"]);
                    pr.PeakPagedMemorySize = Convert.ToUInt64(mo["PeakPageFileUsage"]);
                    pr.ProcessName = Convert.ToString(mo["Name"]);

                    processes.Add(pr);
                }

                Dictionary<uint, List<TCPConnection>> cd = GetPidToConnectionsDictionary();

                foreach(Model.Usage.Process proc in processes)
                {
                    if (cd.ContainsKey(proc.Pid))
                    {
                        proc.TCPConnections.AddRange(cd[proc.Pid]);
                    }

                    foreach (Model.Usage.Process cproc in processes)
                    {
                        if (cproc.Pid == proc.ParentPid)
                        {
                            cproc.ChildProcesses.Add(proc);
                            break;
                        }
                    }
                }

            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return processes;

        }
    }
}
