﻿using System;
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
        private ObservableCollection<NetworkAdapter> _adapters = new ObservableCollection<NetworkAdapter>();

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

        public NetworkAdapterViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            INetworkAdapterService inas = sf.CreateNetworkAdapterService();

            List<NetworkAdapter> nads = inas.GetNetworkAdapters();

            foreach(NetworkAdapter nad in nads)
            {
                _adapters.Add(nad);
            }
        }
    }
}
