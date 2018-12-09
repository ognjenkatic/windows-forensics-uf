using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model of a logical disk
    /// </summary>
    public class LogicalDiskViewModel : BaseViewModel
    {
        private string _caption;
        private string _deviceId;
        private string _fileSystem;
        private string _type;
        private string _freeSpace;
        private string _size;
        private string _volumeName;
        private string _volumeSerialNumber;
        private ulong _freeSpaceInt;
        private ulong _takenSpaceInt;
        private ulong _sizeInt;
        private string _memoryAllocation;

        public ulong SizeInt
        {
            get
            {
                return _sizeInt;
            }
            set
            {
                if(_sizeInt!= value)
                {
                    _sizeInt = value;
                    RaisePropertyChanged("SizeInt");
                }
            }
        }
        public ulong FreeSpaceInt
        {
            get
            {
                return _freeSpaceInt;
            }
            set
            {
                if(_freeSpaceInt != value)
                {
                    _freeSpaceInt = value;
                    RaisePropertyChanged("FreeSpaceInt");
                }
            }
        }

        public ulong TakenSpaceInt
        {
            get
            {
                return _takenSpaceInt;
            }
            set
            {
                if(_takenSpaceInt != value)
                {
                    _takenSpaceInt = value;
                    RaisePropertyChanged("TakenSpaceInt");
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

        public string FileSystem
        {
            get
            {
                return _fileSystem;
            }
            set
            {
                if(_fileSystem != value)
                {
                    _fileSystem = value;
                    RaisePropertyChanged("FileSystem");
                }
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                if(_type != value)
                {
                    _type = value;
                    RaisePropertyChanged("Type");
                }
            }
        }

        public string FreeSpace
        {
            get
            {
                return _freeSpace;
            }
            set
            {
                if(_freeSpace != value)
                {
                    _freeSpace = value;
                    RaisePropertyChanged("FreeSpace");
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

        public string VolumeName
        {
            get
            {
                return _volumeName;
            }
            set
            {
                if(_volumeName != value)
                {
                    _volumeName = value;
                    RaisePropertyChanged("VolumeName");
                }
            }
        }

        public string VolumeSerialNumber
        {
            get
            {
                return _volumeSerialNumber;
            }
            set
            {
                if(_volumeSerialNumber != value)
                {
                    _volumeSerialNumber = value;
                    RaisePropertyChanged("VolumeSerialNumber");
                }
            }
        }

        public string MemoryAllocation
        {
            get
            {
                return _memoryAllocation;
            }
            set
            {
                if(_memoryAllocation != value)
                {
                    _memoryAllocation = value;
                    RaisePropertyChanged("MemoryAllocation");
                }
            }
        }
        public LogicalDiskViewModel(LogicalDisk logicalDisk)
        {
            Caption = logicalDisk.VolumeName+"("+ logicalDisk.Caption+")";
            DeviceId = logicalDisk.DeviceId;
            FileSystem = logicalDisk.FileSystem;
            Type = logicalDisk.Type;
            FreeSpace = logicalDisk.FreeSpace / 1024 / 1024 / 1024 + " GB";
            Size = logicalDisk.Size / 1024 / 1024 / 1024 + " GB";
            VolumeName = logicalDisk.VolumeName;
            VolumeSerialNumber = logicalDisk.VolumeSerialNumber;
            FreeSpaceInt = logicalDisk.FreeSpace;
            TakenSpaceInt = logicalDisk.Size - logicalDisk.FreeSpace;
            SizeInt = logicalDisk.Size;
            MemoryAllocation = (logicalDisk.Size - logicalDisk.FreeSpace) / 1024/1024/1024 + " / "+logicalDisk.Size / 1024/1024/1024 + " GigaBytes";
        }
    }
}
