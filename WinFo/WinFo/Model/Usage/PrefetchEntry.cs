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
        private string _executableName;

        #endregion

        #region properties
        public PrefetchFileType PrefetchType { get => _prefetchType; set => _prefetchType = value; }
        public string ExecutableName { get => _executableName; set => _executableName = value; }
        #endregion
    }
}
