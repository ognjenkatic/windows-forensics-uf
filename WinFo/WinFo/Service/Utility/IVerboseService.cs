using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility
{
    public interface IVerboseService
    {
        event UpdateProgressDelegate UpdateProgress;
    }
}
