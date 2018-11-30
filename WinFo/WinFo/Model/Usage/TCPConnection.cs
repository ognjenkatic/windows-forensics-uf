using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public enum TCPConnectionState
    {
        INVALID,
        CLOSED,
        LISTEN,
        SYN_SENT,
        SYN_RECEIVED,
        ESTABLISHED,
        FIN_WAIT1,
        FIN_WAIT2,
        CLOSE_WAIT,
        CLOSING,
        LAST_ACK,
        TIME_WAIT,
        DELETE_TCB
    }
    public class TCPConnection
    {
        private int _localPort;
        private int _remotePort;
        private string _localIP;
        private string _remoteIP;
        private TCPConnectionState _state;

        public int LocalPort { get => _localPort; set => _localPort = value; }
        public int RemotePort { get => _remotePort; set => _remotePort = value; }
        public string LocalIP { get => _localIP; set => _localIP = value; }
        public string RemoteIP { get => _remoteIP; set => _remoteIP = value; }
        public TCPConnectionState State { get => _state; set => _state = value; }
    }
}
