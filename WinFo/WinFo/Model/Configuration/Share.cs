using System;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about a share
    /// </summary>
    public class Share
    {
        #region fields
        private string _caption;
        private string _description;
        private string _status;
        private DateTime _creation;
        private DateTime _lastAccessed;
        private DateTime _lastModified;
        private bool _hidden;
        private string _drive;
        private string _name;
        private string _path;
        private string _type;

        #endregion

        #region properties

        public string Caption { get => _caption; set => _caption = value; }
        public string Description { get => _description; set => _description = value; }
        public string Name { get => _name; set => _name = value; }
        public string Path { get => _path; set => _path = value; }
        public string Type { get => _type; set => _type = value; }
        public string Status { get => _status; set => _status = value; }
        public DateTime Creation { get => _creation; set => _creation = value; }
        public DateTime LastAccessed { get => _lastAccessed; set => _lastAccessed = value; }
        public DateTime LastModified { get => _lastModified; set => _lastModified = value; }
        public bool Hidden { get => _hidden; set => _hidden = value; }
        public string Drive { get => _drive; set => _drive = value; }
        #endregion
    }
}
