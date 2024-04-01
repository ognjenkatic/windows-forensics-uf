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
    /// The view model for the ip4 routing rable
    /// </summary
    public class IP4RoutingTableViewModel : BaseViewModel
    {
        private List<IP4Route> _ip4Routes = new List<IP4Route>();

        /// <summary>
        /// A collection of IP4 routes
        /// </summary>
        public List<IP4Route> IP4Routes
        {
            get
            {
                return _ip4Routes;
            }
            set
            {
                _ip4Routes = value;
            }
        }

        public async void AsyncUpdateIp4RoutingTableInformation()
        {
            IsModelInformationBeingUpdated = true;

            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IIP4RoutingTableService ip4rt = sf.CreateIP4RoutingTableService();

            ModelInformationUpdateProgress = "Loading Ip4 routing data...";

            IP4Routes = await Task.Run(() =>
            {
                return ip4rt.GetRoutes();
            });

            RaisePropertyChanged("IP4Routes");

            IsModelInformationBeingUpdated = false;
        }
        public IP4RoutingTableViewModel()
        {
            AsyncUpdateIp4RoutingTableInformation();
        }
    }
}
