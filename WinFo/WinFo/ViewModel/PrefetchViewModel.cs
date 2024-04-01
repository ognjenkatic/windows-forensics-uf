﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service;
using WinFo.Service.Usage;

namespace WinFo.ViewModel
{
    public class PrefetchViewModel : BaseViewModel
    {
        private List<PrefetchEntry> _entries;
        private PrefetchEntry _selectedEntry;
        public List<PrefetchEntry> Entries { get => _entries; set => _entries = value; }
        public PrefetchEntry SelectedEntry
        {
            get
            {
                return _selectedEntry;
            }
            set
            {
                _selectedEntry = value;
                RaisePropertyChanged("SelectedEntry");
            }
        }

        public async void AsyncUpdatePrefetchInformation()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading prefetch data...";

            Entries = await Task.Run(() =>
            {
                IPrefetchService pfs = sf.CreatePrefetchService();

                return pfs.GetPrefetchEntries();
            });

            RaisePropertyChanged("Entries");

            IsModelInformationBeingUpdated = false;
            
        }
        public PrefetchViewModel()
        {
            AsyncUpdatePrefetchInformation();
        }
    }
}
