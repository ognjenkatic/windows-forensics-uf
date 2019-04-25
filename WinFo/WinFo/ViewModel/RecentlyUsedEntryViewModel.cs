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

        public async void UpdateRecentlyUsedEntryViewModel()
        {
            IsModelInformationBeingUpdated = true;

            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IRecentRunBarService rus = sf.CreateRecentRunBarService();
            IMainWindowCacheService mwcs = sf.CreateMainWindowCacheService();
            IRecentlyOpenedFileService rofs = sf.CreateRecentlyOpenedFileService();

            ModelInformationUpdateProgress = "Fetching opened file history...";

            List<OpenedFileEntry> openedFileList = await Task.Run(() =>
            {
                return rofs.GetRecentlyOpenedFiles();
            });

            foreach (OpenedFileEntry entry in openedFileList)
            {
                _recentlyOpenedFilesEntries.Add(entry);
            }

            ModelInformationUpdateProgress = "Fetching run bar history...";

            List<RunBarEntry> runBarList = await Task.Run(() =>
            {
                return rus.GetRecentlRunBarEntries();
            });

            foreach (RunBarEntry entry in runBarList)
            {
                _recentRunBarEntries.Add(entry);
            }

            ModelInformationUpdateProgress = "Fetching main window cache history...";

            List<MainWindowCacheEntry> cacheList = await Task.Run(() =>
            {
                return mwcs.GetMainWindowCache();
            });

            foreach (MainWindowCacheEntry entry in cacheList)
            {
                _recentWindowEntries.Add(entry);
            }

            IsModelInformationBeingUpdated = false;
        }

        public RecentlyUsedEntryViewModel()
        {
            UpdateRecentlyUsedEntryViewModel();
        }
    }
}
