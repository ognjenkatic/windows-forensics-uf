using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the system updates applied to the computer
    /// </summary>
    public class Update
    {
        #region fields

        private string _title;
        private DateTime _time;
        private string _source;
        #endregion

        #region properties
        public string Title { get => _title; set => _title = value; }
        public DateTime Time { get => _time; set => _time = value; }
        public string Source { get => _source; set => _source = value; }
        #endregion
    }
}
