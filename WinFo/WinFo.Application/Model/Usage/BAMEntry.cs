using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class BAMEntry
    {
        #region fields
        private string _appName;
        private string _appPath;
        private DateTime _latestExecutionTime;
        private string _sid;
        #endregion

        #region properties
        public string AppName { get => _appName; set => _appName = value; }
        public string AppPath { get => _appPath; set => _appPath = value; }
        public DateTime LatestExecutionTime { get => _latestExecutionTime; set => _latestExecutionTime = value; }
        public string SID { get => _sid; set => _sid = value; }
        #endregion
    }
}
