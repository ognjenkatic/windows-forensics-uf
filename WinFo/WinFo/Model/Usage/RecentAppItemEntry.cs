using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class RecentAppItemEntry
    {
        #region fields
        private string _displayName;
        private string _path;
        private DateTime _lastAccessedTime;
        #endregion

        #region properties
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public string Path { get => _path; set => _path = value; }
        public DateTime LastAccessedTime { get => _lastAccessedTime; set => _lastAccessedTime = value; }
        #endregion
    }
}
