using System;

namespace WinFo.Usage.Model
{
    /// <summary>
    /// Representation of a time span in which a computer was powered on
    /// </summary>
    public class ComputerSession
    {
        #region fields
        private DateTime _beginning;
        private DateTime _end;
        private string _machineName;
        #endregion
        #region properties
        public DateTime Beginning { get => _beginning; set => _beginning = value; }
        public DateTime End { get => _end; set => _end = value; }
        public string MachineName { get => _machineName; set => _machineName = value; }
        public TimeSpan Duration
        {
            get
            {
                return _end.Subtract(Beginning);
            }
        }

        #endregion
    }
}
