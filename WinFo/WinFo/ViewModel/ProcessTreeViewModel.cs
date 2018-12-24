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
        private ObservableCollection<Process> _processes = new ObservableCollection<Process>();
        private List<Process> _processesFlat = new List<Process>();

        private string _selectedPID;
        private Process _selectedProcess;
        private SeriesCollection _processesMemoryUsageSeriesCollection = new SeriesCollection();
        private SeriesCollection _processesDataWrittenSeriesCollection = new SeriesCollection();
        private SeriesCollection _processesDataReadSeriesCollection = new SeriesCollection();
        private Dictionary<string, ulong> _topFiveProcessByPhysicalMemory = new Dictionary<string, ulong>();
        private Dictionary<string, ulong> _topFiveProcessByDataWritten = new Dictionary<string, ulong>();
        private Dictionary<string, ulong> _topFiveProcessByDataRead = new Dictionary<string, ulong>();

        private string[] _byDataWrittenProcessNames;
        private string[] _byDataReadProcessNames;

        private string[] _processNames;

        public string[] ProcessNames
        {
            get
            {
                return _processNames;
            }
            set
            {
                if(_processNames != value)
                {
                    _processNames = value;
                    RaisePropertyChanged("ProcessNames");
                }
            }
        }
        public ObservableCollection<Process> Processes { get => _processes; set => _processes = value; }

        public bool IsProcessSelected
        {
            get
            {
                return _selectedProcess != null;
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
                if(_selectedProcess != value)
                {
                    _selectedProcess = value;
                    RaisePropertyChanged("SelectedProcess");
                    RaisePropertyChanged("IsProcessSelected");
                }
            }
        }

        public Dictionary<string, ulong> TopFiveProcessByPhysicalMemory
        {
            get
            {
                return _topFiveProcessByPhysicalMemory;
            }
            set
            {
                if(_topFiveProcessByPhysicalMemory != value)
                {
                    _topFiveProcessByPhysicalMemory = value;
                    RaisePropertyChanged("TopTenProcessByPhysicalMemory");
                }
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

        public string SelectedPID
        {
            get
            {
                return _selectedPID;
            }
            set
            {
                if(_selectedPID != value)
                {
                    _selectedPID = value;
                    RaisePropertyChanged("SelectedPID");
                    TryFetchProcessByPID();
                }
            }
        }

        private void TryFetchProcessByPID()
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
        public async void UpdateProcessTreeInformation()
        {
            IsModelInformationBeingUpdated = true;
            List<Process> processList = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();
                IProcessService ps = sf.CreateProcessService();

                return ps.GetProcesses();
            });

            processList = processList.OrderByDescending(o => o.PhysicalMemory).ToList();

            foreach (Process proc in processList)
            {
                _processesFlat.Add(proc);
                if (proc.IsOrphanProcess)
                    Processes.Add(proc);
            }

            for (int i = 0; i < 5; i++)
                TopFiveProcessByPhysicalMemory.Add(processList.ElementAt(i).ProcessName+" - "+processList.ElementAt(i).Pid, processList.ElementAt(i).PhysicalMemory);

            ColumnSeries cs = new ColumnSeries();
            cs.Title = "Memory used (MB) ";
            cs.Values = new ChartValues<long>();

            foreach (KeyValuePair<string, ulong> kvp in TopFiveProcessByPhysicalMemory)
            {
                cs.Values.Add((long)kvp.Value / 1024 / 1024);
            }

            processList = processList.OrderByDescending(o => o.WriteAmount).ToList();

            for(int i=0;i<5;i++)
                _topFiveProcessByDataWritten.Add(processList.ElementAt(i).ProcessName + " - " + processList.ElementAt(i).Pid, processList.ElementAt(i).WriteAmount);

            processList = processList.OrderByDescending(o => o.ReadAmount).ToList();

            for(int i=0;i<5;i++)
                _topFiveProcessByDataRead.Add(processList.ElementAt(i).ProcessName + " - " + processList.ElementAt(i).Pid, processList.ElementAt(i).ReadAmount);


            ColumnSeries cs1 = new ColumnSeries();
            cs1.Title = "Data Written (MB) ";
            cs1.Values = new ChartValues<long>();

            foreach (KeyValuePair<string, ulong> kvp in _topFiveProcessByDataWritten)
            {
                cs1.Values.Add((long)kvp.Value / 1024 / 1024);
            }

            ColumnSeries cs2 = new ColumnSeries();
            cs2.Title = "Data Read (MB) ";
            cs2.Values = new ChartValues<long>();

            foreach (KeyValuePair<string, ulong> kvp in _topFiveProcessByDataRead)
            {
                cs2.Values.Add((long)kvp.Value / 1024 / 1024);
            }

            ProcessNames = TopFiveProcessByPhysicalMemory.Keys.ToArray();
            ByDataWrittenProcessNames = _topFiveProcessByDataWritten.Keys.ToArray();
            ByDataReadProcessNames = _topFiveProcessByDataRead.Keys.ToArray();

            ProcessesDataReadSeriesCollection.Add(cs2);
            ProcessesMemoryUsageSeriesCollection.Add(cs);
            ProcessesDataWrittenSeriesCollection.Add(cs1);

            IsModelInformationBeingUpdated = false;
        }
        public ProcessTreeViewModel()
        {
            UpdateProcessTreeInformation();
        }

        
    }
}
