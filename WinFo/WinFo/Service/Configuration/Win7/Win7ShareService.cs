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
    /// Service responsable for fetching information about the shares
    /// </summary>
    public class Win7ShareService : IShareService
    {
        #region fields
        private static string _SHARE_SEARCH_STRING = "SELECT Caption, Description, InstallDate, Name, Path, Type FROM Win32_Share";
        #endregion

        #region methods
        /// <summary>
        /// Gets a list of configured shares
        /// </summary>
        /// <returns>The list of shares</returns>
        public List<Share> GetShares()
        {
            List<Share> shares = new List<Share>();
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_SHARE_SEARCH_STRING);
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    Share sh = new Share();
                    sh.Caption          = Convert.ToString(mo["Caption"]);
                    sh.Description      = Convert.ToString(mo["Description"]);
                    sh.Name             = Convert.ToString(mo["Name"]);
                    sh.Path             = Convert.ToString(mo["Path"]);
                    string installDate  = Convert.ToString(mo["InstallDate"]);
                    UInt32 id         = Convert.ToUInt32(mo["Type"]);

                    if (installDate != null && installDate != "")
                        sh.InstallDate = ManagementDateTimeConverter.ToDateTime(installDate);

                    // Decode the id to string (based on online documentation)
                    switch (id)
                    {
                        case (0):
                            {
                                sh.Type = "Disk Drive";
                                break;
                            }
                        case (1):
                            {
                                sh.Type = "Print Queue";
                                break;
                            }
                        case (2):
                            {
                                sh.Type = "Device";
                                break;
                            }
                        case (3):
                            {
                                sh.Type = "IPC";
                                break;
                            }
                        case (2147483648):
                            {
                                sh.Type = "Disk Drive Admin";
                                break;
                            }
                        case (2147483649):
                            {
                                sh.Type = "Print Queue Admin";
                                break;
                            }
                        case (2147483650):
                            {
                                sh.Type = "Device Admin";
                                break;
                            }
                        case (2147483651):
                            {
                                sh.Type = "Print Queue Admin";
                                break;
                            }
                    }

                    shares.Add(sh);
                }

            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return shares;
        }
        #endregion
    }
}
