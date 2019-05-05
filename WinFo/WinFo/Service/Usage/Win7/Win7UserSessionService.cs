using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service.MyDebug;
using WinFo.Service.Utility;
using WinFo.Usage.Model;

namespace WinFo.Service.Usage.Win7
{
    /// <summary>
    /// Service responsable for fetching information about user sessions
    /// </summary>
    public class Win7UserSessionService : IUserSessionService
    {
        #region fields
        private static int _INTERACTIVE_LOGON_TYPE_CODE = 2;
        private static int _REMOTE_INTERFACTIVE_LOGON_TYPE_CODE = 10;

        private static int _LOGON_EVENT_ID = 4624;
        private static int _LOGOFF_EVENT_ID = 4634;
        private static int _INITIATE_LOGOFF_EVENT_ID = 4647;
        
        private static int _EVENTLOG_SERVICE_SHUTDOWN_EVENT_ID = 1100;

        private static string _USER_SESSION_LOG_NAME = "Security";

        private int _shutdown_counter = 0;

        public event UpdateProgressDelegate UpdateProgress;

        #endregion

        #region method
        // Since the LoginID is only unique between reboots (also system log and security log dont always go back in history
        // the same amount) and testing showed conflicts appear several timers per year,
        // we need to make an identifier with a smaller chance of conflict.
        // The identifier is built as: account name + login id + shutdown count
        private bool TryGenerateIdentifier(int eventId, EventLogEntry eventLogEntry, out string identifier)
        {
            bool assignedIdentifier = false;
            try
            {
                if (eventId == _LOGON_EVENT_ID)
                {
                    // build the identifier
                    identifier = eventLogEntry.ReplacementStrings[5] + eventLogEntry.ReplacementStrings[7] + eventLogEntry + _shutdown_counter;
                    assignedIdentifier = true;
                }
                else if (eventId == _LOGOFF_EVENT_ID || eventId == _INITIATE_LOGOFF_EVENT_ID)
                {
                    // build the identifier
                    identifier = eventLogEntry.ReplacementStrings[1] + eventLogEntry.ReplacementStrings[3] + eventLogEntry + _shutdown_counter;
                    assignedIdentifier = true;
                }
                else
                {
                    identifier = "";
                }
            }
            catch (Exception exc)
            {
                identifier = "";
                assignedIdentifier = false;
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return assignedIdentifier;
        }

        /// <summary>
        /// Gets a list of all the user sessions available from the event logs
        /// </summary>
        /// <returns>The list of user sessions</returns>
        public List<UserSession> GetUserSessions(string username = null)
        {
            // Maps user sessions to their unique keys
            Dictionary<string, UserSession> sessionsDictionary = new Dictionary<string, UserSession>();
            List<UserSession> validSessions = new List<UserSession>();
            try
            {
                EventLog eventLog = new EventLog();
                eventLog.Log = _USER_SESSION_LOG_NAME;

                int counter = 0;
                // iterate over events and add the ones of interest to the dictionary
                foreach (EventLogEntry ele in eventLog.Entries)
                {
                    UpdateProgress($"Searching for user session entries, processed {++counter}/{eventLog.Entries.Count} events found ({Math.Round(100.0 * counter / eventLog.Entries.Count)}%).");
                    // extract the event id part of instance id
                    int eventId = (UInt16)ele.InstanceId;

                    if (eventId == _EVENTLOG_SERVICE_SHUTDOWN_EVENT_ID)
                    {
                        // this tracks shutdowns to help keep identifiers unique
                        _shutdown_counter++;
                    }

                    string identifier = "";

                    // try to generate a better identifier
                    if (TryGenerateIdentifier(eventId, ele, out identifier))
                    {
                        if (eventId == _LOGON_EVENT_ID)
                        {

                            int logonTypeId = Convert.ToInt32(ele.ReplacementStrings[8]);

                            // TO-DO - make this statement less hackey. It filters out the uninteresting usernames of UMFD and DWM
                            if (logonTypeId != _INTERACTIVE_LOGON_TYPE_CODE && logonTypeId != _REMOTE_INTERFACTIVE_LOGON_TYPE_CODE ||
                                sessionsDictionary.ContainsKey(identifier) || (username != null && ele.ReplacementStrings[5] != username) || ele.ReplacementStrings[5].StartsWith("DWM") 
                                || ele.ReplacementStrings[5].StartsWith("UMFD"))
                                continue;
                            else
                            {
                                UserSession us = new UserSession();
                                us.LogonID = ele.ReplacementStrings[7];
                                us.Username = ele.ReplacementStrings[5];
                                us.Beginning = ele.TimeGenerated;
                                if (logonTypeId == _INTERACTIVE_LOGON_TYPE_CODE)
                                    us.Type = SessionType.LocalUser;
                                else if (logonTypeId == _REMOTE_INTERFACTIVE_LOGON_TYPE_CODE)
                                {
                                    us.Type = SessionType.RemoteUser;
                                    us.RemoteConnectionIPAddress = ele.ReplacementStrings[12];
                                }
                                if (!sessionsDictionary.ContainsKey(identifier))
                                    sessionsDictionary.Add(identifier, us);
                            }
                        }
                        else if ((eventId == _INITIATE_LOGOFF_EVENT_ID || eventId == _LOGOFF_EVENT_ID) &&
                            sessionsDictionary.ContainsKey(identifier))
                        {
                            sessionsDictionary[identifier].End = ele.TimeGenerated;
                            if (sessionsDictionary[identifier].End > sessionsDictionary[identifier].Beginning)
                                validSessions.Add(sessionsDictionary[identifier]);
                        }

                    }

                }
            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            string logMessage = $"Loaded {validSessions.Count} user sessions.";
            MyDebugger.Instance.LogMessage(logMessage, DebugVerbocity.Informational);
            UpdateProgress(logMessage);

            return validSessions;
        }
        #endregion
    }

}
