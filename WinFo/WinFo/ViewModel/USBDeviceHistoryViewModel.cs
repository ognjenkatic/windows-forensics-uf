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
    public class USBDeviceHistoryViewModel
    {
        private ObservableCollection<USBDeviceHistoryEntry> _history = new ObservableCollection<USBDeviceHistoryEntry>();

        public ObservableCollection<USBDeviceHistoryEntry> History { get => _history; set => _history = value; }

        public USBDeviceHistoryViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IUSBDeviceHistoryService dhs = sf.CreateUSBDeviceHistoryService();

            foreach (USBDeviceHistoryEntry entry in dhs.GetUSBDeviceHistory())
                _history.Add(entry);
        }
    }
}
