using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Configuration.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the groups and users of the system
    /// </summary>
    public class Win7GroupUserService : IGroupUserService
    {
        #region fields
        private static string _USERS_SEARCH_STRING = "SELECT PartComponent, GroupComponent FROM Win32_GroupUser";
        #endregion

        /// <summary>
        /// Gets the user group to user dictionary
        /// </summary>
        /// <returns>The dictionary containing the users mapped to their groups</returns>
        public Dictionary<UserGroup, List<User>> GetGroupUserDictionary()
        {
            Dictionary<UserGroup, List<User>> groupUserDictionary = new Dictionary<UserGroup, List<User>>();
            
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_USERS_SEARCH_STRING);

                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    ManagementPath partPath =
                        new ManagementPath(mo["PartComponent"].ToString());
                    ManagementPath groupPath =
                        new ManagementPath(mo["GroupComponent"].ToString());

                    List<User> users = new List<User>();
                    if (partPath.ClassName == "Win32_UserAccount" && groupPath.ClassName == "Win32_Group")
                    {
                        ManagementObject mr = new ManagementObject(partPath);
                        ManagementObject gr = new ManagementObject(groupPath);

                        UserGroup userGroup = new UserGroup();

                        userGroup.Description       = Convert.ToString(gr["Description"]);
                        userGroup.Sid               = Convert.ToString(gr["SID"]);
                        userGroup.Name              = Convert.ToString(gr["Name"]);
                        userGroup.IsLocalAccount    = Convert.ToBoolean(gr["LocalAccount"]);

                        User user = new User();
                        user.UserName               = mr["Name"].ToString();
                        user.Sid                    = mr["SID"].ToString();
                        user.Domain                 = mr["Domain"].ToString();
                        user.FullName               = mr["FullName"].ToString();
                        user.IsDisabled             = Convert.ToBoolean(mr["Disabled"]);
                        user.IsLocalAccount         = Convert.ToBoolean(mr["LocalAccount"]);
                        user.IsLockedOut            = Convert.ToBoolean(mr["Lockout"]);
                        user.PasswordChangeable     = Convert.ToBoolean(mr["PasswordChangeable"]);
                        user.PasswordExpires        = Convert.ToBoolean(mr["PasswordExpires"]);
                        user.PasswordRequired       = Convert.ToBoolean(mr["PasswordRequired"]);
                        user.Status                 = Convert.ToString(mr["Status"]);
                       
                        if (!groupUserDictionary.ContainsKey(userGroup))
                            groupUserDictionary.Add(userGroup, new List<User>());

                        groupUserDictionary[userGroup].Add(user);
                    }
                }

            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return groupUserDictionary;
        }
    }
}
