using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;

namespace WinFo.Service.Usage
{
    /// <summary>
    /// Interface that definse a prefetch service
    /// </summary>
    public interface IPrefetchService
    {
        List<PrefetchEntry> GetPrefetchEntries();
    }
}
