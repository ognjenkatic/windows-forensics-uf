using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;
using WinFo.Service.Utility;

namespace WinFo.Service.Usage
{
    public interface IRecentlyOpenedFileService : IVerboseService
    {
        List<OpenedFileEntry> GetRecentlyOpenedFiles();
    }
}
