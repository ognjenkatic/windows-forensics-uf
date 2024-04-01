using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Model.Usage
{
    /// <summary>
    /// Represents a running process
    /// </summary>
    public class Process
    {
        #region fields
        private List<TCPConnection> _tcpConnections = new List<TCPConnection>();
        private List<Process> _childProcesses = new List<Process>();
        private uint _parentPid;
        private uint _pid;
        private string _mainWindowTitle;
        private ulong _physicalMemory;
        private DateTime _startTime;
        private ulong _peakPagedMemorySize;
        private ulong _pagedMemorySize;
        private ulong _peakPhysicalMemory;
        private string _fileName;
        private string _processName;
        private string _commandLine;
        private ulong _readCount;
        private ulong _writeCount;
        private string _user;
        private string _domain;
        private ulong _readAmount;
        private ulong _writeAmount;
        private UInt32 _sessionId;
        private bool _isOrphanProcess = true;
        #endregion

        #region properties
        public List<TCPConnection> TCPConnections { get => _tcpConnections; set => _tcpConnections = value; }
        public uint Pid { get => _pid; set => _pid = value; }
        public string MainWindowTitle { get => _mainWindowTitle; set => _mainWindowTitle = value; }

        //Unit - byte
        public ulong PhysicalMemory { get => _physicalMemory; set => _physicalMemory = value; }
        public DateTime StartTime { get => _startTime; set => _startTime = value; }
        public ulong PeakPagedMemorySize { get => _peakPagedMemorySize; set => _peakPagedMemorySize = value; }
        public ulong PagedMemorySize { get => _pagedMemorySize; set => _pagedMemorySize = value; }
        
        //Unit - kilobyte
        public ulong PeakPhysicalMemory { get => _peakPhysicalMemory; set => _peakPhysicalMemory = value; }
        public string FileName { get => _fileName; set => _fileName = value; }
        public string ProcessName { get => _processName; set => _processName = value; }
        public List<Process> ChildProcesses { get => _childProcesses; set => _childProcesses = value; }
        public uint ParentPid { get => _parentPid; set => _parentPid = value; }
        public string CommandLine { get => _commandLine; set => _commandLine = value; }
        public ulong ReadCount { get => _readCount; set => _readCount = value; }
        public ulong WriteCount { get => _writeCount; set => _writeCount = value; }
        public string User { get => _user; set => _user = value; }
        public string Domain { get => _domain; set => _domain = value; }
        public ulong ReadAmount { get => _readAmount; set => _readAmount = value; }
        public ulong WriteAmount { get => _writeAmount; set => _writeAmount = value; }
        
        public int AssociatedConnectionsCount
        {
            get
            {
                int sum = _tcpConnections.Count;
                foreach (Process child in _childProcesses)
                    sum += child.AssociatedConnectionsCount;
                return sum;
            }
        }

        public ulong AssociatedPhysicalMemory
        {
            get
            {
                ulong sum = _physicalMemory;
                foreach (Process child in _childProcesses)
                    sum += child.AssociatedPhysicalMemory;
                return sum;
            }
        }

        public string AssociatedPhysicalMemoryPrettyPrint
        {
            get
            {
                return AssociatedPhysicalMemory / 1024 / 1024 + " MB";
            }
        }

        public string PagedMemoryPrettyPrint
        {
            get
            {
                return _pagedMemorySize / 1024 / 1024 + " MB";
            }
        }

        public string PeakPagedMemoryPrettyPrint
        {
            get
            {
                return _peakPagedMemorySize / 1024 / 1024 + " MB";
            }
        }

        public string PeakPhysicalMemoryPrettyPrint
        {
            get
            {
                return _peakPhysicalMemory / 1024 + " MB";
            }
        }
        public string PhysicalMemoryPrettyPrint
        {
            get
            {
                return _physicalMemory / 1024 / 1024 + " MB";
            }
        }

        public string ReadAmountPrettyPrint
        {
            get
            {
                return _readAmount / 1024 / 1024 + " MB and " + (_readAmount % (1024 * 1024)) + " Bytes";
            }
        }

        public string WriteAmountPrettyPrint
        {
            get
            {
                return _writeAmount / 1024 / 1024 + " MB and "+ (_writeAmount % (1024 * 1024)) +" Bytes";
            }
        }

        public uint SessionId { get => _sessionId; set => _sessionId = value; }
        public bool IsOrphanProcess { get => _isOrphanProcess; set => _isOrphanProcess = value; }
        #endregion
    }
}
