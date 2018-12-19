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
        private Process _selectedProcess;
        private SeriesCollection _processesMemoryUsageSeriesCollection = new SeriesCollection();
        private Dictionary<string, ulong> _topTenProcessByPhysicalMemory = new Dictionary<string, ulong>();

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

        public Dictionary<string, ulong> TopTenProcessByPhysicalMemory
        {
            get
            {
                return _topTenProcessByPhysicalMemory;
            }
            set
            {
                if(_topTenProcessByPhysicalMemory != value)
                {
                    _topTenProcessByPhysicalMemory = value;
                    RaisePropertyChanged("TopTenProcessByPhysicalMemory");
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

        public async void UpdateProcessTreeInformation()
        {
            IsModelInformationBeingUpdated = true;
            List<Process> processList = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();
                IProcessService ps = sf.CreateProcessService();

                return ps.GetProcesses();
            });

            processList = processList.OrderBy(o => o.PhysicalMemory).ToList();

            foreach (Process proc in processList)
                if (proc.IsOrphanProcess)
                    Processes.Add(proc);

            for (int i = 0; i < 10; i++)
                TopTenProcessByPhysicalMemory.Add(processList.ElementAt(i).ProcessName+"("+processList.ElementAt(i).Pid+")", processList.ElementAt(i).PhysicalMemory);

            ColumnSeries cs = new ColumnSeries();
            cs.Title = "Memory used (MB) ";
            cs.Values = new ChartValues<long>();

            foreach(KeyValuePair<string,ulong> kvp in TopTenProcessByPhysicalMemory)
            {
                cs.Values.Add((long)kvp.Value/1024/1024);
            }

            ProcessesMemoryUsageSeriesCollection.Add(cs);

            IsModelInformationBeingUpdated = false;
        }
        public ProcessTreeViewModel()
        {
            UpdateProcessTreeInformation();
        }

        
    }
}
