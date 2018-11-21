using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service;
using WinFo.Service.Usage;
using WinFo.Usage.Model;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model for the computer session
    /// </summary>
    public class ComputerSessionViewModel : BaseViewModel
    {
        private ObservableCollection<ComputerSession> _computerSessions = new ObservableCollection<ComputerSession>();

        /// <summary>
        /// A collection of computer sessions
        /// </summary>
        public ObservableCollection<ComputerSession> ComputerSessions
        {
            get
            {
                return _computerSessions;
            }
            set
            {
                _computerSessions = value;
            }
        }

        public ComputerSessionViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IComputerSessionService css = sf.CreateComputerSessionService();

            foreach(ComputerSession session in css.GetComputerSessions())
            {
                _computerSessions.Add(session);
            }
        }
    }
}
