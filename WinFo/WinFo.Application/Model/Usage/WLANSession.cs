using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    /// <summary>
    /// Representation of a wireless LAN session
    /// </summary>
    public class WLANSession
    {

        #region fields
        private DateTime _connectedTime;
        private DateTime _disconnectedTime;
        private string _networkAdapter;
        private string _connectionMode;
        private string _ssid;
        private string _bssidType;
        private string _bssid;
        private string _type;
        private string _authentication;
        private string _encryption;
        private bool _is8021xEnabled;
        #endregion

        #region properties
        public DateTime ConnectedTime { get => _connectedTime; set => _connectedTime = value; }
        public DateTime DisconnectedTime { get => _disconnectedTime; set => _disconnectedTime = value; }
        public string NetworkAdapter { get => _networkAdapter; set => _networkAdapter = value; }
        public string ConnectionMode { get => _connectionMode; set => _connectionMode = value; }
        public string Ssid { get => _ssid; set => _ssid = value; }
        public string BssidType { get => _bssidType; set => _bssidType = value; }
        public string Bssid { get => _bssid; set => _bssid = value; }
        public string Type { get => _type; set => _type = value; }
        public string Authentication { get => _authentication; set => _authentication = value; }
        public string Encryption { get => _encryption; set => _encryption = value; }
        public bool Is8021xEnabled { get => _is8021xEnabled; set => _is8021xEnabled = value; }
        #endregion
    }
}
