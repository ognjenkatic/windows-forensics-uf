using Microsoft.Win32;
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
    public class Win7RecentDocumentService : IRecentDocumentService
    {
        #region fields
        private static string _RECENT_DOCUMENTS = @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\RecentDocs";
        #endregion

        /// <summary>
        /// Get a list of recent files by parsing the windows registry
        /// </summary>
        /// <returns>The list of recent files</returns>
        public List<RecentDocument> GetRecentDocuments()
        {
            List<RecentDocument> recentDocuments = new List<RecentDocument>();

            try
            {
                RegistryKey recentDocumentsRegistryKey = Registry.CurrentUser.OpenSubKey(_RECENT_DOCUMENTS);

                string[] subkeys = recentDocumentsRegistryKey.GetSubKeyNames();

                foreach (string subkeyName in subkeys)
                {
                    RegistryKey subkey = recentDocumentsRegistryKey.OpenSubKey(subkeyName);

                    string[] documentEntries = subkey.GetValueNames();

                    
                    foreach (string entryName in documentEntries)
                    {
                        int id = -1;
                        if (!int.TryParse(entryName, out id))
                        {
                            continue;
                        }
                        object obVal = subkey.GetValue(entryName, RegistryValueKind.Binary);

                        
                        byte[] bytes = (byte[])obVal;

                        MemoryStream ms = new MemoryStream();
                        
                        //TO-DO Decode the rest of the data (if possible) in the array, this only extracts the file name 
                        for(int i = 0; i < bytes.Length-1; i++)
                        {
                            if (i>0 && (bytes[i-1] | bytes[i] | bytes[i+1]) == 0)
                            {
                                break;
                            } else
                            {
                                ms.Write(bytes, i, 1);
                            }
                        }
                        
                        string strVal = System.Text.Encoding.Unicode.GetString(ms.ToArray());

                        RecentDocument rd = new RecentDocument();
                        rd.Name = strVal;
                        rd.Extension = Path.GetExtension(strVal);

                        recentDocuments.Add(rd);
                    }
                }
                
            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }
            
            MyDebugger.Instance.LogMessage($"Loaded {recentDocuments.Count} recent documents.", DebugVerbocity.Informational);
            return recentDocuments;
        }
    }
}
