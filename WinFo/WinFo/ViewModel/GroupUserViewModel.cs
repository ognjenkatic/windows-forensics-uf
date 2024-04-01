using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service;
using WinFo.Service.Configuration;

namespace WinFo.ViewModel
{
    public class GroupUserViewModel : BaseViewModel
    {
        private Dictionary<UserGroup, List<User>> _groupUserData = new Dictionary<UserGroup, List<User>>();

        private User _selectedUser;

        private UserGroup _selectedGroup;


        public Dictionary<UserGroup, List<User>> GroupUserData
        {
            get
            {
                return _groupUserData;
            }
            set
            {
                if (_groupUserData != value)
                {
                    _groupUserData = value;
                    RaisePropertyChanged("GroupUserData");
                }
            }
        }

        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                if(_selectedUser != value)
                {
                    _selectedUser = value;
                    RaisePropertyChanged("SelectedUser");
                }
            }
        }

        public object SelectedItem
        {
            set
            {
                if (value is User user)
                    SelectedUser = user;
                else if (value is KeyValuePair<UserGroup,List<User>> userGroupKvp)
                    SelectedGroup = userGroupKvp.Key;
            }
        }

        public UserGroup SelectedGroup
        {
            get
            {
                return _selectedGroup;
            }
            set
            {
                if(_selectedGroup != value)
                {
                    _selectedGroup = value;
                    RaisePropertyChanged("SelectedGroup");
                }
            }
        }

        private async void AsyncUpdateGroupUserInformation()
        {
            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading user and group information...";

            GroupUserData = await Task.Run(() =>
            {
                IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

                IGroupUserService iguf = sf.CreateGroupUserService();

                return iguf.GetGroupUserDictionary();
            });

            IsModelInformationBeingUpdated = false;
        }

        public GroupUserViewModel()
        {
            AsyncUpdateGroupUserInformation();
        }
    }
}
