using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service.MyDebug;
using WinFo.Service.Utility;
using WinFo.Usage.Model;
using WinFo.Service.Utility.Misc;

namespace WinFo.Service.Usage.Win10
{
    public class Win10ComputerSessionService : IComputerSessionService
    {
        #region fields
        private static int _STARTUP_INDICATOR_EVENT_ID = 1;
        private static int _SHUTDOWN_INDICATOR_EVENT_ID = 42;

        private static int _STARTUP_TASK_CATEGORY_NUMBER = 5;
        private static int _SHUTDOWN_TASK_CATEGORY_NUMBER = 64;

        private static int _IMPROPER_SHUTDOWN_INDICATOR_EVENT_ID = 6008;

        private static string _COMPUTER_SESSION_LOG_NAME = "System";

        public event UpdateProgressDelegate UpdateProgress;
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

                int counter = 0;
                foreach (EventLogEntry ele in el.Entries)
                {
                    UpdateProgress?.Invoke($"Searching for computer session entries, processed {++counter}/{el.Entries.Count} events found ({Math.Round(100.0 * counter / el.Entries.Count)}%).");
                    int eventId = (UInt16)ele.InstanceId;

                    if (eventId == _STARTUP_INDICATOR_EVENT_ID && ele.CategoryNumber == _STARTUP_TASK_CATEGORY_NUMBER && ele.UserName == null)
                    {
                        startev = ele;
                    }
                    else if (eventId == _SHUTDOWN_INDICATOR_EVENT_ID && ele.CategoryNumber == _SHUTDOWN_TASK_CATEGORY_NUMBER && ele.UserName == null || eventId == _IMPROPER_SHUTDOWN_INDICATOR_EVENT_ID)
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

            string logMessage = $"Loaded {sessions.Count} computer sessions.";
            MyDebugger.Instance.LogMessage(logMessage, DebugVerbocity.Informational);
            UpdateProgress?.Invoke(logMessage);
            return sessions;
        }
    }
}
