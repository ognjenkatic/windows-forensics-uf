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
        private static string _SHARE_SEARCH_STRING = "SELECT Caption, Description, InstallDate, Name, Path, Type, Status FROM Win32_Share";
        private static string _SHARES_SEARCH_STRING = "SELECT Share, SharedElement FROM Win32_ShareToDirectory";
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
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_SHARES_SEARCH_STRING);

                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    ManagementPath sharePath =
                        new ManagementPath(mo["Share"].ToString());
                    ManagementPath sharedElementPath =
                        new ManagementPath(mo["SharedElement"].ToString());

               
                    if (sharePath.ClassName == "Win32_Share" && sharedElementPath.ClassName == "Win32_Directory")
                    {
                        ManagementObject sh = new ManagementObject(sharePath);
                        ManagementObject se = new ManagementObject(sharedElementPath);

                        Share share = new Share();

                        share.Caption = Convert.ToString(sh["Caption"]);
                        share.Description = Convert.ToString(sh["Description"]);
                        share.Name = Convert.ToString(sh["Name"]);
                        share.Path = Convert.ToString(sh["Path"]);
                        share.Status = Convert.ToString(sh["Status"]);
                        UInt32 id = Convert.ToUInt32(sh["Type"]);
                        
                        string creationDate = Convert.ToString(se["CreationDate"]);
                        string lastAccessed = Convert.ToString(se["LastAccessed"]);
                        string lastModified = Convert.ToString(se["LastModified"]);

                        if (creationDate != null && creationDate != "")
                            share.Creation = ManagementDateTimeConverter.ToDateTime(creationDate);

                        if (lastAccessed != null && lastAccessed != "")
                            share.LastAccessed = ManagementDateTimeConverter.ToDateTime(lastAccessed);

                        if (lastModified != null && lastModified != "")
                            share.LastModified = ManagementDateTimeConverter.ToDateTime(lastModified);

                        share.Hidden = Convert.ToBoolean(se["Hidden"]);
                        share.Drive = Convert.ToString(se["Drive"]);

                        // Decode the id to string (based on online documentation)
                        switch (id)
                        {
                            case (0):
                                {
                                    share.Type = "Disk Drive";
                                    break;
                                }
                            case (1):
                                {
                                    share.Type = "Print Queue";
                                    break;
                                }
                            case (2):
                                {
                                    share.Type = "Device";
                                    break;
                                }
                            case (3):
                                {
                                    share.Type = "IPC";
                                    break;
                                }
                            case (2147483648):
                                {
                                    share.Type = "Disk Drive Admin";
                                    break;
                                }
                            case (2147483649):
                                {
                                    share.Type = "Print Queue Admin";
                                    break;
                                }
                            case (2147483650):
                                {
                                    share.Type = "Device Admin";
                                    break;
                                }
                            case (2147483651):
                                {
                                    share.Type = "Print Queue Admin";
                                    break;
                                }
                        }

                        shares.Add(share);
                    }
                }

                MyDebugger.Instance.LogMessage($"Loaded information about {shares.Count} shares!", DebugVerbocity.Informational);
         
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
