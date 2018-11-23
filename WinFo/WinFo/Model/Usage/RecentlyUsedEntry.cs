using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    /// <summary>
    /// Represents a resource that was recently used
    /// </summary>
    public class RecentlyUsedEntry
    {
        #region fields
        private string _name;
        private string _value;
        private DateTime _created;
        private DateTime _accessed;
        #endregion

        #region properties
        public string Name { get => _name; set => _name = value; }
        public string Value { get => _value; set => _value = value; }
        public DateTime Created { get => _created; set => _created = value; }
        public DateTime Accessed { get => _accessed; set => _accessed = value; }
        #endregion
    }
}
