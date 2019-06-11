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
            { "A_OFFSET", 0x0054 },
            { "A_COUNT" , 0x0058 },
            { "C_OFFSET" , 0x0064 },
            { "C_LENGTH" , 0x0068 },
            { "D_OFFSET" , 0x006C },
            { "D_COUNT"  , 0x0070 },
            { "D_LENGTH" , 0x0074 },
            { "LATEST_EXECUTION", 0x0080 },
            { "LATEST_EXECUTION_2", 0x0088 },
            { "LATEST_EXECUTION_3", 0x0090 },
            { "LATEST_EXECUTION_4", 0x0098 },
            { "LATEST_EXECUTION_5", 0x00A0 },
            { "LATEST_EXECUTION_6", 0x00A8 },
            { "LATEST_EXECUTION_7", 0x00B0 },
            { "LATEST_EXECUTION_8", 0x00B0 },
            { "EXECUTION_COUNT", 0x00D0 }
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

                        byte[] uncompressedBuffer = ExpressHuffmanDecompression.Decompress(compressedBuffer, (int)decompressedDataLenght);

                        //TO-DO handle different format versions, after implementing support for version 30 and testing
                        uint formatVersion = BitConverter.ToUInt32(uncompressedBuffer, 0);

                        entry.ExecutableName = Encoding.Unicode.GetString(uncompressedBuffer, 16, 60).TrimEnd('\0');
                        entry.ExecutionCount = BitConverter.ToInt32(uncompressedBuffer, _DECODE_MAP["EXECUTION_COUNT"]);
                        entry.PrefetchFilePath = path;

                        int aOffset = BitConverter.ToInt32(uncompressedBuffer, _DECODE_MAP["A_OFFSET"]);
                        int aCount = BitConverter.ToInt32(uncompressedBuffer, _DECODE_MAP["A_COUNT"]);

                        
                        int cOffset = BitConverter.ToInt32(uncompressedBuffer, _DECODE_MAP["C_OFFSET"]);
                        int cCount = BitConverter.ToInt32(uncompressedBuffer, _DECODE_MAP["C_LENGTH"]);

                        int dOffset = BitConverter.ToInt32(uncompressedBuffer, _DECODE_MAP["D_OFFSET"]);
                        int dLength = BitConverter.ToInt32(uncompressedBuffer, _DECODE_MAP["D_LENGTH"]);
                        int dCount = BitConverter.ToInt32(uncompressedBuffer, _DECODE_MAP["D_COUNT"]);

                        long latestExecutionLong  = BitConverter.ToInt64(uncompressedBuffer, _DECODE_MAP["LATEST_EXECUTION"]);
                        long latestExecutionLong2 = BitConverter.ToInt64(uncompressedBuffer, _DECODE_MAP["LATEST_EXECUTION_2"]); 
                        long latestExecutionLong3 = BitConverter.ToInt64(uncompressedBuffer, _DECODE_MAP["LATEST_EXECUTION_3"]); 
                        long latestExecutionLong4 = BitConverter.ToInt64(uncompressedBuffer, _DECODE_MAP["LATEST_EXECUTION_4"]); 
                        long latestExecutionLong5 = BitConverter.ToInt64(uncompressedBuffer, _DECODE_MAP["LATEST_EXECUTION_5"]); 
                        long latestExecutionLong6 = BitConverter.ToInt64(uncompressedBuffer, _DECODE_MAP["LATEST_EXECUTION_6"]); 
                        long latestExecutionLong7 = BitConverter.ToInt64(uncompressedBuffer, _DECODE_MAP["LATEST_EXECUTION_7"]); 
                        long latestExecutionLong8 = BitConverter.ToInt64(uncompressedBuffer, _DECODE_MAP["LATEST_EXECUTION_8"]); 

                        entry.LatestExecutionTime  = DateTime.FromFileTime(latestExecutionLong);
                        entry.LatestExecutionTime2 = DateTime.FromFileTime(latestExecutionLong2);
                        entry.LatestExecutionTime3 = DateTime.FromFileTime(latestExecutionLong3);
                        entry.LatestExecutionTime4 = DateTime.FromFileTime(latestExecutionLong4);
                        entry.LatestExecutionTime5 = DateTime.FromFileTime(latestExecutionLong5);
                        entry.LatestExecutionTime6 = DateTime.FromFileTime(latestExecutionLong6);
                        entry.LatestExecutionTime7 = DateTime.FromFileTime(latestExecutionLong7);
                        entry.LatestExecutionTime8 = DateTime.FromFileTime(latestExecutionLong8);


                        entry.FilesLoaded = Encoding.Unicode.GetString(uncompressedBuffer, cOffset , cCount).TrimEnd('\0').Split('\0');

                        entry.FileMetrics = new List<PrefetchFileMetricsEntry>();

                        for (int i = 0; i < aCount; i++)
                        {
                            PrefetchFileMetricsEntry pfme = new PrefetchFileMetricsEntry();
                            pfme.StartTimeMillis = BitConverter.ToInt32(uncompressedBuffer, aOffset + i * 32);
                            pfme.DurationTimeMillis = BitConverter.ToInt32(uncompressedBuffer, aOffset + 4 +  i * 32);
                            pfme.AverageTimeMillis = BitConverter.ToInt32(uncompressedBuffer, aOffset + 8 +   i * 32);

                            pfme.Filename = entry.FilesLoaded[i];

                            entry.FileMetrics.Add(pfme);
                        }

                        entry.VolumeInformation = new List<PrefetchVolumeInformationEntry>();
                        for(int i = 0; i < dCount; i++)
                        {
                            PrefetchVolumeInformationEntry pdie = new PrefetchVolumeInformationEntry();
                            uint serial = BitConverter.ToUInt32(uncompressedBuffer, dOffset + 0x0010 + 96 * i);
                            long volumeCreationTime = BitConverter.ToInt64(uncompressedBuffer, dOffset + 0x0008 + 96 * i);

                            int fOffset = BitConverter.ToInt32(uncompressedBuffer, dOffset + 0x001C + 96 * i);
                            int fCount = BitConverter.ToInt32(uncompressedBuffer, dOffset + 0x0020 + 96 * i);

                            int dirnameOffset = 0;

                            pdie.DirectoriesLoaded = new string[fCount];
                            for (int j = 0; j < fCount; j++)
                            {
                                int dirnameCharCount = BitConverter.ToInt16(uncompressedBuffer, dOffset + fOffset + dirnameOffset);
                                dirnameOffset += 2;
                                pdie.DirectoriesLoaded[j] = Encoding.Unicode.GetString(uncompressedBuffer, dOffset + fOffset + dirnameOffset + 2, dirnameCharCount * 2).TrimEnd('\0');
                                dirnameOffset += dirnameCharCount*2;

                                
                            }
                            pdie.CreationTime = DateTime.FromFileTime(volumeCreationTime);
                            pdie.SerialNumber = String.Format("{0:X}", serial);

                            entry.VolumeInformation.Add(pdie);
                        }
                    }
                    else
                        entry.PrefetchType = PrefetchFileType.UNKNOWN;

                }
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entry;
        }
    }

}
