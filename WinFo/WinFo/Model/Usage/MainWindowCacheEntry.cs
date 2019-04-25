using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class MainWindowCacheEntry
    {
        #region fields
        private string _title;
        private string _path;
        #endregion

        #region properties
        public string Title { get => _title; set => _title = value; }
        public string Path { get => _path; set => _path = value; }
        #endregion
    }
}
