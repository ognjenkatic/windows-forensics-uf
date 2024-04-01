namespace WinFo.Application.Model.Configuration
{
    public class WindowsDefenderInfo
    {
        public bool IsAntivirusEnabled { get; set; }
        public DateTimeOffset LastSignatureUpdate { get; set; }
        public DateTimeOffset LastQuickScan { get; set; }
        public bool AreDefenderSignaturesOutOfDate { get; set; }
    }
}
