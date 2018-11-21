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
        private DateTime _installDate;
        private string _name;
        private string _path;
        private string _type;

        #endregion

        #region properties

        public string Caption { get => _caption; set => _caption = value; }
        public string Description { get => _description; set => _description = value; }
        public DateTime InstallDate { get => _installDate; set => _installDate = value; }
        public string Name { get => _name; set => _name = value; }
        public string Path { get => _path; set => _path = value; }
        public string Type { get => _type; set => _type = value; }
        #endregion
    }
}
