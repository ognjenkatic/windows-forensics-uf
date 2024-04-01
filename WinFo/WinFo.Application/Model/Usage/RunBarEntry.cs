using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class RunBarEntry
    {
        #region fields
        private string _name;
        private string _value;
        private DateTime _lastWrite;
        #endregion

        #region properties
        public string Name { get => _name; set => _name = value; }
        public string Value { get => _value; set => _value = value; }
        public DateTime LastWrite { get => _lastWrite; set => _lastWrite = value; }
        #endregion
    }
}
