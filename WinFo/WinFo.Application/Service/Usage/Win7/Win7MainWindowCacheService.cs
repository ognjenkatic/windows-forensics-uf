using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win7
{
    public class Win7MainWindowCacheService : IMainWindowCacheService
    {
        #region fields
        private static string _WINDOW_TITLE_CACHE_PATH = @"SOFTWARE\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\Shell\\MuiCache";
        #endregion

        /// <summary>
        /// Gets the recently used entries based on the window cache
        /// </summary>
        /// <returns>The recently used entries</returns>
        public List<MainWindowCacheEntry> GetMainWindowCache()
        {
            List<MainWindowCacheEntry> mainCacheEntries = new List<MainWindowCacheEntry>();

            try
            {
                RegistryKey windowHistoryRegistryKey = Registry.CurrentUser.OpenSubKey(_WINDOW_TITLE_CACHE_PATH);

                string[] valueNames = windowHistoryRegistryKey.GetValueNames();

                foreach (string valueName in valueNames)
                {
                    int fapIndex = valueName.IndexOf(".FriendlyAppName");
                    if (fapIndex > 0)
                    {
                        MainWindowCacheEntry mwce = new MainWindowCacheEntry();
                        mwce.Title = windowHistoryRegistryKey.GetValue(valueName).ToString();
                        mwce.Path = valueName.Remove(fapIndex);
                        mainCacheEntries.Add(mwce);
                    }
                }
            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            MyDebugger.Instance.LogMessage($"Loaded {mainCacheEntries.Count} window cache entries.", DebugVerbocity.Informational);
            return mainCacheEntries;
        }
    }
}
