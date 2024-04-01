using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;

namespace WinFo.Model.Usage
{
    public class SRUMNetworkConnectivityEntry
    {
        #region fields
        private int _interfaceIndex;
        private DateTime _timestamp;
        private int _connectedTime;
        private DateTime _conectedStartTime;
        private int _l2ProfileId;
        #endregion

        #region properties
        public DateTime Timestamp { get => _timestamp; set => _timestamp = value; }
        public int ConnectedTime { get => _connectedTime; set => _connectedTime = value; }
        public DateTime ConectedStartTime { get => _conectedStartTime; set => _conectedStartTime = value; }
        public int InterfaceIndex { get => _interfaceIndex; set => _interfaceIndex = value; }
        public int L2ProfileId { get => _l2ProfileId; set => _l2ProfileId = value; }
        #endregion


    }
}
