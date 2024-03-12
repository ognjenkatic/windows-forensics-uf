using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    /// <summary>
    /// Represents a service on the system
    /// </summary>
    public class WindowsService
    {
        private bool _doesAcceptStop;
        private bool _doesAcceptPause;
        private string _caption;
        private string _name;
        private string _status;
        private bool _isDesktopInteract;
        private string _type;
        private string _description;
        private string _displayName;
        private UInt32 _pid;
        private bool _isStarted;
        private string _state;
        private string _pathName;
        private string _startMode;
        private string _startName;

        public bool DoesAcceptStop { get => _doesAcceptStop; set => _doesAcceptStop = value; }
        public bool DoesAcceptPause { get => _doesAcceptPause; set => _doesAcceptPause = value; }
        public string Caption { get => _caption; set => _caption = value; }
        public string Name { get => _name; set => _name = value; }
        public string Status { get => _status; set => _status = value; }
        public bool IsDesktopInteract { get => _isDesktopInteract; set => _isDesktopInteract = value; }
        public string Type { get => _type; set => _type = value; }
        public string Description { get => _description; set => _description = value; }
        public string DisplayName { get => _displayName; set => _displayName = value; }
        public uint Pid { get => _pid; set => _pid = value; }
        public bool IsStarted { get => _isStarted; set => _isStarted = value; }
        public string State { get => _state; set => _state = value; }
        public string PathName { get => _pathName; set => _pathName = value; }
        public string StartMode { get => _startMode; set => _startMode = value; }
        public string StartName { get => _startName; set => _startName = value; }
    }
}
