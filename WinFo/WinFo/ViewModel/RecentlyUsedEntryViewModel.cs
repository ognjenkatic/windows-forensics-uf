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
        private ObservableCollection<RunBarEntry> _recentRunBarEntries = new ObservableCollection<RunBarEntry>();
        private ObservableCollection<MainWindowCacheEntry> _recentWindowEntries = new ObservableCollection<MainWindowCacheEntry>();
        private ObservableCollection<OpenedFileEntry> _recentlyOpenedFilesEntries = new ObservableCollection<OpenedFileEntry>();


        #endregion

        #region properties
        public ObservableCollection<RunBarEntry> RecentRunBarEntries { get => _recentRunBarEntries; set => _recentRunBarEntries = value; }
        public ObservableCollection<MainWindowCacheEntry> RecentWindowEntries { get => _recentWindowEntries; set => _recentWindowEntries = value; }
        public ObservableCollection<OpenedFileEntry> RecentlyOpenedFilesEntries { get => _recentlyOpenedFilesEntries; set => _recentlyOpenedFilesEntries = value; }
        #endregion

        public RecentlyUsedEntryViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IRecentRunBarService rus = sf.CreateRecentRunBarService();

            IMainWindowCacheService mwcs = sf.CreateMainWindowCacheService();
            IRecentlyOpenedFileService rofs = sf.CreateRecentlyOpenedFileService();

            foreach(OpenedFileEntry entry in rofs.GetRecentlyOpenedFiles())
            {
                _recentlyOpenedFilesEntries.Add(entry);
            }

            foreach (RunBarEntry entry in rus.GetRecentlRunBarEntries())
            {
                _recentRunBarEntries.Add(entry);
            }

            foreach (MainWindowCacheEntry entry in mwcs.GetMainWindowCache())
            {
                _recentWindowEntries.Add(entry);
            }
        }
    }
}
