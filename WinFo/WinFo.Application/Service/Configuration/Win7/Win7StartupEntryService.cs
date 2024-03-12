using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;
using WinFo.Service.Configuration;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Configuration.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the commands execute at startup
    /// </summary>
    public class Win7StartupEntryService : IStartupEntryService
    {
        #region fields
        private static string _RUN_REGISTRY_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        private static string _RUN_ONCE_REGISTRY_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
        #endregion

        /// <summary>
        /// Gets the list of startup entries
        /// </summary>
        /// <returns></returns>
        public List<StartupEntry> GetStartupEntries()
        {
            List<StartupEntry> startupEntries = new List<StartupEntry>();

            try
            {
                // the registry key containing commands that run every startup
                RegistryKey runRegistryKey = Registry.CurrentUser.OpenSubKey(_RUN_REGISTRY_KEY);

                // the registry key containing commands that run only once
                RegistryKey runOnceRegistryKey = Registry.CurrentUser.OpenSubKey(_RUN_ONCE_REGISTRY_KEY);

                string[] runValueNames = runRegistryKey.GetValueNames();
                string[] runOnceValueNames = runOnceRegistryKey.GetSubKeyNames();

                foreach(string valueName in runValueNames){

                    StartupEntry re = new StartupEntry();

                    re.EntryName = valueName;
                    re.EntryCommand = runRegistryKey.GetValue(valueName).ToString();

                    startupEntries.Add(re);
                }

                foreach (string valueName in runOnceValueNames)
                {

                    StartupEntry re = new StartupEntry();

                    re.EntryName = valueName;
                    re.EntryCommand = runOnceRegistryKey.GetValue(valueName).ToString();

                    startupEntries.Add(re);
                }
            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return startupEntries;
        }
    }
}
