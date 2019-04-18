using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win7
{
    public class Win7RecentlyOpenedFileService : IRecentlyOpenedFileService
    {
        #region fields
        private static string _RECENTLY_OPENED_PATH = @"%USERPROFILE%\\AppData\\Roaming\\Microsoft\\Windows\Recent";
        #endregion

        /// <summary>
        /// Get a list of recently used opened files by parsing the contents of the windows recent items folder
        /// </summary>
        /// <returns>The list of recently opened files</returns>
        public List<OpenedFileEntry> GetRecentlyOpenedFiles()
        {
            List<OpenedFileEntry> recentlyOpenedFiles = new List<OpenedFileEntry>();

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
                            IWshShell shell = new WshShell();
                            if (shell.CreateShortcut(file) is IWshShortcut lnk)
                            {
                                OpenedFileEntry ofe = new OpenedFileEntry();

                                ofe.Name = Path.GetFileName(lnk.TargetPath);
                                ofe.Path = lnk.TargetPath;
                                ofe.Shortcut = file;

                                Console.WriteLine($"FN:{lnk.FullName}, ARG:{lnk.Arguments},DSC:{lnk.Description},HKEY:{lnk.Hotkey},WD:{lnk.WorkingDirectory}");
                                if (System.IO.File.Exists(lnk.TargetPath)) {
                                    ofe.Accessed = System.IO.File.GetLastAccessTime(lnk.TargetPath);
                                    ofe.Created = System.IO.File.GetCreationTime(lnk.TargetPath);
                                    ofe.Creator = System.IO.File.GetAccessControl(lnk.TargetPath).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                                    ofe.Exists = true;
                                    MyDebugger.Instance.LogMessage($"Recently used file {ofe.Name} is available at {ofe.Path}.", DebugVerbocity.Informational);
                                } else
                                {
                                    ofe.Exists = false;
                                    ofe.Accessed = ofe.Created = DateTime.MinValue;
                                    ofe.Name = lnk.FullName;
                                    ofe.Path = lnk.WorkingDirectory;
                                    MyDebugger.Instance.LogMessage($"Recently used file {ofe.Name} is not available, was at {ofe.Path}.", DebugVerbocity.Informational);
                                }

                                recentlyOpenedFiles.Add(ofe);
                            }
                        } else
                        {
                            MyDebugger.Instance.LogMessage($"Non link file {file} found.", DebugVerbocity.Informational);
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

            return recentlyOpenedFiles;
        }
    }
}
