using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;
using WinFo.Service;
using WinFo.Service.Configuration;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model for the network adapter
    /// </summary
    public class NetworkAdapterViewModel : BaseViewModel
    {
        private bool _isNetworkAdapterInformationBeingUpdated;
        private ObservableCollection<NetworkAdapter> _adapters = new ObservableCollection<NetworkAdapter>();
        private ViewModelCommand UpdateNetworkAdapterInformationCommand;
        /// <summary>
        /// A collection of network adapters
        /// </summary>
        public ObservableCollection<NetworkAdapter> Adapters
        {
            get
            {
                return _adapters;
            }
            set
            {
                _adapters = value;
            }
        }

        public bool IsNetworkAdapterInformationBeingUpdated
        {
            get
            {
                return _isNetworkAdapterInformationBeingUpdated;
            }
            set
            {
                if(_isNetworkAdapterInformationBeingUpdated != value)
                {
                    _isNetworkAdapterInformationBeingUpdated = value;
                    RaisePropertyChanged("IsNetworkAdapterInformationBeingUpdated");
                }
            }
        }

        public async void UpdateNetworkAdapterInformation(object parameter = null)
        {
            IsNetworkAdapterInformationBeingUpdated = true;
            List<NetworkAdapter> nads = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

                INetworkAdapterService inas = sf.CreateNetworkAdapterService();

                return inas.GetNetworkAdapters();

                
            });

            foreach (NetworkAdapter nad in nads)
            {
                _adapters.Add(nad);
            }

            IsNetworkAdapterInformationBeingUpdated = false;
        }

        public bool CanUpdateNetworkAdapterInformation(object parameter = null)
        {
            return !IsNetworkAdapterInformationBeingUpdated;
        }

        public NetworkAdapterViewModel()
        {
            UpdateNetworkAdapterInformationCommand = new ViewModelCommand(UpdateNetworkAdapterInformation, CanUpdateNetworkAdapterInformation);

            UpdateNetworkAdapterInformation();
        }
    }
}
