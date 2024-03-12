using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the computers disk drives
    /// </summary>
    public class DiskDrive
    {
        #region fields
        private string _name;
        private string _deviceId;
        private Dictionary<string, string> _capabilityDescriptionDictionary = new Dictionary<string, string>();
        private string _availability;
        private UInt32 _bytesPerSector;
        private UInt64 _defaultBlockSize;
        private UInt64 _maxBlockSize;
        private UInt64 _minBlockSize;
        private string _caption;
        private string _configManagerErrorCode;
        private string _errorDescription;
        private string _errorMethodology;
        private string _interfaceType;
        private string _manufacturer;
        private string _model;
        private bool _isMediaLoaded;
        private bool _doesNeedCleaning;
        private string _mediaType;
        private UInt32 _partitionCount;
        private string _pnpDeviceID;
        private List<string> _powerManagementCapabilities = new List<string>();
        private bool _isPowerManagementSupported;
        private string _serialNumber;
        private UInt32 _signature;
        private UInt64 _size;
        private string _status;
        private UInt64 _totalCylinders;
        private UInt32 _totalHeads;
        private UInt64 _totalSectors;
        private UInt64 _totalTracks;
        private UInt32 _tracksPerCylinder;
        private List<DiskPartition> _diskPartitions = new List<DiskPartition>();
        #endregion

        #region properties
        public string Name { get => _name; set => _name = value; }
        public string DeviceId { get => _deviceId; set => _deviceId = value; }
        public Dictionary<string, string> CapabilityDescriptionDictionary { get => _capabilityDescriptionDictionary; set => _capabilityDescriptionDictionary = value; }
        public string Availability { get => _availability; set => _availability = value; }
        public uint BytesPerSector { get => _bytesPerSector; set => _bytesPerSector = value; }
        public ulong DefaultBlockSize { get => _defaultBlockSize; set => _defaultBlockSize = value; }
        public ulong MaxBlockSize { get => _maxBlockSize; set => _maxBlockSize = value; }
        public ulong MinBlockSize { get => _minBlockSize; set => _minBlockSize = value; }
        public string Caption { get => _caption; set => _caption = value; }
        public string ConfigManagerErrorCode { get => _configManagerErrorCode; set => _configManagerErrorCode = value; }
        public string ErrorDescription { get => _errorDescription; set => _errorDescription = value; }
        public string ErrorMethodology { get => _errorMethodology; set => _errorMethodology = value; }
        public string InterfaceType { get => _interfaceType; set => _interfaceType = value; }
        public string Manufacturer { get => _manufacturer; set => _manufacturer = value; }
        public string Model { get => _model; set => _model = value; }
        public bool IsMediaLoaded { get => _isMediaLoaded; set => _isMediaLoaded = value; }
        public bool DoesNeedCleaning { get => _doesNeedCleaning; set => _doesNeedCleaning = value; }
        public string MediaType { get => _mediaType; set => _mediaType = value; }
        public uint PartitionCount { get => _partitionCount; set => _partitionCount = value; }
        public string PnpDeviceID { get => _pnpDeviceID; set => _pnpDeviceID = value; }
        public List<string> PowerManagementCapabilities { get => _powerManagementCapabilities; set => _powerManagementCapabilities = value; }
        public string SerialNumber { get => _serialNumber; set => _serialNumber = value; }
        public uint Signature { get => _signature; set => _signature = value; }
        public ulong Size { get => _size; set => _size = value; }
        public string Status { get => _status; set => _status = value; }
        public ulong TotalCylinders { get => _totalCylinders; set => _totalCylinders = value; }
        public uint TotalHeads { get => _totalHeads; set => _totalHeads = value; }
        public ulong TotalSectors { get => _totalSectors; set => _totalSectors = value; }
        public ulong TotalTracks { get => _totalTracks; set => _totalTracks = value; }
        public uint TracksPerCylinder { get => _tracksPerCylinder; set => _tracksPerCylinder = value; }
        public bool IsPowerManagementSupported { get => _isPowerManagementSupported; set => _isPowerManagementSupported = value; }
        public List<DiskPartition> DiskPartitions { get => _diskPartitions; set => _diskPartitions = value; }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is DiskDrive drive)
            {
                if (drive.DeviceId.Equals(DeviceId))
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return DeviceId.GetHashCode();
        }
    }
}
