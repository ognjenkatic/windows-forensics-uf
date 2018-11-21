using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFo.Service
{
    /// <summary>
    /// Produces actual factories
    /// </summary>
    public class ServiceFactoryProducer
    {
        // for the moment only returns win7 service factories
        // TO-DO  - make dynamic
        public static IServiceFactory GetServiceFactory()
        {
            return new Win7ServiceFactory();
        }
    }
}
