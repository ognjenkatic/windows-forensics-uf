using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Configuration.Win7
{
    /// <summary>
    /// Service responsable for fetching BIOS information
    /// </summary>
    public class Win7BIOSService : IBIOSService
    {
        private static string _BIOS_SEARCH_STRING = "SELECT * FROM Win32_BIOS";

        private static Dictionary<UInt16, string> _BIOS_CHARACTERISTICS_DICTIONARY = new Dictionary<UInt16, string>()
        {

            { 0 , "Reserved"},
            { 1 , "Reserved" },
            { 2 , "Unknown" },
            { 3 , "BIOS Characteristics Not Supported" },
            { 4 , "ISA is supported" },
            { 5 , "MCA is supported" },
            { 6 , "EISA is supported" },
            { 7 , "PCI is supported" },
            { 8 , "PC Card (PCMCIA) is supported" },
            { 9 , "Plug and Play is supported" },
            { 10, "APM is supported" },
            { 11, "BIOS is Upgradeable (Flash)" },
            { 12, "BIOS shadowing is allowed" },
            { 13, "VL-VESA is supported" },
            { 14, "ESCD support is available" },
            { 15, "Boot from CD is supported" },
            { 16, "Selectable Boot is supported" },
            { 17, "BIOS ROM is socketed" },
            { 18, "Boot From PC Card (PCMCIA) is supported" },
            { 19, "EDD (Enhanced Disk Drive) Specification is supported" },
            { 20, "Int 13h - Japanese Floppy for NEC 9800 1.2mb (3.5\", 1k Bytes/Sector, 360 RPM) is supported" },
            { 21, "Int 13h - Japanese Floppy for Toshiba 1.2mb (3.5\", 360 RPM) is supported" },
            { 22, "Int 13h - 5.25\" / 360 KB Floppy Services are supported" },
            { 23, "Int 13h - 5.25\" /1.2MB Floppy Services are supported" },
            { 24, "Int 13h - 3.5\" / 720 KB Floppy Services are supported" },
            { 25, "Int 13h - 3.5\" / 2.88 MB Floppy Services are supported" },
            { 26, "Int 5h, Print Screen Service is supported" },
            { 27, "Int 9h, 8042 Keyboard services are supported" },
            { 28, "Int 14h, Serial Services are supported" },
            { 29, "Int 17h, printer services are supported" },
            { 30, "Int 10h, CGA/Mono Video Services are supported" },
            { 31, "NEC PC-98" },
            { 32, "ACPI supported" },
            { 33, "USB Legacy is supported" },
            { 34, "AGP is supported" },
            { 35, "I2O boot is supported" },
            { 36, "LS-120 boot is supported" },
            { 37, "ATAPI ZIP Drive boot is supported" },
            { 38, "1394 boot is supported" },
            { 39, "Smart Battery supported" }
        };

        /// <summary>
        /// Gets the BIOS information
        /// </summary>
        /// <returns>The BIOS information</returns>
        public BIOS GetBIOS()
        {
            BIOS bios = new BIOS();

            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_BIOS_SEARCH_STRING);
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    bios.BuildNumber = Convert.ToString(mo["BuildNumber"]);

                    UInt16[] capabilities = (UInt16[])mo["BiosCharacteristics"];
                    foreach(UInt16 capability in capabilities)
                    {
                        if (_BIOS_CHARACTERISTICS_DICTIONARY.ContainsKey(capability))
                            bios.Capabilities.Add(_BIOS_CHARACTERISTICS_DICTIONARY[capability]);
                    }

                    bios.CurrentLanguage = Convert.ToString(mo["CurrentLanguage"]);
                    bios.IsPrimaryBIOS = Convert.ToBoolean(mo["PrimaryBIOS"]);
                    bios.Manufacturer = Convert.ToString(mo["Manufacturer"]);
                    bios.Name = Convert.ToString(mo["Name"]);
                    bios.ReleaseDate = ManagementDateTimeConverter.ToDateTime(Convert.ToString(mo["ReleaseDate"]));
                    bios.SerialNumber = Convert.ToString(mo["SerialNumber"]);
                    bios.SmBIOSBIOSVersion = Convert.ToString(mo["SMBIOSBIOSVersion"]);
                    bios.SmBIOSMajorVersion = Convert.ToUInt16(mo["SMBIOSMajorVersion"]);
                    bios.SmBIOSMinorVersion = Convert.ToUInt16(mo["SMBIOSMinorVersion"]);
                    bios.Version = Convert.ToString(mo["Version"]);
                }
            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return bios;
        }
    }
}
