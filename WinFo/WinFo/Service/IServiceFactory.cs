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

        IRecentlyUsedService CreateRecentlyUsedService();

    }
}
