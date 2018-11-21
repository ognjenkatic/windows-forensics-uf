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
    /// The view model for the user session
    /// </summary
    public class UserSessionViewModel : BaseViewModel
    {
        private ObservableCollection<UserSession> _userSessions = new ObservableCollection<UserSession>();

        /// <summary>
        /// A collection of user sessions
        /// </summary>
        public ObservableCollection<UserSession> UserSession
        {
            get
            {
                return _userSessions;
            }
            set
            {
                _userSessions = value;
            }
        }

        public UserSessionViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IUserSessionService iuss = sf.CreateUserSessionService();

            foreach (UserSession session in iuss.GetUserSessions())
            {
                _userSessions.Add(session);
            }
        }
    }
}
