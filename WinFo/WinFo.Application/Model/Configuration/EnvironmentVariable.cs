using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about an environment variable
    /// </summary>
    public class EnvironmentVariable
    {
        #region fields
        private string _key;
        private string _value;
        private bool _isSystemVariable;
        private string _username;
        #endregion

        #region properties
        public string Key { get => _key; set => _key = value; }
        public string Value { get => _value; set => _value = value; }
        public bool IsSystemVariable { get => _isSystemVariable; set => _isSystemVariable = value; }
        public string Username { get => _username; set => _username = value; }
        #endregion
    }
}
