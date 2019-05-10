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
    /// The view model for installed program information
    /// </summary>
    public class InstalledProgramsViewModel : BaseViewModel
    {
        private List<InstalledProgram> _installedPrograms = new List<InstalledProgram>();

        public List<InstalledProgram> InstalledPrograms { get => _installedPrograms; set => _installedPrograms = value; }

        public async void AsyncUpdateInstalledProgramsInformation()
        {
            IsModelInformationBeingUpdated = true;

            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IInstalledProgramService ips = sf.CreateInstalledProgramService();

            ModelInformationUpdateProgress = "Loading installed programs information...";

            InstalledPrograms = await Task.Run(() =>
            {
                return ips.GetInstalledPrograms();
            });

            RaisePropertyChanged("InstalledPrograms");

            IsModelInformationBeingUpdated = false;
        }
        public InstalledProgramsViewModel()
        {
            AsyncUpdateInstalledProgramsInformation();
        }
    }
}
