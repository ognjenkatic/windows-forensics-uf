using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;

namespace WinFo.ViewModel
{
    /// <summary>
    /// View model for the CPU info
    /// </summary>
    public class CPUInfoViewModel : BaseViewModel
    {
        private string _architecture;
        private string _numberOfLogicalProcessors;
        private string _numberOfCores;
        private UInt16 _addressWidth;
        private string _caption;
        private UInt32 _currentCLockSpeed;
        private string _deviceId;
        private UInt32 _extClock;
        private UInt32 _l2CacheSize;
        private UInt32 _l2CacheSpeed;
        private UInt32 _l3CacheSize;
        private UInt32 _l3CacheSpeed;
        private string _manufacturer;
        private string _name;
        private string _processorId;
        private string _socketDesignation;

        public string Architecture
        {
            get
            {
                return _architecture;
            }
            set
            {
                if(_architecture != value)
                {
                    _architecture = value;
                    RaisePropertyChanged("Architecture");
                }
            }
        }     
        public string NumberOfLogicalProcessors
        {
            get
            {
                return _numberOfLogicalProcessors;
            }
            set
            {
                if(_numberOfLogicalProcessors != value)
                {
                    _numberOfLogicalProcessors = value;
                    RaisePropertyChanged("NumberOfLogicalProcessors");
                }
            }
        }
        public string NumberOfCores
        {
            get
            {
                return _numberOfCores;
            }
            set
            {
                if(_numberOfCores != value)
                {
                    _numberOfCores = value;
                    RaisePropertyChanged("NumberOfPhysicalProcessors");
                }
            }
        }
        public ushort AddressWidth
        {
            get
            {
                return _addressWidth;
            }
            set
            {
                if(_addressWidth != value)
                {
                    _addressWidth = value;
                    RaisePropertyChanged("AddressWidth");
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
                if (_caption != value)
                {
                    _caption = value;
                    RaisePropertyChanged("Caption");
                }
            }
        }
        public uint CurrentClockSpeed
        {
            get
            {
                return _currentCLockSpeed;
            }
            set
            {
                if(_currentCLockSpeed != value)
                {
                    _currentCLockSpeed = value;
                    RaisePropertyChanged("CurrentClockSpeed");
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
                if(_deviceId != value)
                {
                    _deviceId = value;
                    RaisePropertyChanged("DeviceId");
                }
            }
        }
        public uint ExtClock
        {
            get
            {
                return _extClock;
            }
            set
            {
                if(_extClock != value)
                {
                    _extClock = value;
                    RaisePropertyChanged("ExtClock");
                }
            }
        }
        public uint L2CacheSize
        {
            get
            {
                return _l2CacheSize;
            }
            set
            {
                if(_l2CacheSize != value)
                {
                    _l2CacheSize = value;
                    RaisePropertyChanged("L2CacheSize");
                }
            }
        }
        public uint L2CacheSpeed
        {
            get
            {
                return _l2CacheSpeed;
            }
            set
            {
                if(_l2CacheSpeed != value)
                {
                    _l2CacheSpeed = value;
                    RaisePropertyChanged("L2CacheSpeed");
                }
            }
        }
        public uint L3CacheSize
        {
            get
            {
                return _l3CacheSize;
            }
            set
            {
                if(_l3CacheSize!= value)
                {
                    _l3CacheSize = value;
                    RaisePropertyChanged("L3CacheSize");
                }
            }
        }
        public uint L3CacheSpeed
        {
            get
            {
                return _l3CacheSpeed;
            }
            set
            {
                if(_l3CacheSpeed != value)
                {
                    _l3CacheSpeed = value;
                    RaisePropertyChanged("L3CacheSpeed");
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
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(_name != value)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        public string ProcessorId
        {
            get
            {
                return _processorId;
            }
            set
            {
                if(_processorId != value)
                {
                    _processorId = value;
                    RaisePropertyChanged("ProcessorId");
                }
            }
        }
        public string SocketDesignation
        {
            get
            {
                return _socketDesignation;
            }
            set
            {
                if(_socketDesignation != value)
                {
                    _socketDesignation = value;
                    RaisePropertyChanged("SocketDesignation");
                }
            }
        }

        public CPUInfoViewModel(CPUInfo cpuInfo)
        {
            Architecture = cpuInfo.Architecture;
            NumberOfLogicalProcessors = cpuInfo.NumberOfLogicalProcessors.ToString();
            NumberOfCores = cpuInfo.NumberOfCores.ToString();
            AddressWidth = cpuInfo.AddressWidth;
            Caption = cpuInfo.Caption;
            CurrentClockSpeed = cpuInfo.CurrentClockSpeed;
            DeviceId = cpuInfo.DeviceId;
            ExtClock = cpuInfo.ExtClock;
            L2CacheSize = cpuInfo.L2CacheSize;
            L2CacheSpeed = cpuInfo.L2CacheSpeed;
            L3CacheSize = cpuInfo.L3CacheSize;
            L3CacheSpeed = cpuInfo.L3CacheSpeed;
            Manufacturer = cpuInfo.Manufacturer;
            Name = cpuInfo.Name;
            ProcessorId = cpuInfo.ProcessorId;
            SocketDesignation = cpuInfo.SocketDesignation;
        }
    }
}
