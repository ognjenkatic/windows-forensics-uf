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
    /// The view model for update information
    /// </summary>
    public class UpdateViewModel : BaseViewModel
    {
        #region fields
        private List<Update> _updates = new List<Update>();
        #endregion

        #region properties
        public List<Update> Updates { get => _updates; set => _updates = value; }
        #endregion

        public async void AsyncUpdateUpdateInformation()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IUpdateService ius = sf.CreateUpdateService();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading update data...";

            Updates = await Task.Run(() =>
            {
                return ius.GetUpdates();
            });

            RaisePropertyChanged("Updates");

            IsModelInformationBeingUpdated = false;

        }

        public UpdateViewModel()
        {
            AsyncUpdateUpdateInformation();
        }
    }
}
