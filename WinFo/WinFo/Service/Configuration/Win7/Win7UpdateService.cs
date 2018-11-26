using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Configuration.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the updates applied to the system
    /// </summary>
    public class Win7UpdateService : IUpdateService
    {
        #region fields
        private static string _UPDATE_LOG = "System";

        private static int _UPDATE_EVENT_ID = 19;
        #endregion

        #region methods
        /// <summary>
        /// Gets the update information as far back in time as the event log allows
        /// </summary>
        /// <returns>List of update information entries</returns>
        public List<Update> GetUpdates()
        {
            List<Update> updates = new List<Update>();

            try
            {
                EventLog el = new EventLog();
                el.Log = _UPDATE_LOG;

                foreach(EventLogEntry ele in el.Entries)
                {
                    int eventId = (UInt16)ele.InstanceId;
                    if (eventId == _UPDATE_EVENT_ID)
                    {
                        Update ud = new Update();
                        ud.Title = ele.ReplacementStrings[0];
                        ud.Source = ele.Source;
                        ud.Time = ele.TimeGenerated;

                        updates.Add(ud);
                    }
                }

            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return updates;
        }
        #endregion
    }
}
