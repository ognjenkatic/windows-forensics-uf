using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class PrefetchFileMetricsEntry
    {
        #region fields
        private int _startTimeMillis;
        private int _durationTimeMillis;
        private int _averageTimeMillis;
        private string _filename;
        #endregion


        #region properties
        public int StartTimeMillis { get => _startTimeMillis; set => _startTimeMillis = value; }
        public int DurationTimeMillis { get => _durationTimeMillis; set => _durationTimeMillis = value; }
        public int AverageTimeMillis { get => _averageTimeMillis; set => _averageTimeMillis = value; }
        public string Filename { get => _filename; set => _filename = value; }
        #endregion

    }
}
