using System;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the CPU architecture
    /// </summary>
    public class CPUInfo
    {
        #region fields
        private UInt16 _addressWidth;
        private string _caption;
        private UInt32 _currentClockSpeed;
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

        private int _numberOfCores;
        private int _numberOfLogicalProcessors;
        private string _architecture;
        #endregion

        #region properties
        public int NumberOfCores { get => _numberOfCores; set => _numberOfCores = value; }
        public int NumberOfLogicalProcessors { get => _numberOfLogicalProcessors; set => _numberOfLogicalProcessors = value; }
        public string Architecture { get => _architecture; set => _architecture = value; }
        public ushort AddressWidth { get => _addressWidth; set => _addressWidth = value; }
        public string Caption { get => _caption; set => _caption = value; }
        public uint CurrentClockSpeed { get => _currentClockSpeed; set => _currentClockSpeed = value; }
        public string DeviceId { get => _deviceId; set => _deviceId = value; }
        public uint ExtClock { get => _extClock; set => _extClock = value; }
        public uint L2CacheSize { get => _l2CacheSize; set => _l2CacheSize = value; }
        public uint L2CacheSpeed { get => _l2CacheSpeed; set => _l2CacheSpeed = value; }
        public uint L3CacheSize { get => _l3CacheSize; set => _l3CacheSize = value; }
        public uint L3CacheSpeed { get => _l3CacheSpeed; set => _l3CacheSpeed = value; }
        public string Manufacturer { get => _manufacturer; set => _manufacturer = value; }
        public string Name { get => _name; set => _name = value; }
        public string ProcessorId { get => _processorId; set => _processorId = value; }
        public string SocketDesignation { get => _socketDesignation; set => _socketDesignation = value; }
        #endregion
    }
}
