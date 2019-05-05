using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service;
using WinFo.Service.Usage;
using WinFo.Service.Utility;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model for recently used information
    /// </summary>
    public class RecentlyUsedEntryViewModel : BaseViewModel
    {
        #region fields
        private List<RunBarEntry> _recentRunBarEntries = new List<RunBarEntry>();
        private List<MainWindowCacheEntry> _recentWindowEntries = new List<MainWindowCacheEntry>();
        private List<OpenedFileEntry> _recentlyOpenedFilesEntries = new List<OpenedFileEntry>();
        private List<RecentDocument> _recentDocumentEntries = new List<RecentDocument>();

        #endregion

        #region properties
        public List<RunBarEntry> RecentRunBarEntries { get => _recentRunBarEntries; set => _recentRunBarEntries = value; }
        public List<MainWindowCacheEntry> RecentWindowEntries { get => _recentWindowEntries; set => _recentWindowEntries = value; }
        public List<OpenedFileEntry> RecentlyOpenedFilesEntries { get => _recentlyOpenedFilesEntries; set => _recentlyOpenedFilesEntries = value; }
        public List<RecentDocument> RecentDocumentEntries { get => _recentDocumentEntries; set => _recentDocumentEntries = value; }
        #endregion
        
        public async void UpdateRecentlyUsedEntryViewModel()
        {
            IsModelInformationBeingUpdated = true;

            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IRecentRunBarService rus = sf.CreateRecentRunBarService();
            IMainWindowCacheService mwcs = sf.CreateMainWindowCacheService();
            IRecentlyOpenedFileService rofs = sf.CreateRecentlyOpenedFileService();
            IRecentDocumentService rds = sf.CreateRecentDocumentService();

            rds.UpdateProgress += UpdateModelInformation;
            rofs.UpdateProgress += UpdateModelInformation;
            
            ModelInformationUpdateProgress = "Searching for recently opened files...";
            
            RecentlyOpenedFilesEntries = await Task.Run(() =>
            {
                return rofs.GetRecentlyOpenedFiles();
            });
            
            RaisePropertyChanged("RecentlyOpenedFilesEntries");

            ModelInformationUpdateProgress = "Searching for recent run bar entries...";

            RecentRunBarEntries = await Task.Run(() =>
            {
                return rus.GetRecentlRunBarEntries();
            });

            RaisePropertyChanged("RecentRunBarEntries");

            ModelInformationUpdateProgress = "Searching for main window cache entries...";

            RecentWindowEntries = await Task.Run(() =>
            {
                return mwcs.GetMainWindowCache();
            });

            RaisePropertyChanged("RecentWindowEntries");
            

            ModelInformationUpdateProgress = "Searching for recent documents entries...";

            RecentDocumentEntries = await Task.Run(() =>
            {
                return rds.GetRecentDocuments();
            });

            RaisePropertyChanged("RecentDocumentEntries");

            IsModelInformationBeingUpdated = false;
        }

        public RecentlyUsedEntryViewModel()
        {
            UpdateRecentlyUsedEntryViewModel();
        }
    }
}
