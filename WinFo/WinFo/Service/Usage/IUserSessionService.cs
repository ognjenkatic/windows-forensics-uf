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
    /// Interface that defines a user session service
    /// </summary>
    public interface IUserSessionService : IVerboseService
    {
        List<UserSession> GetUserSessions(string username = null);
    }
}
