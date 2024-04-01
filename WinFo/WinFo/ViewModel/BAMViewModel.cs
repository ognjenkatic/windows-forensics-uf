using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service;
using WinFo.Service.Usage;

namespace WinFo.ViewModel
{
    public class BAMViewModel : BaseViewModel
    {
        private List<BAMEntry> _entries;

        public List<BAMEntry> Entries { get => _entries; set => _entries = value; }

        public async void AsyncUpdateBAMInformation()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading BAM data...";

            Entries = await Task.Run(() =>
            {
                IBAMService bs = sf.CreateBAMService();

                return bs.GetBAMEntries();
            });

            RaisePropertyChanged("Entries");

            IsModelInformationBeingUpdated = false;

        }
        public BAMViewModel()
        {
            AsyncUpdateBAMInformation();
        }
    }
}
