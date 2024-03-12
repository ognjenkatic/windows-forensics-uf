using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class PrefetchVolumeInformationEntry
    {
        #region fields
        private string _serialNumber;
        private DateTime _creationTime;
        private string[] _directoriesLoaded;
        #endregion

        #region properties
        public string SerialNumber { get => _serialNumber; set => _serialNumber = value; }
        public DateTime CreationTime { get => _creationTime; set => _creationTime = value; }
        public string[] DirectoriesLoaded { get => _directoriesLoaded; set => _directoriesLoaded = value; }
        #endregion

    }
}
