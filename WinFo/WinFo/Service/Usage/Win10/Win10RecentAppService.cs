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

                    foreach(string appItemsSubkey in recentAppSubkey.GetSubKeyNames())
                    {
                        if (appItemsSubkey.Equals(_RECENT_APP_ITEMS_REG_KEY))
                        {
                            RegistryKey recentItemsSubkey = recentAppSubkey.OpenSubKey(appItemsSubkey);

                            foreach(string itemSubkey in recentItemsSubkey.GetSubKeyNames())
                            {
                                RegistryKey recentItemSubkey = recentItemsSubkey.OpenSubKey(itemSubkey);

                                string displayName = (string)recentItemSubkey.GetValue("DisplayName");

                                Console.WriteLine("---" + displayName);
                            }
                        }
                    }
                    string appId = (string)recentAppSubkey.GetValue("AppId");
                    Console.WriteLine(appId);
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entries;
        }
    }
}
