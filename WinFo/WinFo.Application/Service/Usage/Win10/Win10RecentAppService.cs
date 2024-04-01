using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win10
{
    /// <summary>
    /// Service responsable for fetching information about recently used apps
    /// </summary>
    public class Win10RecentAppService : IRecentAppService
    {
        #region fields
        private static string _RECENT_APP_REG_KEY = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Search\\RecentApps";
        private static string _RECENT_APP_ITEMS_REG_KEY = "RecentItems";
        #endregion

        public List<RecentAppEntry> GetRecentAppEntries()
        {
            List<RecentAppEntry> entries = new List<RecentAppEntry>();

            try
            {
                RegistryKey recentAppsKey = Registry.CurrentUser.OpenSubKey(_RECENT_APP_REG_KEY);

                foreach(string subkey in recentAppsKey.GetSubKeyNames())
                {
                    RegistryKey recentAppSubkey = recentAppsKey.OpenSubKey(subkey);

                    RecentAppEntry rae = new RecentAppEntry();

                    rae.AppId = (string)recentAppSubkey.GetValue("AppId");
                    rae.AppPath = (string)recentAppSubkey.GetValue("AppPath");
                    rae.LastAccessedTime = DateTime.FromFileTime((long)recentAppSubkey.GetValue("LastAccessedTime"));
                    rae.LaunchCount = (int)recentAppSubkey.GetValue("LaunchCount");

                    rae.RecentItems = new List<RecentAppItemEntry>();

                    foreach(string appItemsSubkey in recentAppSubkey.GetSubKeyNames())
                    {
                        if (appItemsSubkey.Equals(_RECENT_APP_ITEMS_REG_KEY))
                        {
                            RegistryKey recentItemsSubkey = recentAppSubkey.OpenSubKey(appItemsSubkey);

                            foreach(string itemSubkey in recentItemsSubkey.GetSubKeyNames())
                            {
                                RegistryKey recentItemSubkey = recentItemsSubkey.OpenSubKey(itemSubkey);

                                RecentAppItemEntry raie = new RecentAppItemEntry();
                                raie.DisplayName = (string)recentItemSubkey.GetValue("DisplayName");
                                raie.Path = (string)recentItemSubkey.GetValue("Path");
                                raie.LastAccessedTime = DateTime.FromFileTime((long)recentItemSubkey.GetValue("LastAccessedTime"));

                                rae.RecentItems.Add(raie);

                            }
                        }
                    }

                    entries.Add(rae);
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entries;
        }
    }
}
