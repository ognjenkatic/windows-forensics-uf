using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;

namespace WinFo.Service.Usage
{
    /// <summary>
    /// Interface that defines a recently used service
    /// </summary>
    public interface IRecentlyUsedService
    {
        List<RecentlyUsedEntry> GetRecentlyOpenedFiles();
        
        List<RecentlyUsedEntry> GetRecentlRunBarEntries();

        List<RecentlyUsedEntry> GetMainWindowCache();
    }
}
