using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class SRUMApplicationResourceUsageDataEntry
    {
        #region fields
        private DateTime _timestamp;
        private string _appName;
        private string _sid;
        private long _backgroundBytesRead;
        private long _backgroundBytesWritten;
        private long _backgroundCycleTime;
        private long _faceTime;
        private long _foregroundBytesRead;
        private long _foregroundBytesWritten;
        private long _foregroundCycleTime;
        private int _backgroundContextSwitches;
        private int _backgroundNumberOfFlushes;
        private int _backgroundNumReadOperations;
        private int _backgroundNumWriteOperations;
        private int _foregroundContextSwitches;
        private int _foregroundNumberOfFlushes;
        private int _foregroundNumReadOperations;
        private int _foregroundNumWriteOperations;

        #endregion
        #region properties
        public DateTime Timestamp { get => _timestamp; set => _timestamp = value; }
        public string AppName { get => _appName; set => _appName = value; }
        public string SID { get => _sid; set => _sid = value; }
        public long BackgroundBytesRead { get => _backgroundBytesRead; set => _backgroundBytesRead = value; }
        public long BackgroundBytesWritten { get => _backgroundBytesWritten; set => _backgroundBytesWritten = value; }
        public long BackgroundCycleTime { get => _backgroundCycleTime; set => _backgroundCycleTime = value; }
        public long FaceTime { get => _faceTime; set => _faceTime = value; }
        public long ForegroundBytesRead { get => _foregroundBytesRead; set => _foregroundBytesRead = value; }
        public long ForegroundBytesWritten { get => _foregroundBytesWritten; set => _foregroundBytesWritten = value; }
        public long ForegroundCycleTime { get => _foregroundCycleTime; set => _foregroundCycleTime = value; }
        public int BackgroundContextSwitches { get => _backgroundContextSwitches; set => _backgroundContextSwitches = value; }
        public int BackgroundNumberOfFlushes { get => _backgroundNumberOfFlushes; set => _backgroundNumberOfFlushes = value; }
        public int BackgroundNumReadOperations { get => _backgroundNumReadOperations; set => _backgroundNumReadOperations = value; }
        public int BackgroundNumWriteOperations { get => _backgroundNumWriteOperations; set => _backgroundNumWriteOperations = value; }
        public int ForegroundContextSwitches { get => _foregroundContextSwitches; set => _foregroundContextSwitches = value; }
        public int ForegroundNumberOfFlushes { get => _foregroundNumberOfFlushes; set => _foregroundNumberOfFlushes = value; }
        public int ForegroundNumReadOperations { get => _foregroundNumReadOperations; set => _foregroundNumReadOperations = value; }
        public int ForegroundNumWriteOperations { get => _foregroundNumWriteOperations; set => _foregroundNumWriteOperations = value; }
        #endregion
    }
}
