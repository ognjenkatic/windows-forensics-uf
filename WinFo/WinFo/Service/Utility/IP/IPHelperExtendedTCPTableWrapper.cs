using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility.IP
{
    // The code below was taken from http://pinvoke.net/default.aspx/iphlpapi.GetExtendedTcpTable, Nov 29th 2018
    
    [StructLayout(LayoutKind.Sequential)]
    struct MIB_TCPROW_OWNER_PID
    {
        public uint state;
        public uint localAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] localPort;
        public uint remoteAddr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] remotePort;
        public uint owningPid;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MIB_TCPTABLE_OWNER_PID
    {
        public uint dwNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        public MIB_TCPROW_OWNER_PID[] table;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MIB_TCP6ROW_OWNER_PID
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] localAddr;
        public uint localScopeId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] localPort;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] remoteAddr;
        public uint remoteScopeId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] remotePort;
        public uint state;
        public uint owningPid;
    }

    enum MIB_TCP_STATE
    {
        MIB_TCP_STATE_CLOSED,
        MIB_TCP_STATE_LISTEN,
        MIB_TCP_STATE_SYN_SENT,
        MIB_TCP_STATE_SYN_RCVD,
        MIB_TCP_STATE_ESTAB,
        MIB_TCP_STATE_FIN_WAIT1,
        MIB_TCP_STATE_FIN_WAIT2,
        MIB_TCP_STATE_CLOSE_WAIT,
        MIB_TCP_STATE_CLOSING,
        MIB_TCP_STATE_LAST_ACK,
        MIB_TCP_STATE_TIME_WAIT,
        MIB_TCP_STATE_DELETE_TCB
    }

    static class IPHelperAPI
    {
        [DllImport("iphlpapi.dll", SetLastError = true)]
        internal static extern uint GetExtendedTcpTable(
            IntPtr tcpTable,
            ref int tcpTableLength,
            bool sort,
            int ipVersion,
            TCP_TABLE_CLASS tcpTableType,
            int reserved = 0);
    }


    enum TCP_TABLE_CLASS
    {
        TCP_TABLE_BASIC_LISTENER,
        TCP_TABLE_BASIC_CONNECTIONS,
        TCP_TABLE_BASIC_ALL,
        TCP_TABLE_OWNER_PID_LISTENER,
        TCP_TABLE_OWNER_PID_CONNECTIONS,
        TCP_TABLE_OWNER_PID_ALL,
        TCP_TABLE_OWNER_MODULE_LISTENER,
        TCP_TABLE_OWNER_MODULE_CONNECTIONS,
        TCP_TABLE_OWNER_MODULE_ALL
    }


    [StructLayout(LayoutKind.Sequential)]
    struct MIB_TCP6TABLE_OWNER_PID
    {
        public uint dwNumEntries;
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
        public MIB_TCP6ROW_OWNER_PID[] table;
    }
    
    class IPHelperExtendedTCPTableWrapper : IDisposable
    {

        public const int AF_INET = 2;
        public const int AF_INET6 = 23;
        
        public IPHelperExtendedTCPTableWrapper() { }
        
        public void Dispose() { GC.SuppressFinalize(this); }

        public List<MIB_TCPROW_OWNER_PID> GetAllTCPv4Connections()
        {
            return GetTCPConnections<MIB_TCPROW_OWNER_PID, MIB_TCPTABLE_OWNER_PID>(AF_INET);
        }

        public List<MIB_TCP6ROW_OWNER_PID> GetAllTCPv6Connections()
        {
            return GetTCPConnections<MIB_TCP6ROW_OWNER_PID, MIB_TCP6TABLE_OWNER_PID>(AF_INET6);
        }

        public List<IPR> GetTCPConnections<IPR, IPT>(int ipVersion)
        { 

            IPR[] tableRows;
            int buffSize = 0;
            var dwNumEntriesField = typeof(IPT).GetField("dwNumEntries");

         
            uint ret = IPHelperAPI.GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, ipVersion, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL);
            IntPtr tcpTablePtr = Marshal.AllocHGlobal(buffSize);

            try
            {
                ret = IPHelperAPI.GetExtendedTcpTable(tcpTablePtr, ref buffSize, true, ipVersion, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL);
                if (ret != 0) return new List<IPR>();

                IPT table = (IPT)Marshal.PtrToStructure(tcpTablePtr, typeof(IPT));
                int rowStructSize = Marshal.SizeOf(typeof(IPR));
                uint numEntries = (uint)dwNumEntriesField.GetValue(table);
                
                tableRows = new IPR[numEntries];

                IntPtr rowPtr = (IntPtr)((long)tcpTablePtr + 4);
                for (int i = 0; i < numEntries; i++)
                {
                    IPR tcpRow = (IPR)Marshal.PtrToStructure(rowPtr, typeof(IPR));
                    tableRows[i] = tcpRow;
                    rowPtr = (IntPtr)((long)rowPtr + rowStructSize);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(tcpTablePtr);
            }
            return tableRows != null ? tableRows.ToList() : new List<IPR>();
        }
        
        ~IPHelperExtendedTCPTableWrapper() { Dispose(); }

    }
}

