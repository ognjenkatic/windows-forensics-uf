using System;
using System.Collections.Generic;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// General information about the computer system
    /// </summary>
    public class ComputerSystem
    {
        #region fields
        private string _dnsHostName;
        private string _domain;
        private string _domainRole;
        private string _manufacturer;
        private string _model;
        private bool _isPartOfDomain;
        private string _workgroup;
        private string _buildNumber;
        private string _caption;
        private UInt32 _numberOfUsers;
        private UInt16 _servicePackMajorVersion;
        private UInt16 _servicePackMinorVersion;
        private string _version;
        private UInt64 _freePhysicalMemory;
        private UInt64 _freeVirtualMemory;
        private DateTime _installDate;
        private DateTime _lastBootUpTime;

        private List<PhysicalMemory> _physicalMemoryCollection = new List<PhysicalMemory>();
        private CPUInfo _cpuInfo;

        #endregion

        #region properties
        public string BuildNumber { get => _buildNumber; set => _buildNumber = value; }
        public string Caption { get => _caption; set => _caption = value; }
        public uint NumberOfUsers { get => _numberOfUsers; set => _numberOfUsers = value; }
        public ushort ServicePackMajorVersion { get => _servicePackMajorVersion; set => _servicePackMajorVersion = value; }
        public ushort ServicePackMinorVersion { get => _servicePackMinorVersion; set => _servicePackMinorVersion = value; }
        public string Version { get => _version; set => _version = value; }
        public ulong FreePhysicalMemory { get => _freePhysicalMemory; set => _freePhysicalMemory = value; }
        public ulong FreeVirtualMemory { get => _freeVirtualMemory; set => _freeVirtualMemory = value; }
        public DateTime InstallDate { get => _installDate; set => _installDate = value; }
        public DateTime LastBootUpTime { get => _lastBootUpTime; set => _lastBootUpTime = value; }

        public TimeSpan Uptime
        {
            get
            {
                return DateTime.Now.Subtract(LastBootUpTime);
            }
        }

        public List<PhysicalMemory> PhysicalMemoryCollection { get => _physicalMemoryCollection; set => _physicalMemoryCollection = value; }
        public CPUInfo CpuInfo { get => _cpuInfo; set => _cpuInfo = value; }
        public string DnsHostName { get => _dnsHostName; set => _dnsHostName = value; }

        /// If the computer is not part of a domain, then the name of the workgroup is returned.
        public string Domain { get => _domain; set => _domain = value; }
        public string DomainRole { get => _domainRole; set => _domainRole = value; }
        public string Manufacturer { get => _manufacturer; set => _manufacturer = value; }
        public string Model { get => _model; set => _model = value; }
        public bool IsPartOfDomain { get => _isPartOfDomain; set => _isPartOfDomain = value; }
        public string Workgroup { get => _workgroup; set => _workgroup = value; }
        #endregion
    }
}
