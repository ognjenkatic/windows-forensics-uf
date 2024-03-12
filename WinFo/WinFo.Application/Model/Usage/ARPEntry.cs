using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    /// <summary>
    /// Information about an ARP table entry
    /// </summary>
    public class ARPEntry
    {
        #region fields
        private string _internetAddress;
        private string _physicalAddress;
        private UInt32 _interfaceIndex;
        #endregion

        #region properties
        public string InternetAddress { get => _internetAddress; set => _internetAddress = value; }
        public string PhysicalAddress { get => _physicalAddress; set => _physicalAddress = value; }
        public uint InterfaceIndex { get => _interfaceIndex; set => _interfaceIndex = value; }
        #endregion
    }
}
