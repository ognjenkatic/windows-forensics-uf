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
    /// The view model for recently used information
    /// </summary>
    public class RecentlyUsedEntryViewModel : BaseViewModel
    {
        #region fields
        private ObservableCollection<RecentlyUsedEntry> _recentRunBarEntries = new ObservableCollection<RecentlyUsedEntry>();
        private ObservableCollection<RecentlyUsedEntry> _recentWindowEntries = new ObservableCollection<RecentlyUsedEntry>();
        private ObservableCollection<RecentlyUsedEntry> _recentlyOpenedFilesEntries = new ObservableCollection<RecentlyUsedEntry>();


        #endregion

        #region properties
        public ObservableCollection<RecentlyUsedEntry> RecentRunBarEntries { get => _recentRunBarEntries; set => _recentRunBarEntries = value; }
        public ObservableCollection<RecentlyUsedEntry> RecentWindowEntries { get => _recentWindowEntries; set => _recentWindowEntries = value; }
        public ObservableCollection<RecentlyUsedEntry> RecentlyOpenedFilesEntries { get => _recentlyOpenedFilesEntries; set => _recentlyOpenedFilesEntries = value; }
        #endregion

        public RecentlyUsedEntryViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IRecentlyUsedService rus = sf.CreateRecentlyUsedService();

            foreach(RecentlyUsedEntry entry in rus.GetRecentlyOpenedFiles())
            {
                _recentlyOpenedFilesEntries.Add(entry);
            }

            foreach (RecentlyUsedEntry entry in rus.GetRecentlRunBarEntries())
            {
                _recentRunBarEntries.Add(entry);
            }

            foreach (RecentlyUsedEntry entry in rus.GetMainWindowCache())
            {
                _recentWindowEntries.Add(entry);
            }
        }
    }
}
