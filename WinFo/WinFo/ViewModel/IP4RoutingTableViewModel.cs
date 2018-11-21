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
        private ObservableCollection<IP4Route> _ip4Routes = new ObservableCollection<IP4Route>();

        /// <summary>
        /// A collection of IP4 routes
        /// </summary>
        public ObservableCollection<IP4Route> IP4Routes
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

        public IP4RoutingTableViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IIP4RoutingTableService ip4rt = sf.CreateIP4RoutingTableService();

            List<IP4Route> routes = ip4rt.GetRoutes();

            foreach (IP4Route route in routes)
            {
                _ip4Routes.Add(route);
            }
        }
    }
}
