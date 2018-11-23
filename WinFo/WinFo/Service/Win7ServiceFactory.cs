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

        public IRecentlyUsedService CreateRecentlyUsedService()
        {
            return new Win7RecentlyUsedService();
        }
    }
}
