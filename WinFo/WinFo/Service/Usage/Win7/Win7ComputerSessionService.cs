using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service.MyDebug;
using WinFo.Usage.Model;

namespace WinFo.Service.Usage.Win7
{
    /// <summary>
    /// Service responsable for fetching information about computer power on and off cycles
    /// </summary>
    public class Win7ComputerSessionService : IComputerSessionService
    {

        #region fields
        private static int _STARTUP_INDICATOR_EVENT_ID = 6005;
        private static int _SHUTDOWN_INDICATOR_EVENT_ID = 6006;
        private static int _IMPROPER_SHUTDOWN_INDICATOR_EVENT_ID = 6008;
        
        private static string _COMPUTER_SESSION_LOG_NAME = "System";

        #endregion

        /// <summary>
        /// Gets a list of computer usage sessions based on the event log
        /// </summary>
        /// <returns>The list of computer usage sessions</returns>
        public List<ComputerSession> GetComputerSessions()
        {
            List<ComputerSession> sessions = new List<ComputerSession>();
            try
            {
                EventLog el = new EventLog();
                el.Log = _COMPUTER_SESSION_LOG_NAME;

                EventLogEntry startev = null;
                EventLogEntry endev = null;

                foreach (EventLogEntry ele in el.Entries)
                {
                    int eventId = (UInt16)ele.InstanceId;

                    if (eventId == _STARTUP_INDICATOR_EVENT_ID)
                    {
                        startev = ele;
                    }
                    else if (eventId == _SHUTDOWN_INDICATOR_EVENT_ID || eventId == _IMPROPER_SHUTDOWN_INDICATOR_EVENT_ID)
                    {
                        endev = ele;
                    }

                    // check if a session is completed and the events are in right order
                    if (startev != null && endev != null && endev.TimeGenerated > startev.TimeGenerated)
                    {
                        ComputerSession us = new ComputerSession();
                        us.Beginning = startev.TimeGenerated;
                        us.End = endev.TimeGenerated;
                        us.MachineName = endev.MachineName;

                        sessions.Add(us);
                        startev = endev = null;
                    }
                }
            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return sessions;
        }
    }
}
