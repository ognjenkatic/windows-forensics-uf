using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win10
{
    public class Win10BAMService : IBAMService
    {
        private static string _BAM_REG_KEY = @"SYSTEM\\CurrentControlSet\\Services\\bam\\State\\UserSettings";
        private static string _ALT_BAM_REG_KEY = @"SYSTEM\\CurrentControlSet\\Services\\bam\\UserSettings";
        public List<BAMEntry> GetBAMEntries()
        {
            List<BAMEntry> entries = new List<BAMEntry>();

            try
            {
                RegistryKey bamKey = Registry.LocalMachine.OpenSubKey(_BAM_REG_KEY);

                foreach(string subkeyName in bamKey.GetSubKeyNames())
                {
                    //TO-DO add regex check
                    string sid = subkeyName;
                    RegistryKey sidKey = bamKey.OpenSubKey(sid);

                    foreach(string valueName in sidKey.GetValueNames())
                    {
                        if (sidKey.GetValueKind(valueName) == RegistryValueKind.Binary)
                        {
                            BAMEntry entry = new BAMEntry();
                            byte[] data = (byte[])sidKey.GetValue(valueName);
                            long lastRunLong = BitConverter.ToInt64(data, 0);
                            entry.LatestExecutionTime = DateTime.FromFileTime(lastRunLong);
                            entry.AppPath = valueName;
                            entry.AppName = Path.GetFileName(valueName);
                            entry.SID = sid;

                            entries.Add(entry);
                        }
                    }
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }
            return entries;
        }
    }
}
