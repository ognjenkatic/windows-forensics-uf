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
    /// The view model for BIOS information
    /// </summary>
    public class BIOSViewModel : BaseViewModel
    {
        private ObservableCollection<BIOS> _bios = new ObservableCollection<BIOS>();

        public ObservableCollection<BIOS> Bios { get => _bios; set => _bios = value; }

        public BIOSViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IBIOSService bs = sf.CreateBIOSService();

            _bios.Add(bs.GetBIOS());
        }
    }
}
