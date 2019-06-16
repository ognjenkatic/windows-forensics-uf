using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility.IP
{
    // The code below was taken from https://www.pinvoke.net/default.aspx/iphlpapi.GetIpNetTable, Dec 3rd 2018
    // and modified for use in this project
 
    [StructLayout(LayoutKind.Sequential)]
    struct MIB_IPNETROW
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint dwIndex;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwPhysAddrLen;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac0;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac1;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac2;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac3;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac4;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac5;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac6;
        [MarshalAs(UnmanagedType.U1)]
        public byte mac7;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwAddr;
        [MarshalAs(UnmanagedType.U4)]
        public uint dwType;
    }

    class IPHelperNetTableWrapper
    {       
        const int MAXLEN_PHYSADDR = 8;

        [DllImport("IpHlpApi.dll")]
        [return: MarshalAs(UnmanagedType.U4)]
        static extern int GetIpNetTable(
            IntPtr pIpNetTable,
            [MarshalAs(UnmanagedType.U4)]
            ref int pdwSize,
            bool bOrder);

        const int ERROR_INSUFFICIENT_BUFFER = 122;

        

        public MIB_IPNETROW[] GetIPNetRows()
        {
            MIB_IPNETROW[] table = null;

            int bytesNeeded = 0;

            int result = GetIpNetTable(IntPtr.Zero, ref bytesNeeded, false);

            if (result != ERROR_INSUFFICIENT_BUFFER)
            {
                throw new Win32Exception(result);
            }

            IntPtr buffer = IntPtr.Zero;
            
            try
            {
                buffer = Marshal.AllocCoTaskMem(bytesNeeded);

                result = GetIpNetTable(buffer, ref bytesNeeded, false);

                if (result != 0)
                {
                    throw new Win32Exception(result);
                }

                int entries = Marshal.ReadInt32(buffer);

                IntPtr currentBuffer = new IntPtr(buffer.ToInt64() +
                   Marshal.SizeOf(typeof(int)));
                
                table = new MIB_IPNETROW[entries];

                for (int index = 0; index < entries; index++)
                {

                    table[index] = (MIB_IPNETROW)Marshal.PtrToStructure(new
                   IntPtr(currentBuffer.ToInt64() + (index *
                   Marshal.SizeOf(typeof(MIB_IPNETROW)))), typeof(MIB_IPNETROW));
                }
            }
            finally
            {

                Marshal.FreeCoTaskMem(buffer);
            }

            return table;
        
        }
    }
}
