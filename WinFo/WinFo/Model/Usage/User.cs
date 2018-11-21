namespace WinFo.Model.Usage
{
    /// <summary>
    /// Information about a system user
    /// </summary>
    public class User
    {
        #region fields
        private string _userName;
        private string _sid;
        private string _domain;
        #endregion

        #region properties
        public string UserName { get => _userName; set => _userName = value; }
        public string Sid { get => _sid; set => _sid = value; }
        public string Domain { get => _domain; set => _domain = value; }
        #endregion
    }
}
