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
    /// The view model for the computer system
    /// </summary
    public class ComputerSystemViewModel : BaseViewModel
    {
        // TO-DO remove the collection and create a concrete representation of the computer system
        private ObservableCollection<ComputerSystem> _computerSystem = new ObservableCollection<ComputerSystem>();

        /// <summary>
        /// A collection that will contain only one computer system instance ( this is for ease of display and will be changed in later versions)
        /// </summary>
        public ObservableCollection<ComputerSystem> ComputerSystem
        {
            get
            {
                return _computerSystem;
            }
            set
            {
                _computerSystem = value;
            }
        }

        public ComputerSystemViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IComputerSystemService csc = sf.CreateComputerSystemService();

            _computerSystem.Add(csc.GetComputerSystem());
        }

    }
}
