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
    /// The view model for the ARP table information
    /// </summary>
    public class ARPTableViewModel : BaseViewModel
    {
        private List<ARPEntry> _arpEntries = new List<ARPEntry>();

        public List<ARPEntry> ArpEntries { get => _arpEntries; set => _arpEntries = value; }


        public async void AsyncUpdateArpTableInformation()
        {
            IsModelInformationBeingUpdated = true;
            
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IARPTableService ats = sf.CreateARPTableService();

            ModelInformationUpdateProgress = "Loading ARP data...";

            ArpEntries = await Task.Run(() =>
            {
                return ats.GetARPEntries();
            });

            RaisePropertyChanged("ArpEntries");

            IsModelInformationBeingUpdated = false;

        }
        public ARPTableViewModel()
        {
            AsyncUpdateArpTableInformation();
        }
    }
}
