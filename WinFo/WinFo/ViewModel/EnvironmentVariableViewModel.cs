using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;
using WinFo.Service;
using WinFo.Service.Configuration;

namespace WinFo.ViewModel
{
    /// <summary>
    /// The view model for the environment variables
    /// </summary>
    public class EnvironmentVariableViewModel : BaseViewModel
    {
        #region fields
        private List<EnvironmentVariable> _environmentVariables = new List<EnvironmentVariable>();
        #endregion

        #region methods

        /// <summary>
        /// A colelction of environment variables
        /// </summary>
        public List<EnvironmentVariable> EnvironmentVariables
        {
            get
            {
                return _environmentVariables;
            }
            set
            {
                _environmentVariables = value;
            }
        }

        #endregion

        public async void AsyncUpdateEnvironmentVariableInformation()
        {
            IsModelInformationBeingUpdated = true;

            
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IEnvironmentVariableService evs = sf.CreateEnvironmentVariableService();

            ModelInformationUpdateProgress = "Loading environment variable information...";

            EnvironmentVariables = await Task.Run(() =>
            {
                return evs.GetEnvironmentVariables();
            });

            RaisePropertyChanged("EnvironmentVariables");

            IsModelInformationBeingUpdated = false;
            
        }
        public EnvironmentVariableViewModel()
        {
            AsyncUpdateEnvironmentVariableInformation();
        }
    }
}
