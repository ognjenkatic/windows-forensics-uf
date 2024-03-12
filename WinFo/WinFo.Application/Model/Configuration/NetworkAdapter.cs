using System;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the network adapter
    /// </summary>
    public class NetworkAdapter
    {
        #region fields
        private string _caption;
        private string[] _defaultIPGateway;
        private bool _dhcpEnabled;
        private DateTime _dhcpLeaseObtained;
        private DateTime _dhcpLeaseExpires;
        private string _dhcpServer;
        private string _dnsDomain;
        private string _dnsHostName;
        private UInt32 _index;
        private string[] _ipAddress;
        private bool _ipEnabled;
        private string _macAddress;
        private UInt32 _mtu;
        private string _serviceName;
        #endregion

        #region properties
        public string Caption { get => _caption; set => _caption = value; }
        public string[] DefaultIPGateway { get => _defaultIPGateway; set => _defaultIPGateway = value; }
        public bool DhcpEnabled { get => _dhcpEnabled; set => _dhcpEnabled = value; }
        public DateTime DhcpLeaseObtained { get => _dhcpLeaseObtained; set => _dhcpLeaseObtained = value; }
        public DateTime DhcpLeaseExpires { get => _dhcpLeaseExpires; set => _dhcpLeaseExpires = value; }
        public string DhcpServer { get => _dhcpServer; set => _dhcpServer = value; }
        public string DnsDomain { get => _dnsDomain; set => _dnsDomain = value; }
        public string DnsHostName { get => _dnsHostName; set => _dnsHostName = value; }
        public uint Index { get => _index; set => _index = value; }
        public string[] IpAddress { get => _ipAddress; set => _ipAddress = value; }
        public bool IpEnabled { get => _ipEnabled; set => _ipEnabled = value; }
        public string MacAddress { get => _macAddress; set => _macAddress = value; }
        public uint Mtu { get => _mtu; set => _mtu = value; }
        public string ServiceName { get => _serviceName; set => _serviceName = value; }
        #endregion
    }
}
