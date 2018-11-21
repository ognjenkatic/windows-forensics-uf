using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Usage.Model;

namespace WinFo.Service.Usage
{
    /// <summary>
    /// Interface that defines a user session service
    /// </summary>
    public interface IUserSessionService
    {
        List<UserSession> GetUserSessions(string username = null);
    }
}
