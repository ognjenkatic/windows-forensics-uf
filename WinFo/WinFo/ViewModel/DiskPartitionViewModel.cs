using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;

namespace WinFo.ViewModel
{
    /// <summary>
    /// View Modem for the disk partition
    /// </summary>
    public class DiskPartitionViewModel : BaseViewModel
    {
        private string _name;
        private string _deviceId;
        private UInt64 _blockSize;
        private bool _isBootable;
        private bool _isBootPartition;
        private string _caption;
        private UInt32 _hiddenSectorCount;
        private UInt32 _index;
        private UInt64 _blockCount;
        private bool _isPrimaryPartition;
        private string _purpose;
        private string _size;
        private UInt64 _startingOffset;
        private string _type;

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

        public ulong BlockSize
        {
            get
            {
                return _blockSize;
            }
            set
            {
                if (_blockSize != value)
                {
                    _blockSize = value;
                    RaisePropertyChanged("BlockSize");
                }
            }
        }

        public bool IsBootable
        {
            get
            {
                return _isBootable;
            }
            set
            {
                if(_isBootable != value)
                {
                    _isBootable = value;
                    RaisePropertyChanged("IsBootable");
                }
            }
        }

        public bool IsBootPartition
        {
            get
            {
                return _isBootPartition;
            }
            set
            {
                if(_isBootPartition != value)
                {
                    _isBootPartition = value;
                    RaisePropertyChanged("IsBootPartition");
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

        public uint HiddenSectorCount
        {
            get
            {
                return _hiddenSectorCount;
            }
            set
            {
                if(_hiddenSectorCount != value)
                {
                    _hiddenSectorCount = value;
                    RaisePropertyChanged("HiddenSectorCount");
                }
            }
        }

        public uint Index
        {
            get
            {
                return _index;
            }
            set
            {
                if(_index != value)
                {
                    _index = value;
                    RaisePropertyChanged("Index");
                }
            }
        }

        public ulong BlockCount
        {
            get
            {
                return _blockCount;
            }
            set
            {
                if(_blockCount != value)
                {
                    _blockCount = value;
                    RaisePropertyChanged("BlockCount");
                }
            }
        }

        public bool IsPrimaryPartition
        {
            get
            {
                return _isPrimaryPartition;
            }
            set
            {
                if(_isPrimaryPartition != value)
                {
                    _isPrimaryPartition = value;
                    RaisePropertyChanged("IsPrimaryPartition");
                }
            }
        }

        public string Purpose
        {
            get
            {
                return _purpose;
            }
            set
            {
                if(_purpose != value)
                {
                    _purpose = value;
                    RaisePropertyChanged("Purpose");
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

        public ulong StartingOffset
        {
            get
            {
                return _startingOffset;
            }
            set
            {
                if(_startingOffset != value)
                {
                    _startingOffset = value;
                    RaisePropertyChanged("StartingOffset");
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

        public DiskPartitionViewModel(DiskPartition diskPartition)
        {
            BlockCount =  diskPartition.BlockCount;
            BlockSize = diskPartition.BlockSize;
            Caption =  diskPartition.Caption;
            DeviceId =  diskPartition.DeviceId;
            HiddenSectorCount = diskPartition.HiddenSectorCount;
            Index = diskPartition.Index;
            IsBootable =  diskPartition.IsBootable;
            IsBootPartition = diskPartition.IsBootPartition;
            IsPrimaryPartition =  diskPartition.IsPrimaryPartition;
            Name =  diskPartition.Name;
            Purpose = diskPartition.Purpose;
            Size = diskPartition.Size/1024/1024/1024 +" GB";
            StartingOffset = diskPartition.StartingOffset;
            Type = diskPartition.Type;
        }
    }
}
