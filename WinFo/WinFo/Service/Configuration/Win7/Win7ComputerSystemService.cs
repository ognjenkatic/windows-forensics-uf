using System;
using System.Management;
using WinFo.Model.Configuration;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Configuration.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the computer system
    /// </summary>
    public class Win7ComputerSystemService : IComputerSystemService
    {
        private static string _OPERATING_SYSTEM_SEARCH_STRING = "SELECT BuildNumber , Caption, NumberOfUsers, ServicePackMajorVersion, " +
            "ServicePackMinorVersion, Version, FreePhysicalMemory, FreeVirtualMemory, InstallDate, LastBootUpTime FROM Win32_OperatingSystem";

        private static string _COMPUTER_SYSTEM_INFORMATION_SEARCH_STRING = "SELECT NumberOfProcessors, NumberOfLogicalProcessors, SystemType FROM Win32_ComputerSystem";
        private static string _PHYSICAL_MEMORY_SEARCH_STRING = "SELECT Capacity, Speed FROM Win32_PhysicalMemory";

        private static string _COMPUTER_SYSTEM_SEARCH_STRING = "SELECT DNSHostName, Domain, DomainRole, Manufacturer, Model, PartOfDomain, Workgroup FROM Win32_ComputerSystem";


        /// <summary>
        /// Gets the computer system information
        /// </summary>
        /// <returns>The information about the computer system</returns>
        public ComputerSystem GetComputerSystem()
        {
            ComputerSystem computerSystem = new ComputerSystem();
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_OPERATING_SYSTEM_SEARCH_STRING);
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    
                    computerSystem.BuildNumber              = Convert.ToString(mo["BuildNumber"]);
                    computerSystem.FreePhysicalMemory       = Convert.ToUInt64(mo["FreePhysicalMemory"]);
                    computerSystem.FreeVirtualMemory        = Convert.ToUInt64(mo["FreeVirtualMemory"]);
                    computerSystem.Caption                  = Convert.ToString(mo["Caption"]);
                    computerSystem.NumberOfUsers            = Convert.ToUInt32(mo["NumberOfUsers"]);
                    computerSystem.ServicePackMajorVersion  = Convert.ToUInt16(mo["ServicePackMajorVersion"]);
                    computerSystem.ServicePackMinorVersion  = Convert.ToUInt16(mo["ServicePackMinorVersion"]);
                    computerSystem.Version                  = Convert.ToString(mo["Version"]);
                    computerSystem.InstallDate              = ManagementDateTimeConverter.ToDateTime(Convert.ToString(mo["InstallDate"]));
                    computerSystem.LastBootUpTime           = ManagementDateTimeConverter.ToDateTime(Convert.ToString(mo["LastBootUpTime"]));

                }

                mos.Query.QueryString = _COMPUTER_SYSTEM_INFORMATION_SEARCH_STRING;
                moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    CPUInfo cpui = new CPUInfo();

                    cpui.NumberOfPhysicalProcessors         = Convert.ToInt32(mo["NumberOfProcessors"]);
                    cpui.NumberOfLogicalProcessors          = Convert.ToInt32(mo["NumberOfLogicalProcessors"]);
                    cpui.Architecture                       = Convert.ToString(mo["SystemType"]);

                    computerSystem.CpuInfo = cpui;
                }

                mos.Query.QueryString = _PHYSICAL_MEMORY_SEARCH_STRING;
                moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    PhysicalMemory pm = new PhysicalMemory();
                    pm.Capacity                             = Convert.ToUInt64(mo["Capacity"]);
                    pm.Speed                                = Convert.ToUInt32(mo["Speed"]);

                    computerSystem.PhysicalMemoryCollection.Add(pm);
                }

                mos.Query.QueryString = _COMPUTER_SYSTEM_SEARCH_STRING;
                moc = mos.Get();
                
                foreach (ManagementObject mo in moc)
                {
                    computerSystem.DnsHostName              = Convert.ToString(mo["DNSHostName"]);
                    computerSystem.Domain                   = Convert.ToString(mo["Domain"]);
                    UInt16 domainRoleId                     = Convert.ToUInt16(mo["DomainRole"]);

                    // Decode the id to string (based on online documentation)
                    switch (domainRoleId)
                    {
                        case (0):
                            {
                                computerSystem.DomainRole = "Standalone Workstation";
                                break;
                            }
                        case (1):
                            {
                                computerSystem.DomainRole = "Member Workstation";
                                break;
                            }
                        case (2):
                            {
                                computerSystem.DomainRole = "Standalone Server";
                                break;
                            }
                        case (3):
                            {
                                computerSystem.DomainRole = "Member Server";
                                break;
                            }
                        case (4):
                            {
                                computerSystem.DomainRole = "Backup Domain Controller";
                                break;
                            }
                        case (5):
                            {
                                computerSystem.DomainRole = "Primary Domain Controller";
                                break;
                            }
                    }
                    computerSystem.Manufacturer             = Convert.ToString(mo["Manufacturer"]);
                    computerSystem.Model                    = Convert.ToString(mo["Model"]);
                    computerSystem.IsPartOfDomain           = Convert.ToBoolean(mo["PartOfDomain"]);
                    computerSystem.Workgroup                = Convert.ToString(mo["Workgroup"]);
                }



            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }
            return computerSystem;
        }
    }
}
