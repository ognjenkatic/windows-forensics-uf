﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Usage;

namespace WinFo.Service.Usage
{
    public interface ISRUMService
    {
        List<SRUMNetworkConnectivityEntry> GetSRUMNetworkConnectivityEntries(string filePath);

        List<SRUMApplicationResourceUsageDataEntry> GetSRUMApplicationResourceUsageDataEntries(string filePath);
    }
}
