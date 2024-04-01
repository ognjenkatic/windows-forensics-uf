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
    /// Shim cache View Model
    /// </summary>
    public class ShimCacheViewModel : BaseViewModel
    {
        #region fields
        private List<ShimCacheEntry> _shimCacheEntries;
        #endregion

        #region properties
        public List<ShimCacheEntry> ShimCacheEntries { get => _shimCacheEntries; set => _shimCacheEntries = value; }
        #endregion

        public async void AsyncShimCacheUpdateInformation()
        {
            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            IsModelInformationBeingUpdated = true;

            ModelInformationUpdateProgress = "Loading shim cache information...";

            ShimCacheEntries = await Task.Run(() =>
            {
                IShimCacheService iscs = sf.CreateShimCacheService();

                return iscs.GetShimCacheEntries();
            });

            RaisePropertyChanged("ShimCacheEntries");

            ModelInformationUpdateProgress = "Finished loading shim cache information...";

            IsModelInformationBeingUpdated = false;
        }
        public ShimCacheViewModel()
        {
            AsyncShimCacheUpdateInformation();
        }
    }
}
