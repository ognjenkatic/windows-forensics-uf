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
    public class RecentAppViewModel : BaseViewModel
    {
        private List<RecentAppEntry> _recentAppEntries;

        private RecentAppEntry _selectedRecentAppEntry = new RecentAppEntry();

        private RecentAppItemEntry _selectedRecentAppItemEntry = new RecentAppItemEntry();

        public List<RecentAppEntry> RecentAppEntries { get => _recentAppEntries; set => _recentAppEntries = value; }

        public RecentAppEntry SelectedRecentAppEntry
        {
            get
            {
                return _selectedRecentAppEntry;
            }
            set
            {
                _selectedRecentAppEntry = value;
                RaisePropertyChanged("SelectedRecentAppEntry");
            }
        }

        public RecentAppItemEntry SelectedRecentAppItemEntry
        {
            get
            {
                return _selectedRecentAppItemEntry;
            }
            set
            {
                _selectedRecentAppItemEntry = value;
                RaisePropertyChanged("SelectedRecentAppItemEntry");
            }
        }

        public async void AsyncUpdateRecentAppInformation()
        {
            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading recent app data...";

            RecentAppEntries = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

                IRecentAppService iras = sf.CreateRecentAppService();

                return iras.GetRecentAppEntries();
            });

            RaisePropertyChanged("RecentAppEntries");

            ModelInformationUpdateProgress = "Completed loading recent app data...";

            IsModelInformationBeingUpdated = false;
        }

        public RecentAppViewModel()
        {
            AsyncUpdateRecentAppInformation();
        }
    }
}
