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
    /// The view model for process information
    /// </summary>
    public class ProcessViewModel : BaseViewModel
    {
        private ObservableCollection<Process> _processes = new ObservableCollection<Process>();

        public ObservableCollection<Process> Processes { get => _processes; set => _processes = value; }

        public ProcessViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();
            IProcessService ps = sf.CreateProcessService();

            foreach(Process process in ps.GetProcesses())
            {
                Processes.Add(process);
            }
        }

        
    }
}
