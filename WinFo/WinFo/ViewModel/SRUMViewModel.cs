using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service;
using WinFo.Service.Usage;
using Microsoft.Win32;

namespace WinFo.ViewModel
{
    public class SRUMViewModel : BaseViewModel
    {
        private List<SRUMApplicationResourceUsageDataEntry> entries;
        private string _filePath;
        public List<SRUMApplicationResourceUsageDataEntry> Entries { get => entries; set => entries = value; }

        public ViewModelCommand SelectFileCommand { get; set; }
        private async void AsyncUpdateSRUMInformation()
        {
          
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading srum information...";

            Entries = await Task.Run(() =>
            {
                ISRUMService iss = sf.CreateSRUMService();

                return iss.GetSRUMApplicationResourceUsageDataEntries(_filePath);
            });

            ModelInformationUpdateProgress = "Loading complete...";

            IsModelInformationBeingUpdated = false;

            RaisePropertyChanged("Entries");

        }

        public bool CanSelectFile(object parameter = null)
        {
            return !IsModelInformationBeingUpdated;
        }

        public void SelectFile(object parameter = null)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".dat";
            dlg.FileName = "SRUDB";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                _filePath = dlg.FileName;
                AsyncUpdateSRUMInformation();
            }
        }

        public SRUMViewModel()
        {
            SelectFileCommand = new ViewModelCommand(SelectFile, CanSelectFile);
            
            
        }
    }
}
