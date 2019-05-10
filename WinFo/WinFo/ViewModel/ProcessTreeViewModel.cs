using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service;
using WinFo.Service.Usage;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model for process information
    /// </summary>
    public class ProcessTreeViewModel : BaseViewModel
    {
        #region fields
        private ObservableCollection<Process> _processes                = null;
        private List<Process> _processesFlat                            = null;

        private string _selectedPID                                     = "0";
        private Process _selectedProcess                                = null;
        private SeriesCollection _processesMemoryUsageSeriesCollection  = null;
        private SeriesCollection _processesDataWrittenSeriesCollection  = null;
        private SeriesCollection _processesDataReadSeriesCollection     = null;

        private Dictionary<string, ulong> _topFiveProcessByPhysicalMemory   = null;
        private Dictionary<string, ulong> _topFiveProcessByDataWritten      = null;
        private Dictionary<string, ulong> _topFiveProcessByDataRead         = null;

        private string[] _byDataWrittenProcessNames                         = null;
        private string[] _byDataReadProcessNames                            = null;
        private string[] _byPhysicalMemoryProcessNames                      = null;
        #endregion

        #region properties
        public ObservableCollection<Process> Processes
        {
            get
            {
                return _processes;
            }
            set
            {
                if(_processes != value)
                {
                    _processes = value;
                    RaisePropertyChanged("Processes");
                }
            }
        }

        public string SelectedPID
        {
            get
            {
                return _selectedPID;
            }
            set
            {
                if (_selectedPID != value)
                {
                    _selectedPID = value;
                    RaisePropertyChanged("SelectedPID");
                    ParseSelectedProcessPID();
                }
            }
        }
        public Process SelectedProcess
        {
            get
            {
                return _selectedProcess;
            }
            set
            {
                if (_selectedProcess != value)
                {
                    _selectedProcess = value;
                    RaisePropertyChanged("SelectedProcess");
                    RaisePropertyChanged("IsProcessSelected");
                }
            }
        }

        public bool IsProcessSelected
        {
            get
            {
                return _selectedProcess != null;
            }
        }

        public SeriesCollection ProcessesDataWrittenSeriesCollection
        {
            get
            {
                return _processesDataWrittenSeriesCollection;
            }
            set
            {
                if (_processesDataWrittenSeriesCollection != value)
                {
                    _processesDataWrittenSeriesCollection = value;
                    RaisePropertyChanged("ProcessesDataWrittenSeriesCollection");
                }
            }
        }
        public SeriesCollection ProcessesDataReadSeriesCollection
        {
            get
            {
                return _processesDataReadSeriesCollection;
            }
            set
            {
                if (_processesDataReadSeriesCollection != value)
                {
                    _processesDataReadSeriesCollection = value;
                    RaisePropertyChanged("ProcessesDataReadSeriesCollection");
                }
            }
        }
        public SeriesCollection ProcessesMemoryUsageSeriesCollection
        {
            get
            {
                return _processesMemoryUsageSeriesCollection;
            }
            set
            {
                if (_processesMemoryUsageSeriesCollection != value)
                {
                    _processesMemoryUsageSeriesCollection = value;
                    RaisePropertyChanged("ProcessMemoryUsageSeriesCollection");
                }
            }
        }
        
        public string[] ByDataWrittenProcessNames
        {
            get
            {
                return _byDataWrittenProcessNames;
            }
            set
            {
                if(_byDataWrittenProcessNames != value)
                {
                    _byDataWrittenProcessNames = value;
                    RaisePropertyChanged("ByDataWrittenProcessNames");
                }
            }
        }
        public string[] ByDataReadProcessNames
        {
            get
            {
                return _byDataReadProcessNames;
            }
            set
            {
                if (_byDataReadProcessNames != value)
                {
                    _byDataReadProcessNames = value;
                    RaisePropertyChanged("ByDataReadProcessNames");
                }
            }
        }
        public string[] ByPhysicalMemoryProcessNames
        {
            get
            {
                return _byPhysicalMemoryProcessNames;
            }
            set
            {
                if (_byPhysicalMemoryProcessNames != value)
                {
                    _byPhysicalMemoryProcessNames = value;
                    RaisePropertyChanged("ByPhysicalMemoryProcessNames");
                }
            }
        }
        #endregion
        #region constructor
        public ProcessTreeViewModel()
        {
            _processesFlat = new List<Process>();
            _topFiveProcessByDataRead = new Dictionary<string, ulong>();
            _topFiveProcessByDataWritten = new Dictionary<string, ulong>();
            _topFiveProcessByPhysicalMemory = new Dictionary<string, ulong>();
            _processes = new ObservableCollection<Process>();

            _processesDataReadSeriesCollection = new SeriesCollection();
            _processesDataWrittenSeriesCollection = new SeriesCollection();
            _processesMemoryUsageSeriesCollection = new SeriesCollection();

            AsyncUpdateProcessTreeInformation();
        }
        #endregion
        private void ParseSelectedProcessPID()
        {
            uint pid = 0;
            if (UInt32.TryParse(_selectedPID, out pid)){

                foreach(Process proc in  _processesFlat.Where(o => o.Pid == pid))
                {
                    SelectedProcess = proc;
                    break;
                }
            };

        }
        
        private ColumnSeries CreateColumnSeries(string title, Dictionary<string,ulong> dataDictionary)
        {
            ColumnSeries columnSeries = new ColumnSeries
            {
                Title = title,
                Values = new ChartValues<long>()
            };

            foreach (KeyValuePair<string, ulong> kvp in dataDictionary)
            {
                columnSeries.Values.Add((long)kvp.Value / 1024 / 1024);
            }

            return columnSeries;
        }

        public async void AsyncUpdateProcessTreeInformation()
        {
            IsModelInformationBeingUpdated = true;
            ModelInformationUpdateProgress = "Fetching process data...";
            List<Process> processList = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();
                IProcessService ps = sf.CreateProcessService();
                return ps.GetProcesses();
            });

            ModelInformationUpdateProgress = $"Loaded information about {processList.Count} processes!";
            if (processList != null && processList.Count > 0)
            {
                _processesFlat.Clear();
                _topFiveProcessByDataRead.Clear();
                _topFiveProcessByDataWritten.Clear();
                _topFiveProcessByPhysicalMemory.Clear();
                _processes.Clear();
                SelectedProcess = null;
                

                ProcessesDataReadSeriesCollection.Clear();
                ProcessesMemoryUsageSeriesCollection.Clear();
                ProcessesDataWrittenSeriesCollection.Clear();

                processList = processList.OrderByDescending(o => o.PhysicalMemory).ToList();

                foreach (Process proc in processList)
                {
                    _processesFlat.Add(proc);
                    if (proc.IsOrphanProcess)
                        Processes.Add(proc);
                }

                for (int i = 0; i < 5; i++)
                    _topFiveProcessByPhysicalMemory.Add(processList.ElementAt(i).ProcessName + " - " + processList.ElementAt(i).Pid, processList.ElementAt(i).PhysicalMemory);

                processList = processList.OrderByDescending(o => o.WriteAmount).ToList();

                for (int i = 0; i < 5; i++)
                    _topFiveProcessByDataWritten.Add(processList.ElementAt(i).ProcessName + " - " + processList.ElementAt(i).Pid, processList.ElementAt(i).WriteAmount);

                processList = processList.OrderByDescending(o => o.ReadAmount).ToList();

                for (int i = 0; i < 5; i++)
                    _topFiveProcessByDataRead.Add(processList.ElementAt(i).ProcessName + " - " + processList.ElementAt(i).Pid, processList.ElementAt(i).ReadAmount);

                ByPhysicalMemoryProcessNames = _topFiveProcessByPhysicalMemory.Keys.ToArray();
                ByDataWrittenProcessNames = _topFiveProcessByDataWritten.Keys.ToArray();
                ByDataReadProcessNames = _topFiveProcessByDataRead.Keys.ToArray();

                ProcessesDataReadSeriesCollection.Add(CreateColumnSeries("Data Read (MB) ", _topFiveProcessByDataRead));
                ProcessesMemoryUsageSeriesCollection.Add(CreateColumnSeries("Memory used (MB) ", _topFiveProcessByPhysicalMemory));
                ProcessesDataWrittenSeriesCollection.Add(CreateColumnSeries("Data Written (MB) ", _topFiveProcessByDataWritten));
            }

            IsModelInformationBeingUpdated = false;
        }
        

        
    }
}
