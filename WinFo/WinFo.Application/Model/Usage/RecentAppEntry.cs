using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class RecentAppEntry
    {
        #region fields
        private string _appId;
        private string _appPath;
        private DateTime _lastAccessedTime;
        private int _launchCount;
        private List<RecentAppItemEntry> _recentItems = new List<RecentAppItemEntry>();
        #endregion

        #region properties
        public string AppId { get => _appId; set => _appId = value; }
        public string AppPath { get => _appPath; set => _appPath = value; }
        public DateTime LastAccessedTime { get => _lastAccessedTime; set => _lastAccessedTime = value; }
        public int LaunchCount { get => _launchCount; set => _launchCount = value; }
        public List<RecentAppItemEntry> RecentItems { get => _recentItems; set => _recentItems = value; }
        #endregion
    }
}
