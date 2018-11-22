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
    public class StartupEntryViewModel
    {
        #region fields
        private ObservableCollection<StartupEntry> _startupEntries = new ObservableCollection<StartupEntry>();

        #endregion

        #region properties
        /// <summary>
        /// A collection of startup entries
        /// </summary>
        public ObservableCollection<StartupEntry> StartupEntries
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

        public StartupEntryViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IStartupEntryService rs = sf.CreateStartupEntryService();

            List<StartupEntry> entries = rs.GetStartupEntries();

            foreach(StartupEntry entry in entries)
            {
                _startupEntries.Add(entry);
            }

        }
    }
}
