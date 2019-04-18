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
    public class Win7USBDeviceHistoryService : IUSBDeviceHistoryService
    {
        private static string _USBSTOR_REG_KEY = @"SYSTEM\\CurrentControlSet\\Enum\\USBSTOR";

        private static string _USB_REG_KEY = @"SYSTEM\\CurrentControlSet\\Enum\\USB";

        private static string _MOUNTED_DEVICES_KEY = @"SYSTEM\\MountedDevices";
        /// <summary>
        /// Gets a list of usb device history entries
        /// </summary>
        /// <returns>The list of usb device history entries</returns>
        public List<USBDeviceHistoryEntry> GetUSBDeviceHistory()
        {
            List<USBDeviceHistoryEntry> history = new List<USBDeviceHistoryEntry>();
            try
            {
                RegistryKey usbstoreKey = Registry.LocalMachine.OpenSubKey(_USBSTOR_REG_KEY);
                RegistryKey usbKey = Registry.LocalMachine.OpenSubKey(_USB_REG_KEY);

                foreach (RegistryKey regKey in new RegistryKey[] { usbstoreKey, usbKey })
                {
                    foreach (string classKeyName in regKey.GetSubKeyNames())
                    {
                        RegistryKey classKey = regKey.OpenSubKey(classKeyName);

                        foreach (string instanceKeyName in classKey.GetSubKeyNames())
                        {
                            RegistryKey instanceKey = classKey.OpenSubKey(instanceKeyName);

                            USBDeviceHistoryEntry entry = new USBDeviceHistoryEntry();
                            entry.DeviceId = instanceKeyName;
                            object description = instanceKey.GetValue("DeviceDesc");
                            object name = instanceKey.GetValue("FriendlyName");

                            entry.DeviceDescription = "Unknown";
                            entry.DeviceName = "Unknown";

                            if (description != null)
                            {
                                string dsc = description.ToString();
                                if (dsc.Contains(';'))
                                    dsc = dsc.Split(';')[1];

                                entry.DeviceName = dsc;
                                entry.DeviceDescription = dsc;
                            }
                            if (name != null && name != "")
                            {
                                entry.DeviceName = name.ToString();
                            }
                            

                            
                            entry.LastSeen = RegQueryInformationHelper.GetLastWritten(instanceKey);

                            if (!history.Contains(entry))
                            {
                                history.Add(entry);
                            }
                            
                        }
                    }
                }

                RegistryKey rki = Registry.LocalMachine.OpenSubKey(_MOUNTED_DEVICES_KEY);

                string[] names = rki.GetValueNames();

                foreach (string name in names)
                {
                    if (name.Contains("DosDevices"))
                    {
                        object ray = rki.GetValue(name, RegistryValueKind.Binary);
                        string vaks = System.Text.Encoding.Unicode.GetString((byte[])ray);
                        string[] info = vaks.Split('#');

                        foreach (USBDeviceHistoryEntry entry in history)
                        {
                            if (info.Length > 2 && entry.DeviceId == info[2])
                            {
                                entry.HasMountPoint = true;
                                entry.MountPoint = name.Split('\\')[2];
                            }
                        }
                    }

                }
                
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            
            return history;
        }
    }
}
