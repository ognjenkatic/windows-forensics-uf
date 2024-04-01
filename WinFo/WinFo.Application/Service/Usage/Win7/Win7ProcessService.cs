﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;
using System.Diagnostics;
using System.Management;
using WinFo.Service.Utility.IP;

namespace WinFo.Service.Usage.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the currently active processes
    /// </summary>
    public class Win7ProcessService : IProcessService
    {
        private static string _PROCESS_SEARCH_STRING = "SELECT * FROM Win32_Process";

        /// <summary>
        /// Gets a dictionary that maps a list of connections to a PID
        /// </summary>
        /// <returns>The dictionary that maps conenctions to PIDS</returns>
        private Dictionary<uint,List<TCPConnection>> GetPidToConnectionsDictionary()
        {
            Dictionary<uint, List<TCPConnection>> pidToConnections = new Dictionary<uint, List<TCPConnection>>();
            try
            {
                IPHelperExtendedTCPTableWrapper iphw = new IPHelperExtendedTCPTableWrapper();
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

        /// <summary>
        /// Gets a list of currently active processses
        /// </summary>
        /// <returns>The list of currently active processes</returns>
        public List<Model.Usage.Process> GetProcesses()
        {
            List<Model.Usage.Process> processes = new List<Model.Usage.Process>();

            ManagementObjectSearcher mos = new ManagementObjectSearcher(_PROCESS_SEARCH_STRING);
            ManagementObjectCollection moc = mos.Get();

            foreach (ManagementObject mo in moc)
            {
                ManagementBaseObject ownerData = null;

                try
                {
                    ownerData = mo.InvokeMethod("GetOwner", null, null);
                }
                catch (Exception exc)
                {
                    MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
                }

                Model.Usage.Process pr = new Model.Usage.Process();

                pr.ReadAmount = Convert.ToUInt64(mo["ReadTransferCount"]);
                pr.SessionId = Convert.ToUInt32(mo["SessionId"]);
                pr.WriteAmount = Convert.ToUInt64(mo["WriteTransferCount"]);
                pr.Domain = ownerData == null ? "Unknown" :  Convert.ToString(ownerData["User"]);
                pr.User = ownerData == null ? "Unknown" : Convert.ToString(ownerData["Domain"]);
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

            for (int i = processes.Count - 1; i >= 0; i--)
            {
                Model.Usage.Process proc = processes.ElementAt(i);

                if (cd.ContainsKey(proc.Pid))
                {
                    proc.TCPConnections.AddRange(cd[proc.Pid]);
                }

                foreach (Model.Usage.Process cproc in processes)
                {
                    if (cproc.Pid == proc.ParentPid && proc.StartTime > cproc.StartTime)
                    {
                        cproc.ChildProcesses.Add(proc);
                        proc.IsOrphanProcess = false;
                        break;
                    }
                }
            }


            

            return processes;

        }
    }
}
