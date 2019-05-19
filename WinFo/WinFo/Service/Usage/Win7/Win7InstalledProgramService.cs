using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the installed programs
    /// </summary>
    public class Win7InstalledProgramService : IInstalledProgramService
    {
        private static string _INSTALLED_PROGRAMS_PATH = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

        private static string _INSTALLED_PROGRAMS_x86_KEY_PATH = @"SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

        /// <summary>
        /// Parses the information about an installed program from the registry information
        /// </summary>
        /// <param name="installedProgramRegEntry">The registry subkey</param>
        /// <param name="id">The after which the subkey is named</param>
        /// <returns>The installed program</returns>
        private InstalledProgram ParseInstalledProgramFromKey(RegistryKey installedProgramRegEntry,string id)
        {
            InstalledProgram ip = new InstalledProgram();
            foreach (string subkeyName in installedProgramRegEntry.GetValueNames())
            {
                object subkeyValue = installedProgramRegEntry.GetValue(subkeyName);
                
                if (subkeyValue != null)
                    switch (subkeyName)
                    {
                        case ("DisplayName"):
                            {
                                ip.Name = subkeyValue.ToString();
                                break;
                            }
                        case ("InstallDate"):
                            {
                                try
                                {
                                    ip.InstallDate = DateTime.ParseExact(subkeyValue.ToString(), new string[] { CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, "ddMMyyyy","yyyyMMdd","MMddyyyy","yyyyddMM" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
                                } catch (Exception exc)
                                {
                                    MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
                                    ip.InstallDate = DateTime.MinValue;
                                }
                                break;
                            }
                        case ("Publisher"):
                            {
                                ip.Publisher = subkeyValue.ToString();
                                break;
                            }
                        case ("DisplayVersion"):
                            {
                                ip.DisplayVersion = subkeyValue.ToString();
                                break;
                            }
                        case ("InstallLocation"):
                            {
                                ip.InstallLocation = subkeyValue.ToString();
                                break;
                            }
                        case ("EstimatedSize"):
                            {
                                ip.EstimatedSize = Convert.ToUInt32(subkeyValue);
                                break;
                            }


                    }

            }
            if (ip.Name == null || ip.Name == "")
                ip.Name = id;
            return ip;
        }

        /// <summary>
        /// Gets a list of installed programs
        /// </summary>
        /// <returns>The list of installed programs</returns>
        public List<InstalledProgram> GetInstalledPrograms()
        {
            List<InstalledProgram> installedPrograms = new List<InstalledProgram>();

            try
            {
                RegistryKey installedRegistryPath = Registry.LocalMachine.OpenSubKey(_INSTALLED_PROGRAMS_PATH);
                RegistryKey userSpecificInstalledRegistryPath = Registry.CurrentUser.OpenSubKey(_INSTALLED_PROGRAMS_PATH);
                RegistryKey installedx86RegistryPath = Registry.LocalMachine.OpenSubKey(_INSTALLED_PROGRAMS_x86_KEY_PATH);

                string[] machineSpecificInstalledIds = installedRegistryPath.GetSubKeyNames();
                string[] userSpecificInstalledIds = userSpecificInstalledRegistryPath.GetSubKeyNames();
                string[] installedx86RegistryIds = installedx86RegistryPath.GetSubKeyNames();

                foreach (string id in machineSpecificInstalledIds)
                {
                    RegistryKey installedProgramRegEntry = Registry.LocalMachine.OpenSubKey(_INSTALLED_PROGRAMS_PATH + "\\" + id);
                    InstalledProgram ip = ParseInstalledProgramFromKey(installedProgramRegEntry, id);
                    if (ip.InstallDate == DateTime.MinValue)
                        ip.InstallDate = RegQueryInformationHelper.GetLastWritten(installedProgramRegEntry);
                    if (!installedPrograms.Contains(ip))
                        installedPrograms.Add(ip);
                }

                foreach (string id in userSpecificInstalledIds)
                {
                    RegistryKey installedProgramRegEntry = Registry.CurrentUser.OpenSubKey(_INSTALLED_PROGRAMS_PATH + "\\" + id);
                    InstalledProgram ip = ParseInstalledProgramFromKey(installedProgramRegEntry, id);
                    if (ip.InstallDate == DateTime.MinValue)
                        ip.InstallDate = RegQueryInformationHelper.GetLastWritten(installedProgramRegEntry);
                    if (!installedPrograms.Contains(ip))
                        installedPrograms.Add(ip);
                }

                foreach (string id in installedx86RegistryIds)
                {
                    RegistryKey installedProgramRegEntry = Registry.LocalMachine.OpenSubKey(_INSTALLED_PROGRAMS_x86_KEY_PATH + "\\" + id);
                    InstalledProgram ip = ParseInstalledProgramFromKey(installedProgramRegEntry, id);
                    if (ip.InstallDate == DateTime.MinValue)
                        ip.InstallDate = RegQueryInformationHelper.GetLastWritten(installedProgramRegEntry);
                    if (!installedPrograms.Contains(ip))
                        installedPrograms.Add(ip);
                }

            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }
            return installedPrograms;
        }
    }
}
