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
    /// The view model for WLAN session information
    /// </summary>
    public class WLANSessionViewModel : BaseViewModel
    {
        private ObservableCollection<WLANSession> sessions = new ObservableCollection<WLANSession>();

        public ObservableCollection<WLANSession> Sessions { get => sessions; set => sessions = value; }

        public WLANSessionViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IWLANSessionService wss = sf.CreateWLANSessionService();

            foreach(WLANSession session in wss.GetWLANSessions())
            {
                sessions.Add(session);
            }
        }
    }
}
