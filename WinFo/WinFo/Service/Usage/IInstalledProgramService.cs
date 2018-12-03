using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;

namespace WinFo.Service.Usage
{
    /// <summary>
    /// Interface that defines an installed programs service
    /// </summary>
    public interface IInstalledProgramService
    {
        List<InstalledProgram> GetInstalledPrograms();
    }
}
