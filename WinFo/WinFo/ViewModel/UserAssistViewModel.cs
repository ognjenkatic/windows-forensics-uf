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
    /// <summary>
    /// User Assist View Model
    /// </summary>
    public class UserAssistViewModel : BaseViewModel
    {
        private List<UserAssistEntry> _userAssistEntries = new List<UserAssistEntry>();

        public List<UserAssistEntry> UserAssistEntries { get => _userAssistEntries; set => _userAssistEntries = value; }

        public async void AsyncUpdateUserAssistInformation()
        {
            IsModelInformationBeingUpdated = true;

            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IUserAssistService iuas = sf.CreateUserAssistService();

            ModelInformationUpdateProgress = "Loading user assist information...";

            UserAssistEntries = await Task.Run(() =>
            {
                return iuas.GetUserAssistEntries();
            });

            RaisePropertyChanged("UserAssistEntries");

            IsModelInformationBeingUpdated = false;
        }

        public UserAssistViewModel()
        {
            AsyncUpdateUserAssistInformation();
        }
    }
}
