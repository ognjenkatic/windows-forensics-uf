using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    /// <summary>
    /// Represents a record of interaction with a usb device
    /// </summary>
    public class USBDeviceHistoryEntry
    {
        #region fields
        private string _deviceDescription;
        private string _deviceId;
        private DateTime _lastSeen;
        private string _mountPoint;
        private bool _hasMountPoint;
        private string _deviceName;
        #endregion

        #region properties
        public string DeviceDescription { get => _deviceDescription; set => _deviceDescription = value; }
        public string DeviceId { get => _deviceId; set => _deviceId = value; }
        public DateTime LastSeen { get => _lastSeen; set => _lastSeen = value; }
        public string MountPoint { get => _mountPoint; set => _mountPoint = value; }
        public bool HasMountPoint { get => _hasMountPoint; set => _hasMountPoint = value; }
        public string DeviceName { get => _deviceName; set => _deviceName = value; }
        #endregion

        public override bool Equals(object obj)
        {
            if(obj is USBDeviceHistoryEntry usbd)
            {
                if (usbd.DeviceId != null && usbd.DeviceId.Equals(_deviceId))
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _deviceId.GetHashCode();
        }
    }
}
