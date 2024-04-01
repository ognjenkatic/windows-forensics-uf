using System;

namespace WinFo.Usage.Model
{
    public enum SessionType
    {
        RemoteUser, LocalUser
    }

    /// <summary>
    /// Representation of a time span in which a user was logged on
    /// </summary>
    public class UserSession
    {
        #region fields
        private bool _signupSuccess;

        private DateTime _beginning;

        private DateTime _end;

        private string _username;

        private SessionType _type;

        private string _logonID;

        private string _remoteConnectionIPAddress;
        #endregion
        #region properties
        public bool SignupSuccess { get => _signupSuccess; set => _signupSuccess = value; }
        public DateTime Beginning { get => _beginning; set => _beginning = value; }
        public DateTime End { get => _end; set => _end = value; }
        public TimeSpan Duration
        {
            get
            {
                return _end.Subtract(Beginning);
            }
        }
        public string Username { get => _username; set => _username = value; }
        public SessionType Type { get => _type; set => _type = value; }
        public string LogonID { get => _logonID; set => _logonID = value; }
        public string RemoteConnectionIPAddress { get => _remoteConnectionIPAddress; set => _remoteConnectionIPAddress = value; }
        #endregion
    }
}
