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
        private ObservableCollection<ARPEntry> _arpEntries = new ObservableCollection<ARPEntry>();

        public ObservableCollection<ARPEntry> ArpEntries { get => _arpEntries; set => _arpEntries = value; }

        public ARPTableViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IARPTableService ats = sf.CreateARPTableService();

            foreach(ARPEntry ae in ats.GetARPEntries())
            {
                _arpEntries.Add(ae);
            }
        }
    }
}
