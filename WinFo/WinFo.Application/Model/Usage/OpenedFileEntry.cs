using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    public class OpenedFileEntry
    {
        #region fields
        private string _name;
        private string _path;
        private string _shortcut;
        private DateTime _created;
        private DateTime _accessed;
        private bool _exists;
        private string _creator;
        #endregion

        #region properties
        public string Name { get => _name; set => _name = value; }
        public string Path { get => _path; set => _path = value; }
        public DateTime Created { get => _created; set => _created = value; }
        public DateTime Accessed { get => _accessed; set => _accessed = value; }
        public bool Exists { get => _exists; set => _exists = value; }
        public string Creator { get => _creator; set => _creator = value; }
        public string Shortcut { get => _shortcut; set => _shortcut = value; }
        #endregion
    }
}
