namespace WinFo.Model.Configuration
{
    /// <summary>
    /// Information about the CPU architecture
    /// </summary>
    public class CPUInfo
    {
        #region fields
        private int _numberOfPhysicalProcessors;
        private int _numberOfLogicalProcessors;
        private string _architecture;
        #endregion
        #region properties
        public int NumberOfPhysicalProcessors { get => _numberOfPhysicalProcessors; set => _numberOfPhysicalProcessors = value; }
        public int NumberOfLogicalProcessors { get => _numberOfLogicalProcessors; set => _numberOfLogicalProcessors = value; }
        public string Architecture { get => _architecture; set => _architecture = value; }
        #endregion
    }
}
