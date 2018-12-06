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
        private bool _isSystemInformationBeingUpdated;
        private string _modelActivity;
        private ComputerSystem _system;
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

        public bool IsSystemInformationBeingUpdated
        {
            get
            {
                return _isSystemInformationBeingUpdated;
            }
            set
            {
                if (_isSystemInformationBeingUpdated != value)
                {
                    _isSystemInformationBeingUpdated = value;
                    RaisePropertyChanged("IsSystemInformationBeingUpdated");
                }
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
            return !IsSystemInformationBeingUpdated;
        }

        public async void UpdateSystemInformation(object parameter = null)
        {

            IsSystemInformationBeingUpdated = true;
            _modelActivity = "(Fetching System Info)";
            RaisePropertyChanged("ModelTarget");
            UpdateSystemInformationCommand.RaiseCanExecuteChanged();
            System = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

                IComputerSystemService css = sf.CreateComputerSystemService();

                return css.GetComputerSystem();
            });


            IsSystemInformationBeingUpdated = false;
            _modelActivity = "(Idle)";
            RaisePropertyChanged("ModelTarget");
            UpdateSystemInformationCommand.RaiseCanExecuteChanged();
        }

        public MainViewModel()
        {
            LoadWindowCommand = new ViewModelCommand(LoadWindow, CanLoadWindow);

            UpdateSystemInformationCommand = new ViewModelCommand(UpdateSystemInformation, CanUpdateSystemInformation);

            UpdateSystemInformation();
        }

    }

    
}
