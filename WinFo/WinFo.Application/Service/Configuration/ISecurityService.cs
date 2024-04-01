using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Application.Model.Configuration;

namespace WinFo.Application.Service.Configuration
{
    public interface ISecurityService
    {
        string[] GetAntivirusNames();
        WindowsDefenderInfo GetWindowsDefenderInfo();
        FirewallInfo GetFirewallInfo();
    }
}
