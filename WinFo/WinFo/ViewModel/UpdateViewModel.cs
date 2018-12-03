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
    /// The view model for update information
    /// </summary>
    public class UpdateViewModel
    {
        #region fields
        private ObservableCollection<Update> _updates = new ObservableCollection<Update>();
        #endregion

        #region properties
        public ObservableCollection<Update> Updates { get => _updates; set => _updates = value; }
        #endregion

        public UpdateViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IUpdateService ius = sf.CreateUpdateService();

            foreach(Update ud in ius.GetUpdates())
            {
                _updates.Add(ud);
            }
        }
    }
}
