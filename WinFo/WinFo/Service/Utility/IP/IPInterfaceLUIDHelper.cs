using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility.IP
{
    public class IPInterfaceLUIDHelper
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern int ConvertInterfaceLuidToAlias(
            byte[] interfaceLuid,
            byte[] interfaceAlias,
            int length);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern int ConvertInterfaceLuidToGuid(
            byte[] interfaceLuid,
            out Guid guid);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern int ConvertInterfaceLuidToIndex(
           byte[] interfaceLuid,
           out int index);


        public static Guid GetInterfaceGuidFromLuid(Int64 luid)
        {
            Guid guid;
            var res = ConvertInterfaceLuidToGuid(BitConverter.GetBytes(luid), out guid);
            return guid;
        }

        public static int GetInterfaceIndexFromLuid(Int64 luid)
        {
            int index;
            var res = ConvertInterfaceLuidToIndex(BitConverter.GetBytes(luid), out index);
            return index;
        }

        public static string GetInterfaceAliasFromLuid(Int64 luid)
        {
            //TO-DO hard coded max length, double check with NDIS_IF_MAX_STRING_SIZE
            int bufferLength = 100;
            byte[] buffer = new byte[bufferLength];
            var res = ConvertInterfaceLuidToAlias(BitConverter.GetBytes(luid), buffer, bufferLength);
            string alias = Encoding.Unicode.GetString(buffer).Trim('\0');
            return alias;
        }

    }
}
