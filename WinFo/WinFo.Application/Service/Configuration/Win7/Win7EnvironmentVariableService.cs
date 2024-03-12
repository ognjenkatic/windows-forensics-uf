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
    /// Service responsable for fetching information about the environment variables
    /// </summary>
    public class Win7EnvironmentVariableService : IEnvironmentVariableService
    {
        #region fields

        private static string _ENVIRONMENT_VARIABLE_SEARCH_STRING = "SELECT Name, SystemVariable, VariableValue, UserName FROM Win32_Environment";
        #endregion

        #region methods
        /// <summary>
        /// Gets the list of environment variables
        /// </summary>
        /// <returns> The list of environment variables</returns>
        public List<EnvironmentVariable> GetEnvironmentVariables()
        {
            List<EnvironmentVariable> environmentVariables = new List<EnvironmentVariable>();

            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(_ENVIRONMENT_VARIABLE_SEARCH_STRING);
                ManagementObjectCollection moc = mos.Get();

                foreach(ManagementObject mo in moc)
                {
                    EnvironmentVariable ev = new EnvironmentVariable();
                    ev.Key              = Convert.ToString(mo["Name"]);
                    ev.Value            = Convert.ToString(mo["VariableValue"]);
                    ev.Username         = Convert.ToString(mo["UserName"]);
                    ev.IsSystemVariable = Convert.ToBoolean(mo["SystemVariable"]);

                    environmentVariables.Add(ev);
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return environmentVariables;
        }
        #endregion
    }
}
