using System.Globalization;
using System.Management;
using WinFo.Application.Model.Configuration;

namespace WinFo.Application.Service.Configuration.Win7
{
    public class Win7SecurityService : ISecurityService
    {
        public string[] GetAntivirusNames()
        {
            try
            {
                ManagementScope scope = new(@"\\.\root\SecurityCenter2");

                ObjectQuery query = new("SELECT * FROM AntivirusProduct");

                ManagementObjectSearcher searcher = new(scope, query);

                ManagementObjectCollection queryCollection = searcher.Get();

                return queryCollection
                    .Cast<ManagementObject>()
                    .Select(o => $"{o["displayName"]}")
                    .ToArray();
            }
            catch
            {
                return Array.Empty<string>();
            }
        }

        public FirewallInfo GetFirewallInfo()
        {
            Type NetFwMgrType = Type.GetTypeFromProgID("HNetCfg.FwMgr", false) ?? throw new InvalidOperationException("Could not load HNetCfg.FwMgr type");
            dynamic mgr = Activator.CreateInstance(NetFwMgrType) ?? throw new InvalidOperationException("Could not create instance of HNetCfg.FwMgr type");

            var authorizedApplications = new List<string>();
            var globallyOpenPorts = new List<int>();
            var servicePorts = new List<int>();

            foreach (var port in mgr.LocalPolicy.CurrentProfile.GloballyOpenPorts)
            {
                globallyOpenPorts.Add(port.Port);
            }
            foreach (var app in mgr.LocalPolicy.CurrentProfile.AuthorizedApplications)
            {
                authorizedApplications.Add(app.Name);
            }
            foreach (var app in mgr.LocalPolicy.CurrentProfile.Services)
            {
                foreach (var port in app.GloballyOpenPorts)
                {
                    servicePorts.Add(port.Port);
                }
            }

            return new FirewallInfo
            {
                IsEnabled = mgr.LocalPolicy.CurrentProfile.FirewallEnabled,
                AuthorizedApplications = [.. authorizedApplications],
                GloballyOpenPorts = [.. globallyOpenPorts],
                ServicePorts = [.. servicePorts],
            };
        }

        public WindowsDefenderInfo GetWindowsDefenderInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                @"root\Microsoft\Windows\Defender",
                "SELECT * FROM MSFT_MpComputerStatus"
            );

            var response = new WindowsDefenderInfo();

            foreach (ManagementObject obj in searcher.Get())
            {
                response.AreDefenderSignaturesOutOfDate = (bool)obj["DefenderSignaturesOutOfDate"];
                response.IsAntivirusEnabled = (bool)obj["AntivirusEnabled"];
                response.LastSignatureUpdate = ManagementDateTimeConverter.ToDateTime((string)obj["AntivirusSignatureLastUpdated"]);
                response.LastQuickScan = ManagementDateTimeConverter.ToDateTime((string)obj["QuickScanEndTime"]);
            }

            return response;
        }
    }
}
