using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win10
{
    public class Win10RecycleBinService : IRecycleBinService
    {
        private static string _RECYCLE_BIN_PATH = @"C:\$Recycle.Bin";
        public List<RecycleBinEntry> GetRecycleBinEntries()
        {
            List<RecycleBinEntry> retval = new List<RecycleBinEntry>();

            
            foreach (string dirname in Directory.EnumerateDirectories(_RECYCLE_BIN_PATH))
            {
                try
                {
                    string[] filePaths = Directory.GetFiles(dirname);

                    foreach (string filePath in filePaths)
                    {
                        if (filePath.Contains("$I"))
                        {
                            using (FileStream fs = File.OpenRead(filePath))
                            {
                                RecycleBinEntry reb = new RecycleBinEntry();

                                byte[] buffer = File.ReadAllBytes(filePath);

                                long header = BitConverter.ToInt64(buffer, 0);
                                long deletedLong = BitConverter.ToInt64(buffer, 16);
                                int nameLength = BitConverter.ToInt32(buffer, 24);

                                reb.DeletedFIleSize = BitConverter.ToInt64(buffer, 8);
                                reb.DateTimeDeleted = DateTime.FromFileTime(deletedLong);
                                reb.FilePath = Encoding.Unicode.GetString(buffer, 28, nameLength * 2).Trim('\0');

                                retval.Add(reb);

                            }
                        }

                    }
                } catch (Exception exc)
                {
                    MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
                }

            }


          
            return retval;
        }
    }
}
