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
        private List<WLANSession> sessions = new List<WLANSession>();

        public List<WLANSession> Sessions { get => sessions; set => sessions = value; }

        public async void AsyncUpdateWLANSessionInformation()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IWLANSessionService wss = sf.CreateWLANSessionService();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading WLAN session data...";

            Sessions = await Task.Run(() =>
            {
                return wss.GetWLANSessions();
            });

            RaisePropertyChanged("Sessions");

            IsModelInformationBeingUpdated = false;
        }

        public WLANSessionViewModel()
        {
            AsyncUpdateWLANSessionInformation();
        }
    }
}
