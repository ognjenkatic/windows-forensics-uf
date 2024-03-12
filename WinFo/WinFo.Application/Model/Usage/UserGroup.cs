namespace WinFo.Model.Usage
{
    /// <summary>
    /// Information about a user group
    /// </summary>
    public class UserGroup
    {
        #region fields
        private string _description;
        private string _sid;
        private string _name;
        private bool _isLocalAccount;
        #endregion

        #region properties
        public string Description { get => _description; set => _description = value; }
        public string Sid { get => _sid; set => _sid = value; }
        public string Name { get => _name; set => _name = value; }
        public bool IsLocalAccount { get => _isLocalAccount; set => _isLocalAccount = value; }
        #endregion

        #region methods
        public override bool Equals(object obj)
        {
            if (obj is UserGroup)
            {
                UserGroup ug = (UserGroup)obj;
                if (this.Name.Equals(ug.Name))
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        #endregion
    }
}
