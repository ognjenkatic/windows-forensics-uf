using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service.Utility.Serialization
{
    public interface IMySerializer
    {
        string Serialize(object data);
    }
}
