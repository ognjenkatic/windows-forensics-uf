using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;
using WinFo.Service.Utility;
using WinFo.Service.Utility.Misc;

namespace WinFo.Service.Usage.Win7
{
    //TO-DO This registry value has changed in win7 to 72 bytes, in previous versions the data encoding was different, implement for previous versions
    /// <summary>
    /// Service responsable for fetching information about launched programs
    /// </summary>
    public class Win7UserAssistService : IUserAssistService
    {
        #region fields
        private static string _USER_ASSIST_REG_KEY = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\UserAssist";
        private static string _EXECUTABLE_FILE_REG_KEY_PREFIX = "{CEBFF5CD";
        private static string _SHORTCUT_REG_KEY_PREFIX = "{F4E57C4B";
        private static string _COUNT_REG_KEY = "Count";
        #endregion
        public List<UserAssistEntry> GetUserAssistEntries()
        {
            List<UserAssistEntry> entries = new List<UserAssistEntry>();
            
            try
            {
                RegistryKey userAssistKey = Registry.CurrentUser.OpenSubKey(_USER_ASSIST_REG_KEY);
                string[] subkeyNames = userAssistKey.GetSubKeyNames();

                foreach(string subkeyName in subkeyNames)
                {
                    if (subkeyName.StartsWith(_EXECUTABLE_FILE_REG_KEY_PREFIX) || subkeyName.StartsWith(_SHORTCUT_REG_KEY_PREFIX))
                    {
                        RegistryKey executablesFilesSubkey = userAssistKey.OpenSubKey(subkeyName);
                        RegistryKey countSubkey = executablesFilesSubkey.OpenSubKey(_COUNT_REG_KEY);

                        string[] valueNames = countSubkey.GetValueNames();

                        foreach(string valueName in valueNames)
                        {
                            UserAssistEntry uae = new UserAssistEntry();
                            
                            //decoding information from https://intotheboxes.files.wordpress.com/2010/04/intotheboxes_2010_q1.pdf

                            byte[] value = (byte[])countSubkey.GetValue(valueName);
                            long fileTime = BitConverter.ToInt64(value, 60);

                            uae.Path = ROT13Decoder.Decode(valueName);
                            uae.LastLaunchTime = DateTime.FromFileTime(fileTime);
                            uae.RunCount = BitConverter.ToInt32(value, 4);
                            uae.FocusCount = BitConverter.ToInt32(value, 8);
                            uae.FocusTimeMillis = BitConverter.ToInt32(value, 12);
                            
                            entries.Add(uae);
                        }
                    }
                    else
                        continue;
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entries;
        }
    }
}
