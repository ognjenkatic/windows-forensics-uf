namespace WinFo.Application.Model.Configuration
{
    public class FirewallInfo
    {
        public bool IsEnabled { get; set; }
        public string[] AuthorizedApplications { get; set; } = [];
        public int[] ServicePorts { get; set; } = [];
        public int[] GloballyOpenPorts { get; set; } = [];

    }
}
