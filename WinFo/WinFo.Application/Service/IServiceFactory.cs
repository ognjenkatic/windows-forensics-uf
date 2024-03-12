using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service.Configuration;
using WinFo.Service.Usage;

namespace WinFo.Service
{
    /// <summary>
    /// Abstract factory interface. Allows implementation of new factories for compatibilty with different operating systems
    /// </summary>
    public interface IServiceFactory
    {
        IComputerSessionService CreateComputerSessionService();

        IUserSessionService CreateUserSessionService();

        IComputerSystemService CreateComputerSystemService();
        
        IShareService CreateShareService();

        INetworkAdapterService CreateNetworkAdapterService();

        IIP4RoutingTableService CreateIP4RoutingTableService();

        IGroupUserService CreateGroupUserService();

        IEnvironmentVariableService CreateEnvironmentVariableService();

        IStartupEntryService CreateStartupEntryService();

        IUpdateService CreateUpdateService();

        IProcessService CreateProcessService();

        IBIOSService CreateBIOSService();

        IWindowsServiceService CreateWindowsServiceService();

        IARPTableService CreateARPTableService();

        IWLANSessionService CreateWLANSessionService();

        IInstalledProgramService CreateInstalledProgramService();

        IUSBDeviceHistoryService CreateUSBDeviceHistoryService();

        IMainWindowCacheService CreateMainWindowCacheService();

        IRecentlyOpenedFileService CreateRecentlyOpenedFileService();

        IRecentRunBarService CreateRecentRunBarService();

        IRecentDocumentService CreateRecentDocumentService();

        IUserAssistService CreateUserAssistService();

        IRecentAppService CreateRecentAppService();

        IShimCacheService CreateShimCacheService();

        IPrefetchService CreatePrefetchService();

        ISRUMService CreateSRUMService();

        IBAMService CreateBAMService();

        IRecycleBinService CreateRecycleBinService();

    }
}
