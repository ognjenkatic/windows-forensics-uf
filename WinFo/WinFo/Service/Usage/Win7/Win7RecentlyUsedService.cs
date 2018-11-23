using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using IWshRuntimeLibrary;
using WinFo.Service.MyDebug;
using Microsoft.Win32;

namespace WinFo.Service.Usage.Win7
{
    /// <summary>
    /// Service responsable for fetching information about recently used resources
    /// </summary>
    public class Win7RecentlyUsedService : IRecentlyUsedService
    {
        #region fields
        private static string _RECENTLY_OPENED_PATH = @"%USERPROFILE%\\AppData\\Roaming\\Microsoft\\Windows\Recent";
        private static string _RECENTLY_RUN_PATH = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\RunMRU";
        private static string _WINDOW_TITLE_CACHE_PATH = @"SOFTWARE\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\Shell\\MuiCache";
        #endregion

        /// <summary>
        /// Gets the recently used entries based on the window cache
        /// </summary>
        /// <returns>The recently used entries</returns>
        public List<RecentlyUsedEntry> GetMainWindowCache()
        {
            List<RecentlyUsedEntry> recentlyUsedEntries = new List<RecentlyUsedEntry>();

            try
            {
                RegistryKey windowHistoryRegistryKey = Registry.CurrentUser.OpenSubKey(_WINDOW_TITLE_CACHE_PATH);

                string[] valueNames = windowHistoryRegistryKey.GetValueNames();

                foreach (string valueName in valueNames)
                {
                    RecentlyUsedEntry rue = new RecentlyUsedEntry();
                    rue.Name = valueName;
                    rue.Value = windowHistoryRegistryKey.GetValue(valueName).ToString();

                    recentlyUsedEntries.Add(rue);
                }
            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return recentlyUsedEntries;
        }
        
        /// <summary>
        /// Gets the recent entries of commands issued in the run bar
        /// </summary>
        /// <returns>The recent entries</returns>
        public List<RecentlyUsedEntry> GetRecentlRunBarEntries()
        {
            List<RecentlyUsedEntry> recentlyUsedEntries = new List<RecentlyUsedEntry>();

            try
            {
                RegistryKey runHistoryRegistryKey = Registry.CurrentUser.OpenSubKey(_RECENTLY_RUN_PATH);

                string[] valueNames = runHistoryRegistryKey.GetValueNames();

                foreach(string valueName in valueNames)
                {
                    RecentlyUsedEntry rue = new RecentlyUsedEntry();
                    rue.Name = valueName;
                    rue.Value = runHistoryRegistryKey.GetValue(valueName).ToString();

                    recentlyUsedEntries.Add(rue);
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return recentlyUsedEntries;
        }

        /// <summary>
        /// Get a list of recently used opened files by parsing the contents of the windows recent items folder
        /// </summary>
        /// <returns>The list of recently opened files</returns>
        public List<RecentlyUsedEntry> GetRecentlyOpenedFiles()
        {
            List<RecentlyUsedEntry> recentlyUsedEntries = new List<RecentlyUsedEntry>();

            try
            {
                string recentFolderPath = Environment.ExpandEnvironmentVariables(_RECENTLY_OPENED_PATH);
                IEnumerable<string> recentFiles = Directory.EnumerateFiles(recentFolderPath);

                foreach (string file in recentFiles)
                {
                    try
                    {
                        if (file.EndsWith("lnk"))
                        {
                            IWshShell shell = new WshShell();
                            if (shell.CreateShortcut(file) is IWshShortcut lnk)
                            {
                                RecentlyUsedEntry rue = new RecentlyUsedEntry();
                                rue.Name = Path.GetFileName(lnk.TargetPath);
                                rue.Value = lnk.TargetPath;

                                rue.Accessed = System.IO.File.GetLastAccessTime(lnk.TargetPath);
                                rue.Created = System.IO.File.GetCreationTime(lnk.TargetPath);

                                recentlyUsedEntries.Add(rue);
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
                    }
                }

            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return recentlyUsedEntries;
        }
    }
}
