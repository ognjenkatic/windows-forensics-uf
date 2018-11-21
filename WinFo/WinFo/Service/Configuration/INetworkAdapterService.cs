using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;

namespace WinFo.Service.Configuration
{
    /// <summary>
    /// Interface that defines a network adapter information service
    /// </summary>
    public interface INetworkAdapterService
    {
        List<NetworkAdapter> GetNetworkAdapters();
    }
}
