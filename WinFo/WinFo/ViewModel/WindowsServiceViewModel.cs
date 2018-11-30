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
    public class WindowsServiceViewModel
    {
        ObservableCollection<WindowsService> _services = new ObservableCollection<WindowsService>();

        public ObservableCollection<WindowsService> Services { get => _services; set => _services = value; }

        public WindowsServiceViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IWindowsServiceService wss = sf.CreateWindowsServiceService();

            foreach(WindowsService ws in wss.GetServices())
            {
                _services.Add(ws);
            }
        }
    }
}
