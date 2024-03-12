using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Usage.Win10
{
    /// <summary>
    /// Service responsable for fetching information about app compat cache
    /// </summary>
    public class Win10ShimCacheService : IShimCacheService
    {
        #region fields
        private static string _SHIM_CACHE_REG_KEY = @"SYSTEM\\CurrentControlSet\\Control\\Session Manager\\AppCompatCache";
        private static int _STANDARD_HEADER_OFFSET = 48;
        private static int _CREATORS_UPDATE_HEADER_OFFSET = 52;
        private static int _SHIM_CACHE_HEADER_SIZE = 14;

        private static string _WIN_10_MAGIC = "10ts";

        #endregion
        public List<ShimCacheEntry> GetShimCacheEntries()
        {
            List<ShimCacheEntry> entries = new List<ShimCacheEntry>();

            try
            {
                int headerOffset = 0;

                RegistryKey shimCacheSubkey = Registry.LocalMachine.OpenSubKey(_SHIM_CACHE_REG_KEY);
                byte[] appCompatCacheBytes = (byte[])shimCacheSubkey.GetValue("AppCompatCache");
                
                string standardPositionMagic = System.Text.Encoding.ASCII.GetString(appCompatCacheBytes, _STANDARD_HEADER_OFFSET, 4);
                string creatorsUpdatePositionMagic = System.Text.Encoding.ASCII.GetString(appCompatCacheBytes, _CREATORS_UPDATE_HEADER_OFFSET, 4);

                if (standardPositionMagic == _WIN_10_MAGIC)
                {
                    headerOffset = _STANDARD_HEADER_OFFSET;
                } else if (creatorsUpdatePositionMagic == _WIN_10_MAGIC)
                {
                    headerOffset = _CREATORS_UPDATE_HEADER_OFFSET;
                } else
                {
                    throw new Exception("Cannot detect correct version of Win10");
                }

                string magic = _WIN_10_MAGIC;
         
                byte[] header = new byte[_SHIM_CACHE_HEADER_SIZE];
                byte[] body;
                
                Array.Copy(appCompatCacheBytes, headerOffset, header, 0, _SHIM_CACHE_HEADER_SIZE);

                int position = 0;

                do
                {
                    magic = System.Text.Encoding.ASCII.GetString(header, 0, 4);
                    UInt32 crc32 = BitConverter.ToUInt32(header, 4);
                    UInt32 entryLen = BitConverter.ToUInt32(header, 8);
                    UInt16 pathLen = BitConverter.ToUInt16(header, 12);

                    body = new byte[entryLen];
                    Array.Copy(appCompatCacheBytes, headerOffset + _SHIM_CACHE_HEADER_SIZE , body, 0, entryLen);
                    
                    
                    headerOffset += (int)entryLen + 12;
                    Array.Copy(appCompatCacheBytes, headerOffset, header, 0, _SHIM_CACHE_HEADER_SIZE);

                    ShimCacheEntry sce = new ShimCacheEntry();
                    
                    sce.LastModified = DateTime.FromFileTime((long)BitConverter.ToUInt64(body, pathLen));
                    sce.Path = System.Text.Encoding.Unicode.GetString(body, 0, pathLen);
                    sce.EntryPosition = ++position;
                    entries.Add(sce);
                } while (magic == _WIN_10_MAGIC);
            } catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return entries;
        }
    }
}
