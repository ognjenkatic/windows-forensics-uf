using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    /// <summary>
    /// Information about a program that was installed on the system using the windows installer
    /// </summary>
    public class InstalledProgram
    {

        #region fields
        private string _name;
        private DateTime _installDate;
        private string _publisher;
        private string _displayVersion;
        private string _installLocation;
        private uint _estimatedSize;
        #endregion

        #region properties
        public string Name { get => _name; set => _name = value; }
        public DateTime InstallDate { get => _installDate; set => _installDate = value; }
        public string Publisher { get => _publisher; set => _publisher = value; }
        public string DisplayVersion { get => _displayVersion; set => _displayVersion = value; }
        public string InstallLocation { get => _installLocation; set => _installLocation = value; }
        public uint EstimatedSize { get => _estimatedSize; set => _estimatedSize = value; }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is InstalledProgram program)
            {
                if (program.Name != null && program.Name == Name)
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}
