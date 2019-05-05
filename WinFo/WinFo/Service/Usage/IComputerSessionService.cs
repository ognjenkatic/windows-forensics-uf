using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Service.Utility;
using WinFo.Usage.Model;

namespace WinFo.Service.Usage
{
    /// <summary>
    /// Interface that defines a computer session
    /// </summary>
    public interface IComputerSessionService : IVerboseService
    {
        List<ComputerSession> GetComputerSessions();
    }
}
