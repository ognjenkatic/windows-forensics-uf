using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WinFo.Model.Configuration;
using WinFo.Service;
using WinFo.Service.Configuration;
using WinFo.View;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model for the application hub
    /// </summary
    public class MainViewModel : BaseViewModel
    {
        #region fields
        private string _modelActivity;
        private ComputerSystem _system;
        private object _selectedItem;
        private DiskDriveViewModel _selectedDiskDrive;
        private DiskPartitionViewModel _selectedDiskPartition;
        private LogicalDiskViewModel _selectedLogicalDisk;
        private CPUInfoViewModel _cpuInfo;
        private ulong _freePhysicalMemoryInt;
        private string _memoryAllocation;
        private ulong _totalPhysicalMemoryInt;
        private ulong _usedPhysicalMemoryInt;
        private int _physicalMemoryCount;
        #endregion

        #region properties
        /// <summary>
        /// The command which loads windows based on names passed as parameters
        /// </summary>
        public ViewModelCommand LoadWindowCommand { get; set; }

        /// <summary>
        /// The command which updates the system information
        /// </summary>
        public ViewModelCommand UpdateSystemInformationCommand { get; set; }

        public ulong UsedPhysicalMemoryInt
        {
            get
            {
                return _usedPhysicalMemoryInt;
            }
            set
            {
                if(_usedPhysicalMemoryInt != value)
                {
                    _usedPhysicalMemoryInt = value;
                    RaisePropertyChanged("UsedPhysicalMemoryInt");
                }
            }
        }


        public ulong FreePhysicalMemoryInt
        {
            get
            {
                return _freePhysicalMemoryInt;
            }
            set
            {
                if(_freePhysicalMemoryInt != value)
                {
                    _freePhysicalMemoryInt = value;
                    RaisePropertyChanged("FreePhysicalMemoryInt");
                }
            }
        }

        public string MemoryAllocation
        {
            get
            {
                return _memoryAllocation;
            }
            set
            {
                if (_memoryAllocation != value)
                {
                    _memoryAllocation = value;
                    RaisePropertyChanged("MemoryAllocation");
                }
            }

        }
        public ulong TotalPhysicalMemoryInt
        {
            get
            {
                return _totalPhysicalMemoryInt;
            }
            set
            {
                if(_totalPhysicalMemoryInt != value)
                {
                    _totalPhysicalMemoryInt = value;
                    RaisePropertyChanged("TotalPhysicalMemoryInt");
                }
            }
        }

        public int PhysicalMemoryCount
        {
            get
            {
                return _physicalMemoryCount;
            }
            set
            {
                if(_physicalMemoryCount != value)
                {
                    _physicalMemoryCount = value;
                    RaisePropertyChanged("PhysicalMemoryCount");
                }
            }
        }

        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
                UpdateSelection();
            }
        }

        public CPUInfoViewModel CpuInfo
        {
            get
            {
                return _cpuInfo;
            }
            set
            {
                _cpuInfo = value;
                RaisePropertyChanged("CpuInfo");
            }
        }
        public LogicalDiskViewModel SelectedLogicalDisk
        {
            get
            {
                return _selectedLogicalDisk;
            }
            set
            {
                _selectedLogicalDisk = value;
                RaisePropertyChanged("SelectedLogicalDisk");
            }
        }

        public DiskPartitionViewModel SelectedDiskPartition
        {
            get
            {
                return _selectedDiskPartition;
            }
            set
            {
                _selectedDiskPartition = value;
                RaisePropertyChanged("SelectedDiskPartition");
            }
        }

        public DiskDriveViewModel SelectedDiskDrive
        {
            get
            {
                return _selectedDiskDrive;
            }
            set
            {
                _selectedDiskDrive = value;
                RaisePropertyChanged("SelectedDiskDrive");
            }
        }
        public ComputerSystem System
        {
            get
            {
                return _system;
            }
            set
            {
                _system = value;
                RaisePropertyChanged("System");
            }
        }
        
        public string ModelTarget
        {
            get
            {
                return "Main Window " + _modelActivity;
            }
        }
        #endregion


        /// <summary>
        /// Load a window
        /// </summary>
        /// <param name="parameter">The window name</param>
        public void LoadWindow(object parameter = null)
        {
            if (parameter is string)
            {
                string windowName = (string)parameter;

                switch (windowName)
                {
                    case ("Shares"):
                        {
                            ShareView sv = new ShareView();
                            sv.Show();
                            break;
                        }
                    case ("Network Adapters"):
                        {
                            NetworkAdapterView nav = new NetworkAdapterView();
                            nav.Show();
                            break;
                        }
                    case ("IP4 Routes"):
                        {
                            IP4RoutingTableView iprv = new IP4RoutingTableView();
                            iprv.Show();
                            break;
                        }
                    case ("User Sessions"):
                        {
                            UserSessionView usv = new UserSessionView();
                            usv.Show();
                            break;
                        }
                    case ("Computer Sessions"):
                        {
                            ComputerSessionView csv = new ComputerSessionView();
                            csv.Show();
                            break;
                        }
                    case ("Computer System"):
                        {
                            ComputerSystemView cosv = new ComputerSystemView();
                            cosv.Show();
                            break;
                        }
                    case ("Environment Variables"):
                        {
                            EnvironmentVariableView evv = new EnvironmentVariableView();
                            evv.Show();
                            break;
                        }
                    case ("Startup Entries"):
                        {
                            StartupEntryView sev = new StartupEntryView();
                            sev.Show();
                            break;
                        }
                    case ("Recently Used"):
                        {
                            RecentlyUsedEntryView reuv = new RecentlyUsedEntryView();
                            reuv.Show();
                            break;
                        }
                    case ("Updates"):
                        {
                            UpdateView uv = new UpdateView();
                            uv.Show();
                            break;
                        }
                    case ("Processes"):
                        {
                            ProcessView pv = new ProcessView();
                            pv.Show();
                            break;
                        }
                    case ("BIOS"):
                        {
                            BIOSView vb = new BIOSView();
                            vb.Show();
                            break;
                        }
                    case ("Services"):
                        {
                            WindowsServiceView wsv = new WindowsServiceView();
                            wsv.Show();
                            break;
                        }
                    case ("ARP Table"):
                        {
                            ARPTableView atv = new ARPTableView();
                            atv.Show();
                            break;
                        }
                    case ("WLAN Sessions"):
                        {
                            WLANSessionView wsv = new WLANSessionView();
                            wsv.Show();
                            break;
                        }
                    case ("Installed Programs"):
                        {
                            InstalledProgramView ipv = new InstalledProgramView();
                            ipv.Show();
                            break;
                        }
                    case ("USB Device History"):
                        {
                            USBDeviceHistoryView udhv = new USBDeviceHistoryView();
                            udhv.ShowDialog();
                            break;
                        }
                    case ("Users And Groups"):
                        {
                            GroupUserView guv = new GroupUserView();
                            guv.ShowDialog();
                            break;
                        }
                    case ("User Assist"):
                        {
                            UserAssistView uaw = new UserAssistView();
                            uaw.ShowDialog();
                            break;
                        }
                    case ("Recent Apps"):
                        {
                            RecentAppView rav = new RecentAppView();
                            rav.ShowDialog();
                            break;
                        }
                    case ("Shim Cache"):
                        {
                            ShimCacheView scv = new ShimCacheView();
                            scv.ShowDialog();
                            break;
                        }
                    case ("Prefetch"):
                        {
                            PrefetchView pfv = new PrefetchView();
                            pfv.ShowDialog();
                            break;
                        }
                    case ("SRUM App Usage"):
                        {
                            SRUMView sv = new SRUMView();
                            sv.ShowDialog();
                            break;
                        }
                    case ("SRUM Network Connectivity"):
                        {
                            SRUMNetworkView snv = new SRUMNetworkView();
                            snv.ShowDialog();
                            break;
                        }
                    case ("BAM Data"):
                        {
                            BAMView bv = new BAMView();
                            bv.ShowDialog();
                            break;
                        }
                    case ("Recycle Bin Data"):
                        {
                            RecycleBinView rbv = new RecycleBinView();
                            rbv.ShowDialog();
                            break;
                        }
                    case ("Export Data"):
                        {
                            DataExportView dev = new DataExportView();
                            dev.ShowDialog();
                            break;
                        }
                }
            }
            
        }
        
        /// <summary>
        /// Check if loading the window is possible
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <returns></returns>
        public bool CanLoadWindow(object parameter = null)
        {
            return true;
        }

        public bool CanUpdateSystemInformation(object parameter = null)
        {
            return !IsModelInformationBeingUpdated;
        }

        private void UpdateSelection()
        {
            if (_selectedItem is DiskDrive diskDrive)
            {
                SelectedDiskDrive = new DiskDriveViewModel(diskDrive);
            }else if (_selectedItem is DiskPartition diskPartition)
            {
                SelectedDiskPartition = new DiskPartitionViewModel(diskPartition);
            } else if (_selectedItem is LogicalDisk logicalDisk)
            {
                SelectedLogicalDisk = new LogicalDiskViewModel(logicalDisk);
            }

            
        }

        public async void AsyncUpdateSystemInformation(object parameter = null)
        {

            IsModelInformationBeingUpdated = true;
            _modelActivity = "(Fetching System Info)";
            RaisePropertyChanged("ModelTarget");
            UpdateSystemInformationCommand.RaiseCanExecuteChanged();
            System = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

                IComputerSystemService css = sf.CreateComputerSystemService();

                return css.GetComputerSystem();
            });

            CpuInfo = new CPUInfoViewModel(_system.CpuInfo);

            IsModelInformationBeingUpdated = false;
            _modelActivity = "(Idle)";
            PhysicalMemoryCount = System.PhysicalMemoryCollection.Count;
            

            ulong tpm = 0;
            foreach(PhysicalMemory pm in System.PhysicalMemoryCollection)
            {
                tpm += pm.Capacity;
            }

            TotalPhysicalMemoryInt = tpm;
            FreePhysicalMemoryInt = System.FreePhysicalMemory;
            UsedPhysicalMemoryInt = TotalPhysicalMemoryInt - FreePhysicalMemoryInt;
            MemoryAllocation = _usedPhysicalMemoryInt/1024/1024 +" / "+ _totalPhysicalMemoryInt / 1024 / 1024 + " MegaBytes";
            RaisePropertyChanged("ModelTarget");
            UpdateSystemInformationCommand.RaiseCanExecuteChanged();
        }

        public MainViewModel()
        {
            LoadWindowCommand = new ViewModelCommand(LoadWindow, CanLoadWindow);

            UpdateSystemInformationCommand = new ViewModelCommand(AsyncUpdateSystemInformation, CanUpdateSystemInformation);

            AsyncUpdateSystemInformation();
        }

    }

    
}
