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
        private bool _isDisabled;
        private bool _isLocalAccount;
        private bool _isLockedOut;
        private string _fullName;
        private bool _passwordChangeable;
        private bool _passwordExpires;
        private bool _passwordRequired;
        private string _status;


        #endregion

        #region properties
        public string UserName { get => _userName; set => _userName = value; }
        public string Sid { get => _sid; set => _sid = value; }
        public string Domain { get => _domain; set => _domain = value; }
        public bool IsDisabled { get => _isDisabled; set => _isDisabled = value; }
        public bool IsLocalAccount { get => _isLocalAccount; set => _isLocalAccount = value; }
        public bool IsLockedOut { get => _isLockedOut; set => _isLockedOut = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public bool PasswordChangeable { get => _passwordChangeable; set => _passwordChangeable = value; }
        public bool PasswordExpires { get => _passwordExpires; set => _passwordExpires = value; }
        public bool PasswordRequired { get => _passwordRequired; set => _passwordRequired = value; }
        public string Status { get => _status; set => _status = value; }
        #endregion
    }
}
