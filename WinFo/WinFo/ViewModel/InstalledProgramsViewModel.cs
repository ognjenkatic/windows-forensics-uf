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
        private ObservableCollection<InstalledProgram> _installedPrograms = new ObservableCollection<InstalledProgram>();

        public ObservableCollection<InstalledProgram> InstalledPrograms { get => _installedPrograms; set => _installedPrograms = value; }

        public InstalledProgramsViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IInstalledProgramService ips = sf.CreateInstalledProgramService();

            foreach(InstalledProgram ip in ips.GetInstalledPrograms())
            {
                _installedPrograms.Add(ip);
            }
        }
    }
}
