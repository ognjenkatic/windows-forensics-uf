using System;
using System.Collections.Generic;
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

        private static string _COMPUTER_SYSTEM_INFORMATION_SEARCH_STRING = "SELECT * FROM Win32_Processor";

        private static string _PHYSICAL_MEMORY_SEARCH_STRING = "SELECT Capacity, Speed FROM Win32_PhysicalMemory";

        private static string _COMPUTER_SYSTEM_SEARCH_STRING = "SELECT DNSHostName, Domain, DomainRole, Manufacturer, Model, PartOfDomain, Workgroup FROM Win32_ComputerSystem";

        private static string _DISK_DRIVE_TO_PARTITION_SEARCH_STRING = "SELECT Antecedent, Dependent FROM Win32_DiskDriveToDiskPartition";

        private static string _LOGICAL_DISK_TO_PARTITION_SEARCH_STRING = "SELECT * FROM Win32_LogicalDiskToPartition";

        private static Dictionary<UInt32, string> _DRIVE_TYPE_DICTIONARY = new Dictionary<UInt32, string>()
        {
            { 0 , "Unknown"},
            { 1 , "No Root Directory" },
            { 2 , "Removable Disk" },
            { 3 , "Local Disk" },
            { 4 , "Network Drive" },
            { 5 , "Compact Disc" },
            { 6 , "RAM Disk" }
        };
        private static Dictionary<UInt32, string> _DISK_CONFIG_MANAGER_ERROR_CODE_DICTIONARY = new Dictionary<uint, string>()
        {
            { 0 , "The device is working properly" },
            { 1 , "The device is not configured correctly" },
            { 2 , "WIndows cannot load the driver for this device" },
            { 3 , "The driver for this device might be corrupted, or your system may be running low on memory or other resources" },
            { 4 , "This device is not working properly. One of its drivers or your registry might be corrupted" },
            { 5 , "The driver for this device needs a resource that Windows cannot manage" },
            { 6 , "The boot configuration for this device conflicts with other devices" },
            { 7 , "Cannot filter" },
            { 8 , "The driver loader for the device is missing" },
            { 9 , "This device is not working properly because the controlling firmware is reporting the resources for the device incorrectly" },
            { 10, "This device cannot start" },
            { 11, "This device failed" },
            { 12, "This device cannot find enough free resources that it can use" },
            { 13, "Windows cannot verify this device's resources" },
            { 14, "This device cannot work properly until you restart your computer" },
            { 15, "This device is not working properly because there is probably a re-enumeration problem" },
            { 16, "Windows cannot identify all the resources this device uses" },
            { 17, "This device is asking for an unknown resource type" },
            { 18, "Reinstall the drivers for this device" },
            { 19, "Failure using the VxD loader" },
            { 20, "Your registry might be corrupted" },
            { 21, "System failure: Try changing the driver for this device. If that does not work, see your hardware documentation. Windows is removing this device" },
            { 22, "This device is disabled" },
            { 23, "System failure: Try changing the driver for this device. If that doesn't work, see your hardware documentation" },
            { 24, "This device is not present, is not working properly, or does not have all its drivers installed" },
            { 25, "Windows is still setting up this device" },
            { 26, "Windows is still setting up this device" },
            { 27, "This device does not have valid log configuration" },
            { 28, "The drivers for this device are not installed" },
            { 29, "This device is disabled because the firmware of the device did not give it the required resources" },
            { 30, "This device is using an Interrupt Request (IRQ) resource that another device is using" },
            { 31, "This device is not working properly because Windows cannot load the drivers required for this device" }
        };

        // TO-DO make all lookups dictionary based
        private static Dictionary<UInt16, string> _DISK_CAPABILITY_DICTIONARY = new Dictionary<ushort, string>()
        {
            { 0 , "Unknown" },
            { 1 , "Other" },
            { 2 , "Sequential Access" },
            { 3 , "Random Access" },
            { 4 , "Supports Writing" },
            { 5 , "Encryption" },
            { 6 , "Compression" },
            { 7 , "Supports Removable Media" },
            { 8 , "Manual Cleaning" },
            { 9 , "Automatic Cleaning" },
            { 10, "SMART Notification" },
            { 11, "Supports Dual Sided Media" },
            { 12, "Predismount Eject Not Required" }
        };

        private static Dictionary<UInt16, string> _DISK_POWER_CAPABILITIES_DICTIONARY = new Dictionary<ushort, string>()
        {
            { 0 , "Unknown" },
            { 1 , "Not Supported" },
            { 2 , "Disabled" },
            { 3 , "Enabled" },
            { 4 , "Power Saving Modes Entered Automatically" },
            { 5 , "Power State Settable" },
            { 6 , "Power Cycling Supported" },
            { 7 , "Timed Power On Supported" }
        };

        private static Dictionary<UInt16, string> _DISK_AVAILABILITY_DICTIONARY = new Dictionary<ushort, string>()
        {
            { 1 , "Other" },
            { 2 , "Unknown " },
            { 3 , "Running/Full Power" },
            { 4 , "Warning" },
            { 5 , "In Test" },
            { 6 , "Not applicable" },
            { 7 , "Power Off" },
            { 8 , "Off Line" },
            { 9 , "Off Duty" },
            { 10, "Degraded" },
            { 11, "Not Installed" },
            { 12, "Install Error" },
            { 13, "Power Save - Unknown" },
            { 14, "Power Save - Low Power Mode" },
            { 15, "Power Save - Standby" },
            { 16, "Power Cycle" },
            { 17, "Power Save - Warning" },
            { 18, "Paused" },
            { 19, "Not Ready" },
            { 20, "Not Configured" },
            { 21, "Quiesced" }
        };

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
                    //convert to bytes from kilobytes
                    computerSystem.FreePhysicalMemory       = Convert.ToUInt64(mo["FreePhysicalMemory"])*1024;
                    computerSystem.FreeVirtualMemory        = Convert.ToUInt64(mo["FreeVirtualMemory"])*1024;

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

                    cpui.NumberOfCores                      = Convert.ToInt32(mo["NumberOfCores"]);
                    cpui.NumberOfLogicalProcessors          = Convert.ToInt32(mo["NumberOfLogicalProcessors"]);
                    cpui.AddressWidth                       = Convert.ToUInt16(mo["AddressWidth"]);
                    cpui.Caption                            = Convert.ToString(mo["Caption"]);
                    cpui.CurrentClockSpeed                  = Convert.ToUInt32(mo["CurrentClockSpeed"]);
                    cpui.DeviceId                           = Convert.ToString(mo["DeviceId"]);
                    cpui.ExtClock                           = Convert.ToUInt32(mo["ExtClock"]);
                    cpui.L2CacheSize                        = Convert.ToUInt32(mo["L2CacheSize"]);
                    cpui.L2CacheSpeed                       = Convert.ToUInt32(mo["L2CacheSpeed"]);
                    cpui.L3CacheSize                        = Convert.ToUInt32(mo["L3CacheSize"]);
                    cpui.L3CacheSpeed                       = Convert.ToUInt32(mo["L3CacheSpeed"]);
                    cpui.Manufacturer                       = Convert.ToString(mo["Manufacturer"]);
                    cpui.Name                               = Convert.ToString(mo["Name"]);
                    cpui.ProcessorId                        = Convert.ToString(mo["ProcessorId"]);
                    cpui.SocketDesignation                  = Convert.ToString(mo["SocketDesignation"]);
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

                mos.Query.QueryString = _DISK_DRIVE_TO_PARTITION_SEARCH_STRING;
                moc = mos.Get();

                foreach(ManagementObject mo in moc)
                {
                    ManagementPath drivePath =
                        new ManagementPath(mo["Antecedent"].ToString());
                    ManagementPath partPath =
                        new ManagementPath(mo["Dependent"].ToString());

                    if (partPath.ClassName == "Win32_DiskPartition" && drivePath.ClassName == "Win32_DiskDrive")
                    {
                        ManagementObject pa = new ManagementObject(partPath);
                        ManagementObject dr = new ManagementObject(drivePath);

                        DiskDrive dd = new DiskDrive();
                        dd.DeviceId = Convert.ToString(dr["DeviceID"]);

                        if (!computerSystem.DiskDriveCollection.Contains(dd))
                        {
                            dd.Availability = Convert.ToString(dr["Availability"]);
                            dd.BytesPerSector = Convert.ToUInt32(dr["BytesPerSector"]);

                            UInt16[] capabilities = (UInt16[])dr["Capabilities"];
                            string[] capabilityDescriptions = (string[])dr["CapabilityDescriptions"];

                            for (int i = 0; i < capabilities.Length; i++)
                            {
                                if (_DISK_CAPABILITY_DICTIONARY.ContainsKey(capabilities[i]))
                                    dd.CapabilityDescriptionDictionary.Add(_DISK_CAPABILITY_DICTIONARY[capabilities[i]], capabilityDescriptions[i]);

                            }

                            dd.Caption = Convert.ToString(dr["Caption"]);

                            UInt32 configManagerErrorCode = Convert.ToUInt32(dr["ConfigManagerErrorCode"]);

                            if (_DISK_CONFIG_MANAGER_ERROR_CODE_DICTIONARY.ContainsKey(configManagerErrorCode))
                                dd.ConfigManagerErrorCode = _DISK_CONFIG_MANAGER_ERROR_CODE_DICTIONARY[configManagerErrorCode];

                            dd.DefaultBlockSize = Convert.ToUInt64(dr["DefaultBlockSize"]);
                            dd.DoesNeedCleaning = Convert.ToBoolean(dr["NeedsCleaning"]);
                            dd.ErrorDescription = Convert.ToString(dr["ErrorDescription"]);
                            dd.ErrorMethodology = Convert.ToString(dr["ErrorMethodology"]);
                            dd.InterfaceType = Convert.ToString(dr["InterfaceType"]);
                            dd.IsMediaLoaded = Convert.ToBoolean(dr["MediaLoaded"]);
                            dd.Manufacturer = Convert.ToString(dr["Manufacturer"]);
                            dd.MaxBlockSize = Convert.ToUInt64(dr["MaxBlockSize"]);
                            dd.MediaType = Convert.ToString(dr["MediaType"]);
                            dd.MinBlockSize = Convert.ToUInt64(dr["MinBlockSize"]);
                            dd.Model = Convert.ToString(dr["Model"]);
                            dd.Name = Convert.ToString(dr["Name"]);
                            dd.PartitionCount = Convert.ToUInt32(dr["Partitions"]);
                            dd.PnpDeviceID = Convert.ToString(dr["PNPDeviceID"]);

                            dd.IsPowerManagementSupported = Convert.ToBoolean(dr["PowerManagementSupported"]);
                            if (dd.IsPowerManagementSupported)
                            {
                                UInt16[] powerManagementCapabilities = (UInt16[])dr["PowerManagementCapabilities"];
                                foreach (UInt16 capability in powerManagementCapabilities)
                                {
                                    if (_DISK_POWER_CAPABILITIES_DICTIONARY.ContainsKey(capability))
                                        dd.PowerManagementCapabilities.Add(_DISK_POWER_CAPABILITIES_DICTIONARY[capability]);

                                }
                            }

                            dd.SerialNumber = Convert.ToString(dr["SerialNumber"]);
                            dd.Signature = Convert.ToUInt32(dr["Signature"]);
                            dd.Size = Convert.ToUInt64(dr["Size"]);
                            dd.Status = Convert.ToString(dr["Status"]);
                            dd.TotalCylinders = Convert.ToUInt64(dr["TotalCylinders"]);
                            dd.TotalHeads = Convert.ToUInt32(dr["TotalHeads"]);
                            dd.TotalSectors = Convert.ToUInt64(dr["TotalSectors"]);
                            dd.TotalTracks = Convert.ToUInt64(dr["TotalTracks"]);
                            dd.TracksPerCylinder = Convert.ToUInt32(dr["TracksPerCylinder"]);

                            computerSystem.DiskDriveCollection.Add(dd);
                        }

                        DiskPartition dp = new DiskPartition();
                        dp.BlockCount = Convert.ToUInt64(pa["NumberOfBlocks"]);
                        dp.BlockSize = Convert.ToUInt64(pa["BlockSize"]);
                        dp.Caption = Convert.ToString(pa["Caption"]);
                        dp.HiddenSectorCount = Convert.ToUInt32(pa["HiddenSectors"]);
                        dp.Index = Convert.ToUInt32(pa["Index"]);
                        dp.IsBootable = Convert.ToBoolean(pa["Bootable"]);
                        dp.IsBootPartition = Convert.ToBoolean(pa["BootPartition"]);
                        dp.Purpose = Convert.ToString(pa["Purpose"]);
                        dp.Size = Convert.ToUInt64(pa["Size"]);
                        dp.StartingOffset = Convert.ToUInt64(pa["StartingOffset"]);
                        dp.Type = Convert.ToString(pa["Type"]);
                        dp.DeviceId = Convert.ToString(pa["DeviceID"]);
                        dp.Name = Convert.ToString(pa["Name"]);

                        foreach(DiskDrive drive in computerSystem.DiskDriveCollection)
                        {
                            if (drive.DeviceId.Equals(dd.DeviceId))
                                drive.DiskPartitions.Add(dp);
                        }
                    }
                }

                mos.Query.QueryString = _LOGICAL_DISK_TO_PARTITION_SEARCH_STRING;
                moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    ManagementPath partPath =
                        new ManagementPath(mo["Antecedent"].ToString());
                    ManagementPath logicalDiskPath =
                        new ManagementPath(mo["Dependent"].ToString());

                    if (partPath.ClassName == "Win32_DiskPartition" && logicalDiskPath.ClassName == "Win32_LogicalDisk")
                    {
                        ManagementObject ppmo = new ManagementObject(partPath);
                        ManagementObject ldmo = new ManagementObject(logicalDiskPath);

                        string partitionId = Convert.ToString(ppmo["DeviceID"]);

                        foreach (DiskDrive drive in computerSystem.DiskDriveCollection)
                        {
                            foreach(DiskPartition partition in drive.DiskPartitions)
                            {
                                if (partition.DeviceId.Equals(partitionId)){

                                    LogicalDisk ld = new LogicalDisk();
                                    ld.Caption = Convert.ToString(ldmo["Caption"]);
                                    ld.DeviceId = Convert.ToString(ldmo["DeviceID"]);
                                    ld.FileSystem = Convert.ToString(ldmo["FileSystem"]);
                                    ld.FreeSpace = Convert.ToUInt64(ldmo["FreeSpace"]);
                                    ld.Size = Convert.ToUInt64(ldmo["Size"]);
                                    UInt32 driveTypeId = Convert.ToUInt32(ldmo["DriveType"]);

                                    if (_DRIVE_TYPE_DICTIONARY.ContainsKey(driveTypeId))
                                        ld.Type = _DRIVE_TYPE_DICTIONARY[driveTypeId];

                                    ld.VolumeName = Convert.ToString(ldmo["VolumeName"]);
                                    ld.VolumeSerialNumber = Convert.ToString(ldmo["VolumeSerialNumber"]);

                                    partition.LogicalDisks.Add(ld);
                                }
                            }
                        }
                    }
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
