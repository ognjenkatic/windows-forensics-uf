using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;
using WinFo.Service.Utility;

namespace WinFo.Service.Usage.Win10
{
    public class Win10PrefetchService : IPrefetchService
    {
        private static string _PREFETCH_DIRECTORY_PATH = @"%SystemRoot%\\Prefetch";
        private static Dictionary<string, int> _DECODE_MAP = new Dictionary<string, int> {
            { "A_OFFSET", 0x0054 }
        };

        /// <summary>
        /// Get a list of prefetch entries
        /// </summary>
        /// <returns>The list of prefetch entries</returns>
        public List<PrefetchEntry> GetPrefetchEntries()
        {
            List<PrefetchEntry> entries = new List<PrefetchEntry>();

            try
            {
                string prefetchDirectoryPath = Environment.ExpandEnvironmentVariables(_PREFETCH_DIRECTORY_PATH);
                IEnumerable<string> prefetchFilePaths = Directory.EnumerateFiles(prefetchDirectoryPath);

                foreach(string prefetchFilePath in prefetchFilePaths)
                {
                    if(prefetchFilePath.EndsWith(".pf"))
                        entries.Add(parsePrefetchFile(prefetchFilePath));
                }

            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entries;
        }


        //Decoding based on https://github.com/libyal/libscca documentation
        //as well as https://www.forensicswiki.org/wiki/Windows_Prefetch_File_Format
        public PrefetchEntry parsePrefetchFile(string path)
        {
            PrefetchEntry entry = new PrefetchEntry();
            try
            {
                using(FileStream fs = File.OpenRead(path))
                {
                    byte[] header = new byte[8];
                    fs.Read(header, 0, 8);

                    uint decompressedDataLenght = BitConverter.ToUInt32(header, 4);

                    string type = Encoding.ASCII.GetString(header, 0, 4);

                    if (type.StartsWith("MAM"))
                    {
                        entry.PrefetchType = PrefetchFileType.MAM;
                       

                        int sizeToRead = (int)(fs.Length - 8);
                        int sizeRead = 0;
                        byte[] compressedBuffer = new byte[sizeToRead] ;
                        fs.Position = 8;

                        while (sizeToRead > 0)
                        {
                            int currentSizeRead = fs.Read(compressedBuffer, sizeRead, sizeToRead);
                           
                            if (sizeRead == 0)
                                break;

                            sizeRead += currentSizeRead;
                            sizeToRead -= currentSizeRead;
                        }

                        byte[] uncompressedBuffer = ExpressHuffmanDecompression.Decompress(compressedBuffer, (ulong)decompressedDataLenght);

                        //TO-DO handle different format versions, after implementing support for version 30 and testing
                        uint formatVersion = BitConverter.ToUInt32(uncompressedBuffer, 0);

                        entry.ExecutableName = Encoding.Unicode.GetString(uncompressedBuffer, 16, 60).TrimEnd('\0');
                        
                        
                    }
                    else
                        entry.PrefetchType = PrefetchFileType.UNKNOWN;

                    Console.WriteLine(entry.PrefetchType);
                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entry;
        }
    }

}
