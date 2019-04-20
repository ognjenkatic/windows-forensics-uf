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
    public class Win7RunBarService : IRecentRunBarService
    {
        #region fields
        private static string _RECENTLY_RUN_PATH = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\RunMRU";
        #endregion

        /// <summary>
        /// Gets the recent entries of commands issued in the run bar
        /// </summary>
        /// <returns>The recent entries</returns>
        public List<RunBarEntry> GetRecentlRunBarEntries()
        {
            List<RunBarEntry> recentRunEntries = new List<RunBarEntry>();

            try
            {
                RegistryKey runHistoryRegistryKey = Registry.CurrentUser.OpenSubKey(_RECENTLY_RUN_PATH);

                string[] runEntries = runHistoryRegistryKey.GetValueNames();

                foreach (string runEntry in runEntries)
                {
                    RunBarEntry rbe = new RunBarEntry();
                    rbe.Name = runEntry;
                    rbe.Value = runHistoryRegistryKey.GetValue(runEntry).ToString();

                    // FIX This is missleading. All run bar entries will have the same last write date because all of the data is associated with the same subkey
                    rbe.LastWrite = RegQueryInformationHelper.GetLastWritten(runHistoryRegistryKey);
                    recentRunEntries.Add(rbe);

                    
                }
            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            MyDebugger.Instance.LogMessage($"Loaded {recentRunEntries.Count} recently run entries.", DebugVerbocity.Informational);
            return recentRunEntries;
        }
    }
}
