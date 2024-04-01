using System;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the physical memory
    /// </summary>
    public class PhysicalMemory
    {
        #region fields
        private UInt64 _capacity;
        private UInt32 _speed;
        private string _bankLabel;
        private string _deviceLocator;
        private string _memoryType;
        private string _partNumber;
        private UInt16 _dataWidth;
        private string _tag;
        #endregion
        #region properties
        public ulong Capacity { get => _capacity; set => _capacity = value; }
        public uint Speed { get => _speed; set => _speed = value; }
        public string BankLabel { get => _bankLabel; set => _bankLabel = value; }
        public string DeviceLocator { get => _deviceLocator; set => _deviceLocator = value; }
        public string MemoryType { get => _memoryType; set => _memoryType = value; }
        public string PartNumber { get => _partNumber; set => _partNumber = value; }
        public string Tag { get => _tag; set => _tag = value; }
        public ushort DataWidth { get => _dataWidth; set => _dataWidth = value; }
        #endregion

    }
}
