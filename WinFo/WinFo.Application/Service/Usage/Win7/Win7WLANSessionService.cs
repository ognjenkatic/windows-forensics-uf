using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the WLAN sessions
    /// </summary>
    public class Win7WLANSessionService : IWLANSessionService
    {
        private static string _WIRELESS_HISTORY_EVENT_PATH = @"%SystemRoot%\System32\Winevt\Logs\Microsoft-Windows-WLAN-AutoConfig%4Operational.evtx";

        /// <summary>
        /// Gets a list of WLAN sessions
        /// </summary>
        /// <returns>The list of WLAN sessions</returns>
        public List<WLANSession> GetWLANSessions()
        {
            List<WLANSession> sessions = new List<WLANSession>();

            try
            {
                string expandedPath = Environment.ExpandEnvironmentVariables(_WIRELESS_HISTORY_EVENT_PATH);

                using (var reader = new EventLogReader(expandedPath, PathType.FilePath))
                {
                    EventRecord eventLogEntry;
                    WLANSession ws = null;
                    while ((eventLogEntry = reader.ReadEvent()) != null)
                    {
                        if (eventLogEntry.Id == 8001)
                        {
                            if (ws != null)
                            {
                                sessions.Add(ws);
                            }

                            ws = new WLANSession();
                            ws.Authentication = eventLogEntry.Properties[8].Value.ToString();
                            ws.Bssid = eventLogEntry.Properties[6].Value.ToString();
                            ws.BssidType = eventLogEntry.Properties[5].Value.ToString();
                            ws.ConnectedTime = eventLogEntry.TimeCreated ?? DateTime.MinValue;
                            ws.ConnectionMode = eventLogEntry.Properties[2].Value.ToString();
                            ws.Encryption = eventLogEntry.Properties[9].Value.ToString();
                            ws.Is8021xEnabled = (eventLogEntry.Properties[10].Value.ToString() == "Yes") ? true : false;
                            ws.NetworkAdapter = eventLogEntry.Properties[0].Value.ToString();
                            ws.Ssid = eventLogEntry.Properties[4].Value.ToString();
                            ws.Type = eventLogEntry.Properties[7].Value.ToString();

                        } else if (eventLogEntry.Id == 8003)
                        {
                            if (ws == null)
                            {
                                ws = new WLANSession();
                                ws.BssidType = eventLogEntry.Properties[5].Value.ToString();
                                ws.DisconnectedTime = eventLogEntry.TimeCreated ?? DateTime.MinValue;
                                ws.ConnectionMode = eventLogEntry.Properties[2].Value.ToString();
                                ws.NetworkAdapter = eventLogEntry.Properties[0].Value.ToString();
                                ws.Ssid = eventLogEntry.Properties[4].Value.ToString();
                                sessions.Add(ws);
                                ws = null;
                            }
                            else
                            {
                                ws.DisconnectedTime = eventLogEntry.TimeCreated ?? DateTime.MinValue;
                                sessions.Add(ws);
                                ws = null;
                            }
                        }
                    }
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return sessions;
        }
    }
}
