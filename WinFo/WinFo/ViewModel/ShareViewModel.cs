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
    /// The view model for the share
    /// </summary
    public class ShareViewModel : BaseViewModel
    {
        private ObservableCollection<Share> _shares = new ObservableCollection<Share>();

        /// <summary>
        /// A collection of shares
        /// </summary>
        public ObservableCollection<Share> Shares
        {
            get
            {
                return _shares;
            }
            set
            {
                _shares = value;
            }
        }

        public ShareViewModel()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IShareService ss = sf.CreateShareService();

            List<Share> shares = ss.GetShares();

            foreach (Share share in shares)
                _shares.Add(share);
        }
    }
}
