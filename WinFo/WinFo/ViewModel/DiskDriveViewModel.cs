using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model of a disk drive
    /// </summary>
    public class DiskDriveViewModel : BaseViewModel
    {


        private string _name;
        private string _deviceId;
        private string _capabilities;
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
        private string _powerManagementCapabilities;
        private bool _isPowerManagementSupported;
        private string _serialNumber;
        private UInt32 _signature;
        private string _size;
        private string _status;
        private UInt64 _totalCylinders;
        private UInt32 _totalHeads;
        private UInt64 _totalSectors;
        private UInt64 _totalTracks;
        private UInt32 _tracksPerCylinder;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public string DeviceId
        {
            get
            {
                return _deviceId;
            }
            set
            {
                if (_deviceId != value)
                {
                    _deviceId = value;
                    RaisePropertyChanged("DeviceId");
                }
            }
        }
        public string Capabilities
        {
            get
            {
                return _capabilities;
            }
            set
            {
                if(_capabilities != value)
                {
                    _capabilities = value;
                    RaisePropertyChanged("Capabilities");
                }
            }
        }
        public string Availability
        {
            get
            {
                return _availability;
            }
            set
            {
                if (_availability != value)
                {
                    _availability = value;
                    RaisePropertyChanged("Availability");
                }
            }
        }
        public uint BytesPerSector
        {
            get
            {
                return _bytesPerSector;
            }
            set
            {
                if (_bytesPerSector != value)
                {
                    _bytesPerSector = value;
                    RaisePropertyChanged("BytesPerSector");
                }
            }
        }
        public ulong DefaultBlockSize
        {
            get
            {
                return _defaultBlockSize;
            }
            set
            {
                if (_defaultBlockSize != value)
                {
                    _defaultBlockSize = value;
                    RaisePropertyChanged("DefaultBlockSize");
                }
            }
        }
        public ulong MaxBlockSize
        {
            get
            {
                return _maxBlockSize;
            }
            set
            {
                if(_maxBlockSize != value)
                {
                    _maxBlockSize = value;
                    RaisePropertyChanged("MaxBlockSize");
                }
            }
        }
        public ulong MinBlockSize
        {
            get
            {
                return _minBlockSize;
            }
            set
            {
                if(_minBlockSize != value)
                {
                    _minBlockSize = value;
                    RaisePropertyChanged("MinBlockSize");
                }
            }
        }
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                if(_caption != value)
                {
                    _caption = value;
                    RaisePropertyChanged("Caption");
                }
            }
        }
        public string ConfigManagerErrorCode
        {
            get
            {
                return _configManagerErrorCode;
            }
            set
            {
                if (_configManagerErrorCode != value)
                {
                    _configManagerErrorCode = value;
                    RaisePropertyChanged("ConfigManagerErrorCode");
                }
            }
        }
        public string ErrorDescription
        {
            get
            {
                return _errorDescription;
            }
            set
            {
                if(_errorDescription != value)
                {
                    _errorDescription = value;
                }
            }
        }
        public string ErrorMethodology
        {
            get
            {
                return _errorMethodology;
            }
            set
            {

            }
        }
        public string InterfaceType
        {
            get
            {
                return _interfaceType;
            }
            set
            {
                if (_interfaceType != value)
                {
                    _interfaceType = value;
                    RaisePropertyChanged("InterfaceType");
                }
            }
        }
        public string Manufacturer
        {
            get
            {
                return _manufacturer;
            }
            set
            {
                if(_manufacturer != value)
                {
                    _manufacturer = value;
                    RaisePropertyChanged("Manufacturer");
                }
            }
        }
        public string Model
        {
            get
            {
                return _model;
            }
            set
            {
                if(_model != value)
                {
                    _model = value;
                    RaisePropertyChanged("Model");
                }
            }
        }
        public bool IsMediaLoaded
        {
            get
            {
                return _isMediaLoaded;
            }
            set
            {
                if(_isMediaLoaded != value)
                {
                    _isMediaLoaded = value;
                    RaisePropertyChanged("IsMediaLoaded");
                }
            }
        }
        public bool DoesNeedCleaning
        {
            get
            {
                return _doesNeedCleaning;
            }
            set
            {
                if(_doesNeedCleaning != value)
                {
                    _doesNeedCleaning = value;
                    RaisePropertyChanged("DoesNeedCleaning");
                }
            }
        }
        public string MediaType
        {
            get
            {
                return _mediaType;
            }
            set
            {
                if(_mediaType != value)
                {
                    _mediaType = value;
                    RaisePropertyChanged("MediaType");
                }
            }
        }
        public uint PartitionCount
        {
            get
            {
                return _partitionCount;
            }
            set
            {
                if(_partitionCount != value)
                {
                    _partitionCount = value;
                    RaisePropertyChanged("PartitionCount");
                }
            }
        }
        public string PnpDeviceID
        {
            get
            {
                return _pnpDeviceID;
            }
            set
            {
                if(_pnpDeviceID != value)
                {
                    _pnpDeviceID = value;
                    RaisePropertyChanged("PnpDeviceID");
                }
            }
        }
        public String PowerManagementCapabilities
        {
            get
            {
                return _powerManagementCapabilities;
            }
            set
            {
                if(_powerManagementCapabilities != value)
                {
                    _powerManagementCapabilities = value;
                    RaisePropertyChanged("PowerManagementCapabilities");
                }
            }
        }
        public bool IsPowerManagementSupported
        {
            get
            {
                return _isPowerManagementSupported;
            }
            set
            {
                if(_isPowerManagementSupported != value)
                {
                    _isPowerManagementSupported = value;
                    RaisePropertyChanged("IsPowerManagementSupported");
                }
            }
        }
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                if(_serialNumber != value)
                {
                    _serialNumber = value;
                    RaisePropertyChanged("SerialNumber");
                }
            }
        }
        public uint Signature
        {
            get
            {
                return _signature;
            }
            set
            {
                if(_signature != value)
                {
                    _signature = value;
                    RaisePropertyChanged("Signature");
                }
            }
        }
        public string Size
        {
            get
            {
                return _size;
            }
            set
            {
                if(_size != value)
                {
                    _size = value;
                    RaisePropertyChanged("Size");
                }
            }
        }
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if(_status != value)
                {
                    _status = value;
                    RaisePropertyChanged("Status");
                }
            }
        }
        public ulong TotalCylinders
        {
            get
            {
                return _totalCylinders;
            }
            set
            {
                if(_totalCylinders != value)
                {
                    _totalCylinders = value;
                    RaisePropertyChanged("TotalCylinders");
                }
            }
        }
        public uint TotalHeads
        {
            get
            {
                return _totalHeads;
            }
            set
            {
                if(_totalHeads != value)
                {
                    _totalHeads = value;
                    RaisePropertyChanged("TotalHeads");
                }
            }
        }
        public ulong TotalSectors
        {
            get
            {
                return _totalSectors;
            }
            set
            {
                if(_totalSectors != value)
                {
                    _totalSectors = value;
                    RaisePropertyChanged("TotalSectors");
                }
            }
        }
        public ulong TotalTracks
        {
            get
            {
                return _totalTracks;
            }
            set
            {
                if(_totalTracks != value)
                {
                    _totalTracks = value;
                    RaisePropertyChanged("TotalTracks");
                }
            }
        }
        public uint TracksPerCylinder
        {
            get
            {
                return _tracksPerCylinder;
            }
            set
            {
                if(_tracksPerCylinder != value)
                {
                    _tracksPerCylinder = value;
                    RaisePropertyChanged("TracksPerCylinder");
                }
            }
        }

        public DiskDriveViewModel(DiskDrive diskDrive)
        {
            Name = diskDrive.Name;
            DeviceId = diskDrive.DeviceId;

            StringBuilder capabs = new StringBuilder();
            foreach(KeyValuePair<string,string> capability in diskDrive.CapabilityDescriptionDictionary)
            {
                capabs.Append(capability.Key);
                capabs.Append("(");
                capabs.Append(capability.Value);
                capabs.Append(");");
            }

            Capabilities = capabs.ToString();

            Availability = diskDrive.Availability;
            BytesPerSector = diskDrive.BytesPerSector;
            DefaultBlockSize = diskDrive.DefaultBlockSize;
            MaxBlockSize = diskDrive.MaxBlockSize;
            MinBlockSize = diskDrive.MinBlockSize;
            Caption = diskDrive.Caption;
            ConfigManagerErrorCode = diskDrive.ConfigManagerErrorCode;
            ErrorDescription = diskDrive.ErrorDescription;
            ErrorMethodology = diskDrive.ErrorMethodology;
            InterfaceType = diskDrive.InterfaceType;
            Manufacturer = diskDrive.Manufacturer;
            Model = diskDrive.Model;
            IsMediaLoaded = diskDrive.IsMediaLoaded;
            DoesNeedCleaning = diskDrive.DoesNeedCleaning;
            MediaType = diskDrive.MediaType;
            PartitionCount = diskDrive.PartitionCount;
            PnpDeviceID = diskDrive.PnpDeviceID;

            capabs.Clear();

            foreach(string capability in diskDrive.PowerManagementCapabilities)
            {
                capabs.Append(capability);
                capabs.Append(";");
            }

            PowerManagementCapabilities = capabs.ToString();

            IsPowerManagementSupported = diskDrive.IsPowerManagementSupported;
            SerialNumber = diskDrive.SerialNumber;
            Signature = diskDrive.Signature;
            Size = diskDrive.Size / 1024 / 1024 / 1024 + " GB";
            Status = diskDrive.Status;
            TotalCylinders = diskDrive.TotalCylinders;
            TotalHeads = diskDrive.TotalHeads;
            TotalSectors = diskDrive.TotalSectors;
            TotalTracks = diskDrive.TotalTracks;

        }
    }
}
