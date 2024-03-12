using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;

namespace WinFo.Service.Configuration
{
    /// <summary>
    /// Interface that defines an ipv4 routing information service
    /// </summary>
    public interface IIP4RoutingTableService
    {
        List<IP4Route> GetRoutes();
    }
}
