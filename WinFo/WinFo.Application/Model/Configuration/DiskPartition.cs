using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the computers disk partitions
    /// </summary>
    public class DiskPartition
    {
        #region fields
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
        private UInt64 _size;
        private UInt64 _startingOffset;
        private string _type;
        private List<LogicalDisk> _logicalDisks = new List<LogicalDisk>();
        #endregion

        #region properties
        public string Name { get => _name; set => _name = value; }
        public ulong BlockSize { get => _blockSize; set => _blockSize = value; }
        public bool IsBootable { get => _isBootable; set => _isBootable = value; }
        public bool IsBootPartition { get => _isBootPartition; set => _isBootPartition = value; }
        public string Caption { get => _caption; set => _caption = value; }
        public uint HiddenSectorCount { get => _hiddenSectorCount; set => _hiddenSectorCount = value; }
        public uint Index { get => _index; set => _index = value; }
        public ulong BlockCount { get => _blockCount; set => _blockCount = value; }
        public bool IsPrimaryPartition { get => _isPrimaryPartition; set => _isPrimaryPartition = value; }
        public string Purpose { get => _purpose; set => _purpose = value; }
        public ulong Size { get => _size; set => _size = value; }
        public ulong StartingOffset { get => _startingOffset; set => _startingOffset = value; }
        public string Type { get => _type; set => _type = value; }
        public List<LogicalDisk> LogicalDisks { get => _logicalDisks; set => _logicalDisks = value; }
        public string DeviceId { get => _deviceId; set => _deviceId = value; }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is DiskPartition part)
            {
                if (part.DeviceId.Equals(DeviceId))
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
