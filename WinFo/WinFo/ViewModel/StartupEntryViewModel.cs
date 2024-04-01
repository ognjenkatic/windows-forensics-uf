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
    /// The view model for the startup entries
    /// </summary>
    public class StartupEntryViewModel : BaseViewModel
    {
        #region fields
        private List<StartupEntry> _startupEntries = new List<StartupEntry>();

        #endregion

        #region properties
        /// <summary>
        /// A collection of startup entries
        /// </summary>
        public List<StartupEntry> StartupEntries
        {
            get
            {
                return _startupEntries;
            }
            set
            {
                _startupEntries = value;
            }
        }
        #endregion

        public async void AsyncUpdateStartupEntryInformation()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IStartupEntryService rs = sf.CreateStartupEntryService();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading startup entry data...";

            StartupEntries = await Task.Run(() =>
            {
                return rs.GetStartupEntries();
            });

            RaisePropertyChanged("StartupEntries");

            IsModelInformationBeingUpdated = false;
        }

        public StartupEntryViewModel()
        {
            AsyncUpdateStartupEntryInformation();
        }
    }
}
