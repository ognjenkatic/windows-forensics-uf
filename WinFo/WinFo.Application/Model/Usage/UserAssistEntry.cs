using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    /// <summary>
    /// Information about launched programs
    /// </summary>
    public class UserAssistEntry
    {
        #region fields
        private string _path;
        private DateTime _lastLaunchTime;
        private int _runCount;
        private int _focusCount;
        private int _focusTimeMillis;
        #endregion

        #region properties
        public string Path { get => _path; set => _path = value; }
        public int RunCount { get => _runCount; set => _runCount = value; }
        public int FocusCount { get => _focusCount; set => _focusCount = value; }
        public int FocusTimeMillis { get => _focusTimeMillis; set => _focusTimeMillis = value; }
        public DateTime LastLaunchTime { get => _lastLaunchTime; set => _lastLaunchTime = value; }
        #endregion
    }
}
