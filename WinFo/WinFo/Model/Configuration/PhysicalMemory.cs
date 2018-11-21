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
        #endregion
        #region properties
        public ulong Capacity { get => _capacity; set => _capacity = value; }
        public uint Speed { get => _speed; set => _speed = value; }
        #endregion

    }
}
