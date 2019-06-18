using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service.Configuration;
using WinFo.Service.Configuration.Win7;
using WinFo.Service.Usage;
using WinFo.Service.Usage.Win7;

namespace WinFo.Service
{
    /// <summary>
    /// An actual factory that creates services compatible with the win7 operating system
    /// </summary>
    public class Win7ServiceFactory : IServiceFactory
    {
        public IComputerSessionService CreateComputerSessionService()
        {
            return new Win7ComputerSessionService();
        }

        public IGroupUserService CreateGroupUserService()
        {
            return new Win7GroupUserService();
        }

        public IIP4RoutingTableService CreateIP4RoutingTableService()
        {
            return new Win7IP4RoutingTableService();
        }

        public INetworkAdapterService CreateNetworkAdapterService()
        {
            return new Win7NetworkAdapterService();
        }

        public IShareService CreateShareService()
        {
            return new Win7ShareService();
        }
        
        public IComputerSystemService CreateComputerSystemService()
        {
            return new Win7ComputerSystemService();
        }

        public IUserSessionService CreateUserSessionService()
        {
            return new Win7UserSessionService();
        }

        public IEnvironmentVariableService CreateEnvironmentVariableService()
        {
            return new Win7EnvironmentVariableService();
        }

        public IStartupEntryService CreateStartupEntryService()
        {
            return new Win7StartupEntryService();
        }

        public IUpdateService CreateUpdateService()
        {
            return new Win7UpdateService();
        }

        public IProcessService CreateProcessService()
        {
            return new Win7ProcessService();
        }

        public IBIOSService CreateBIOSService()
        {
            return new Win7BIOSService();
        }
        
        public IWindowsServiceService CreateWindowsServiceService()
        {
            return new Win7WindowsServiceService();
        }

        public IARPTableService CreateARPTableService()
        {
            return new Win7ARPTableService();
        }

        public IWLANSessionService CreateWLANSessionService()
        {
            return new Win7WLANSessionService();
        }

        public IInstalledProgramService CreateInstalledProgramService()
        {
            return new Win7InstalledProgramService();
        }

        public IUSBDeviceHistoryService CreateUSBDeviceHistoryService()
        {
            return new Win7USBDeviceHistoryService();
        }

        public IMainWindowCacheService CreateMainWindowCacheService()
        {
            return new Win7MainWindowCacheService();
        }

        public IRecentlyOpenedFileService CreateRecentlyOpenedFileService()
        {
            return new Win7RecentlyOpenedFileService();
        }

        public IRecentRunBarService CreateRecentRunBarService()
        {
            return new Win7RunBarService();
        }

        public IRecentDocumentService CreateRecentDocumentService()
        {
            return new Win7RecentDocumentService();
        }

        public IUserAssistService CreateUserAssistService()
        {
            return new Win7UserAssistService();
        }

        // TO-DO this functionality is not present in this form for windows, come up with a better way of relaying this information to the user
        public IRecentAppService CreateRecentAppService()
        {
            throw new NotImplementedException();
        }

        public IShimCacheService CreateShimCacheService()
        {
            throw new NotImplementedException();
        }

        public IPrefetchService CreatePrefetchService()
        {
            throw new NotImplementedException();
        }

        public ISRUMService CreateSRUMService()
        {
            throw new NotImplementedException();
        }
    }
}
