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
    /// The view model for service information
    /// </summary>
    public class WindowsServiceViewModel : BaseViewModel
    {
        private List<WindowsService> _services = new List<WindowsService>();

        public List<WindowsService> Services { get => _services; set => _services = value; }

        public async void AsyncUpdateWindowsServiceInformation()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IWindowsServiceService wss = sf.CreateWindowsServiceService();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading service data...";

            Services = await Task.Run(() =>
            {
                return wss.GetServices();
            });

            RaisePropertyChanged("Services");

            IsModelInformationBeingUpdated = false;
        }

        public WindowsServiceViewModel()
        {
            AsyncUpdateWindowsServiceInformation();
        }
    }
}
