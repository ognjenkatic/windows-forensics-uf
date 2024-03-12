using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public enum PrefetchFileType { MAM, UNKNOWN };

    public class PrefetchEntry
    {
        #region fields
        private PrefetchFileType _prefetchType;
        private string _prefetchFilePath;
        private string _executableName;
        private string[] _filesLoaded;
        private DateTime _latestExecutionTime;
        private DateTime _latestExecutionTime2;
        private DateTime _latestExecutionTime3;
        private DateTime _latestExecutionTime4;
        private DateTime _latestExecutionTime5;
        private DateTime _latestExecutionTime6;
        private DateTime _latestExecutionTime7;
        private DateTime _latestExecutionTime8;
        private int _executionCount;
        private List<PrefetchFileMetricsEntry> _fileMetrics;
        private List<PrefetchVolumeInformationEntry> _volumeInformation;
        #endregion

        #region properties
        public PrefetchFileType PrefetchType { get => _prefetchType; set => _prefetchType = value; }
        public string ExecutableName { get => _executableName; set => _executableName = value; }
        public string[] FilesLoaded { get => _filesLoaded; set => _filesLoaded = value; }
        public DateTime LatestExecutionTime { get => _latestExecutionTime; set => _latestExecutionTime = value; }
        public DateTime LatestExecutionTime2 { get => _latestExecutionTime2; set => _latestExecutionTime2 = value; }
        public DateTime LatestExecutionTime3 { get => _latestExecutionTime3; set => _latestExecutionTime3 = value; }
        public DateTime LatestExecutionTime4 { get => _latestExecutionTime4; set => _latestExecutionTime4 = value; }
        public DateTime LatestExecutionTime5 { get => _latestExecutionTime5; set => _latestExecutionTime5 = value; }
        public DateTime LatestExecutionTime6 { get => _latestExecutionTime6; set => _latestExecutionTime6 = value; }
        public DateTime LatestExecutionTime7 { get => _latestExecutionTime7; set => _latestExecutionTime7 = value; }
        public DateTime LatestExecutionTime8 { get => _latestExecutionTime8; set => _latestExecutionTime8 = value; }
        public int ExecutionCount { get => _executionCount; set => _executionCount = value; }
        public string PrefetchFilePath { get => _prefetchFilePath; set => _prefetchFilePath = value; }
        public List<PrefetchFileMetricsEntry> FileMetrics { get => _fileMetrics; set => _fileMetrics = value; }
        public List<PrefetchVolumeInformationEntry> VolumeInformation { get => _volumeInformation; set => _volumeInformation = value; }
        #endregion
    }
}
