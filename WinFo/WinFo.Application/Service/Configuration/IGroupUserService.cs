using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;

namespace WinFo.Service.Configuration
{
    /// <summary>
    /// Interface that defines a group to user mapping service
    /// </summary>
    public interface IGroupUserService
    {
        Dictionary<UserGroup, List<User>> GetGroupUserDictionary();
    }
}
