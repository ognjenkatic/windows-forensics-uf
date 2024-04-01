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
        private BIOS _bios;

        public BIOS Bios
        {
            get
            {
                return _bios;
            }
            set
            {
                if(_bios != value)
                {
                    _bios = value;
                    RaisePropertyChanged("Bios");
                }
            }
        }



        public async void AsyncUpdateBiosInformation()
        {
            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading BIOS data...";
            Bios = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

                IBIOSService bs = sf.CreateBIOSService();

                return bs.GetBIOS();
            });

            IsModelInformationBeingUpdated = false;
        }

        public BIOSViewModel()
        {
            AsyncUpdateBiosInformation();
        }
    }
}
