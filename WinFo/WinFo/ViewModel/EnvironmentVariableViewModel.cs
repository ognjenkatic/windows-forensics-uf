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
        private ObservableCollection<EnvironmentVariable> _environmentVariables = new ObservableCollection<EnvironmentVariable>();
        #endregion

        #region methods

        /// <summary>
        /// A colelction of environment variables
        /// </summary>
        public ObservableCollection<EnvironmentVariable> EnvironmentVariables
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

        public EnvironmentVariableViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IEnvironmentVariableService evs = sf.CreateEnvironmentVariableService();

            List<EnvironmentVariable> environmentVariables = evs.GetEnvironmentVariables();

            foreach(EnvironmentVariable ev in environmentVariables)
            {
                _environmentVariables.Add(ev);
            }
        }
    }
}
