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
    public class RecycleBinViewModel : BaseViewModel
    {
        #region fields
        private List<RecycleBinEntry> _recycleBinEntries;
        #endregion

        #region properties
        public List<RecycleBinEntry> RecycleBinEntries { get => _recycleBinEntries; set => _recycleBinEntries = value; }
        #endregion

        public async void AsyncUpdateRecycleBinInformation()
        {
            IsModelInformationBeingUpdated = true;

            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IRecycleBinService rbs = sf.CreateRecycleBinService();

            ModelInformationUpdateProgress = "Loading recycle bin data...";

            RecycleBinEntries = await Task.Run(() =>
            {
                return rbs.GetRecycleBinEntries();
            });

            RaisePropertyChanged("RecycleBinEntries");

            IsModelInformationBeingUpdated = false;

        }

        public RecycleBinViewModel()
        {
            AsyncUpdateRecycleBinInformation();
        }
    }
}
