using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;

namespace WinFo.Service.Usage
{
    /// <summary>
    /// Interface that defines a windows service service
    /// </summary>
    public interface IWindowsServiceService
    {
        List<WindowsService> GetServices();
    }
}
