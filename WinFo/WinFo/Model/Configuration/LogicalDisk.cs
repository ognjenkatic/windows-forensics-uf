using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the computers logical disks
    /// </summary>
    public class LogicalDisk
    {
        #region fields
        private string _caption;
        private string _deviceId;
        private string _fileSystem;
        private string _type;
        private UInt64 _freeSpace;
        private UInt64 _size;
        private string _volumeName;
        private string _volumeSerialNumber;
        #endregion

        #region properties
        public string Caption { get => _caption; set => _caption = value; }
        public string DeviceId { get => _deviceId; set => _deviceId = value; }
        public string FileSystem { get => _fileSystem; set => _fileSystem = value; }
        public string Type { get => _type; set => _type = value; }
        public ulong FreeSpace { get => _freeSpace; set => _freeSpace = value; }
        public ulong Size { get => _size; set => _size = value; }
        public string VolumeName { get => _volumeName; set => _volumeName = value; }
        public string VolumeSerialNumber { get => _volumeSerialNumber; set => _volumeSerialNumber = value; }
        #endregion
    }
}
