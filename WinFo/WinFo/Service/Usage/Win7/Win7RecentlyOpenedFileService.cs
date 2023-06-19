using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;
using WinFo.Service.Utility;
using WinFo.Service.Utility.Misc;

namespace WinFo.Service.Usage.Win7
{
    public class Win7RecentlyOpenedFileService : IRecentlyOpenedFileService
    {
        #region fields
        private static string _RECENTLY_OPENED_PATH = @"%USERPROFILE%\\AppData\\Roaming\\Microsoft\\Windows\Recent";

        public event UpdateProgressDelegate UpdateProgress;
        #endregion

        /// <summary>
        /// Get a list of recently used opened files by parsing the contents of the windows recent items folder
        /// </summary>
        /// <returns>The list of recently opened files</returns>
        public List<OpenedFileEntry> GetRecentlyOpenedFiles()
        {
            List<OpenedFileEntry> recentlyOpenedFiles = new List<OpenedFileEntry>();

            int counter = 0;
            try
            {
                string recentFolderPath = Environment.ExpandEnvironmentVariables(_RECENTLY_OPENED_PATH);
                IEnumerable<string> recentFiles = Directory.EnumerateFiles(recentFolderPath);

                foreach (string file in recentFiles)
                {
                    try
                    {
                        
                        if (file.EndsWith("lnk"))
                        {
                            UpdateProgress?.Invoke($"Searching for recently opened file entries, found so far: {++counter}");
                            IWshShell shell = new WshShell();
                            if (shell.CreateShortcut(file) is IWshShortcut lnk)
                            {
                                OpenedFileEntry ofe = new OpenedFileEntry();

                                ofe.Name = Path.GetFileName(lnk.TargetPath);
                                ofe.Path = lnk.TargetPath;
                                ofe.Shortcut = file;
                                
                                if (System.IO.File.Exists(lnk.TargetPath)) {
                                    ofe.Accessed = System.IO.File.GetLastAccessTime(lnk.TargetPath);
                                    ofe.Created = System.IO.File.GetCreationTime(lnk.TargetPath);
                                    ofe.Creator = System.IO.File.GetAccessControl(lnk.TargetPath).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                                    ofe.Exists = true;
                                } else
                                {
                                    ofe.Exists = false;
                                    ofe.Accessed = ofe.Created = System.IO.File.GetCreationTime(lnk.FullName);
                                    ofe.Name = lnk.FullName;
                                    ofe.Path = lnk.WorkingDirectory;
                                }

                                recentlyOpenedFiles.Add(ofe);
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
                    }
                }

            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            string logMessage = $"Loaded {recentlyOpenedFiles.Count} recently opened entries.";
            MyDebugger.Instance.LogMessage(logMessage, DebugVerbocity.Informational);
            UpdateProgress?.Invoke(logMessage);
            return recentlyOpenedFiles;
        }
    }
}
