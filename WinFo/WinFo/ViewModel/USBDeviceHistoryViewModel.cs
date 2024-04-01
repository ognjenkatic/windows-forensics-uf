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
    /// View model for the usb history information
    /// </summary>
    public class USBDeviceHistoryViewModel : BaseViewModel
    {
        private List<USBDeviceHistoryEntry> _history = new List<USBDeviceHistoryEntry>();

        public List<USBDeviceHistoryEntry> History { get => _history; set => _history = value; }

        public async void AsyncUpdateUSBDeviceHistoryInformation()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IUSBDeviceHistoryService dhs = sf.CreateUSBDeviceHistoryService();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading USB History data...";

            History = await Task.Run(() =>
            {
                return dhs.GetUSBDeviceHistory();
            });

            RaisePropertyChanged("History");

            IsModelInformationBeingUpdated = false;
        }

        public USBDeviceHistoryViewModel()
        {
            AsyncUpdateUSBDeviceHistoryInformation();
        }
    }
}
