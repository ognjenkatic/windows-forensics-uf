using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class ShimCacheEntry
    {
        #region fields
        private string _path;
        private DateTime _lastModified;
        private int _entryPosition;
        #endregion

        #region properties
        public string Path { get => _path; set => _path = value; }
        public DateTime LastModified { get => _lastModified; set => _lastModified = value; }
        public int EntryPosition { get => _entryPosition; set => _entryPosition = value; }
        #endregion
    }
}
