using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the computers BIOS
    /// </summary>
    public class BIOS
    {
        #region fields
        private List<string> _capabilities = new List<string>();
        private string _version;
        private string _buildNumber;
        private string _name;
        private string _currentLanguage;
        private string _manufacturer;
        private bool _isPrimaryBIOS;
        private DateTime _releaseDate;
        private string _serialNumber;
        private string _smBIOSBIOSVersion;
        private UInt16 _smBIOSMajorVersion;
        private UInt16 _smBIOSMinorVersion;

        #endregion

        #region properties
        public List<string> Capabilities { get => _capabilities; set => _capabilities = value; }
        public string Version { get => _version; set => _version = value; }
        public string BuildNumber { get => _buildNumber; set => _buildNumber = value; }
        public string Name { get => _name; set => _name = value; }
        public string CurrentLanguage { get => _currentLanguage; set => _currentLanguage = value; }
        public string Manufacturer { get => _manufacturer; set => _manufacturer = value; }
        public bool IsPrimaryBIOS { get => _isPrimaryBIOS; set => _isPrimaryBIOS = value; }
        public DateTime ReleaseDate { get => _releaseDate; set => _releaseDate = value; }
        public string SerialNumber { get => _serialNumber; set => _serialNumber = value; }
        public string SmBIOSBIOSVersion { get => _smBIOSBIOSVersion; set => _smBIOSBIOSVersion = value; }
        public ushort SmBIOSMajorVersion { get => _smBIOSMajorVersion; set => _smBIOSMajorVersion = value; }
        public ushort SmBIOSMinorVersion { get => _smBIOSMinorVersion; set => _smBIOSMinorVersion = value; }
        public string CapabilitiesPrettyPrint
        {
            get
            {
                StringBuilder caps = new StringBuilder();
                foreach (string str in Capabilities)
                    caps.Append(str+" ,");
                caps[caps.Length - 1] = '.';
                return caps.ToString();
            }
        }
        #endregion
    }
}
