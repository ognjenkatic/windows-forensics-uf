using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;
using WinFo.Model.Usage;
using WinFo.Service;
using WinFo.Service.Configuration;
using WinFo.Service.Usage;
using WinFo.Service.Utility.Serialization;
using WinFo.Usage.Model;

namespace WinFo.ViewModel
{
    public class DataExportViewModel : BaseViewModel
    {
        private static string _FOLDER_BASE_NAME = "winfo_export";
        public bool IsARPTableChecked { get; set; }

        public bool IsSharesChecked { get; set; }

        public bool IsNetworkAdaptersChecked { get; set; }

        public bool IsUserSessionsChecked { get; set; }

        public bool IsComputerSessionsChecked { get; set; }

        public bool IsIP4RoutesChecked { get; set; }

        public bool IsComputerSystemChecked { get; set; }

        public bool IsEnvironmentVariablesChecked { get; set; }

        public bool IsStartupEntriesChecked { get; set; }

        public bool IsRecentlyUsedChecked { get; set; }

        public bool IsUpdatesChecked { get; set; }

        public bool IsProcessesChecked { get; set; }

        public bool IsBIOSChecked { get; set; }

        public bool IsServicesChecked { get; set; }

        public bool IsWLANSessionsChecked { get; set; }

        public bool IsInstalledProgramsChecked { get; set; }

        public bool IsUSBDeviceHistoryChecked { get; set; }

        public bool IsUsersAndGroupsChecked { get; set; }

        public bool IsUserAssistChecked { get; set; }

        public bool IsRecentAppsChecked { get; set; }

        public bool IsShimCacheChecked { get; set; }

        public bool IsPrefetchChecked { get; set; }

        public bool IsSRUMAppUsageChecked { get; set; }

        public bool IsSRUMNetworkConnectivityChecked { get; set; }

        public bool IsBAMDataChecked { get; set; }
        
        public bool IsRecycleBinDataChecked { get; set; }

        public ViewModelCommand ExportDataCommand { get; set; }

        private async void ExportData(object parameter = null)
        {
            string exportDirName = _FOLDER_BASE_NAME + DateTime.Now.ToString("_dd_MM_yyyy__hh_mm_ss");

            Directory.CreateDirectory(exportDirName);

            IServiceFactory sf = ServiceFactoryProducer.GetServiceFactory();

            if (IsARPTableChecked)
            {
                IARPTableService ats = sf.CreateARPTableService();

                List<ARPEntry> entries = await Task.Run(() =>
                {
                    return ats.GetARPEntries();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "arp_table.json"), serialized);
                }
            }

            if (IsSharesChecked)
            {
                IShareService ss = sf.CreateShareService();

                List<Share> entries = await Task.Run(() =>
                {
                    return ss.GetShares();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "shares.json"), serialized);
                }
            }

            if (IsNetworkAdaptersChecked)
            {
                INetworkAdapterService nes = sf.CreateNetworkAdapterService();

                List<NetworkAdapter> entries = await Task.Run(() =>
                {
                    return nes.GetNetworkAdapters();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "network_adapters.json"), serialized);
                }
            }

            if (IsUserSessionsChecked)
            {
                IUserSessionService uss = sf.CreateUserSessionService();

                List<UserSession> entries = await Task.Run(() =>
                {
                    return uss.GetUserSessions();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "user_sessions.json"), serialized);
                }
            }

            if (IsComputerSessionsChecked)
            {
                IComputerSessionService css = sf.CreateComputerSessionService();

                List<ComputerSession> entries = await Task.Run(() =>
                {
                    return css.GetComputerSessions();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "computer_sessions.json"), serialized);
                }
            }

            if (IsIP4RoutesChecked)
            {
                IIP4RoutingTableService iprs = sf.CreateIP4RoutingTableService();

                List<IP4Route> entries = await Task.Run(() =>
                {
                    return iprs.GetRoutes();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "IP4_routes.json"), serialized);
                }
            }

            if (IsComputerSystemChecked)
            {
                IComputerSystemService css = sf.CreateComputerSystemService();

                ComputerSystem entry = await Task.Run(() =>
                {
                    return css.GetComputerSystem();
                });

                if (entry != null)
                {
                    string serialized = new MyJSONSerializer().Serialize(entry);
                    File.WriteAllText(Path.Combine(exportDirName, "computer_system.json"), serialized);
                }
            }

            if (IsEnvironmentVariablesChecked)
            {
                IEnvironmentVariableService evs = sf.CreateEnvironmentVariableService();

                List<EnvironmentVariable> entries = await Task.Run(() =>
                {
                    return evs.GetEnvironmentVariables();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "environment_variables.json"), serialized);
                }
            }

            if (IsStartupEntriesChecked)
            {
                IStartupEntryService ses = sf.CreateStartupEntryService();

                List<StartupEntry> entries = await Task.Run(() =>
                {
                    return ses.GetStartupEntries();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "startup.json"), serialized);
                }
            }

            if (IsRecentlyUsedChecked)
            {
                IRecentRunBarService rus = sf.CreateRecentRunBarService();
                IMainWindowCacheService mwcs = sf.CreateMainWindowCacheService();
                IRecentlyOpenedFileService rofs = sf.CreateRecentlyOpenedFileService();
                IRecentDocumentService rds = sf.CreateRecentDocumentService();

                List<OpenedFileEntry> entries = await Task.Run(() =>
                {
                    return rofs.GetRecentlyOpenedFiles();
                });

                List<RecentDocument> rdEntries = await Task.Run(() =>
                {
                    return rds.GetRecentDocuments();
                });

                List<MainWindowCacheEntry> wcEntries = await Task.Run(() =>
                {
                    return mwcs.GetMainWindowCache();
                });

                List<RunBarEntry> rbEntries = await Task.Run(() =>
                {
                    return rus.GetRecentlRunBarEntries();
                });



                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "recently_opened.json"), serialized);
                }

                if (rdEntries != null && rdEntries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(rdEntries);
                    File.WriteAllText(Path.Combine(exportDirName, "recent_documents.json"), serialized);
                }

                if (wcEntries != null && wcEntries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(wcEntries);
                    File.WriteAllText(Path.Combine(exportDirName, "window_cache.json"), serialized);
                }

                if (rbEntries != null && rbEntries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(rbEntries);
                    File.WriteAllText(Path.Combine(exportDirName, "run_bar.json"), serialized);
                }
            }

            if (IsUpdatesChecked)
            {
                IUpdateService us = sf.CreateUpdateService();

                List<Update> entries = await Task.Run(() =>
                {
                    return us.GetUpdates();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "updates.json"), serialized);
                }
            }

            if (IsProcessesChecked)
            {
                IProcessService ps = sf.CreateProcessService();

                List<Process> entries = await Task.Run(() =>
                {
                    return ps.GetProcesses();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "processes.json"), serialized);
                }
            }

            if (IsBIOSChecked)
            {
                IBIOSService bs = sf.CreateBIOSService();

                BIOS entry = await Task.Run(() =>
                {
                    return bs.GetBIOS();
                });

                if (entry != null)
                {
                    string serialized = new MyJSONSerializer().Serialize(entry);
                    File.WriteAllText(Path.Combine(exportDirName, "bios.json"), serialized);
                }
            }

            if (IsServicesChecked)
            {
                IWindowsServiceService wss = sf.CreateWindowsServiceService();

                List<WindowsService> entries = await Task.Run(() =>
                {
                    return wss.GetServices();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "windows_services.json"), serialized);
                }
            }

            if (IsWLANSessionsChecked)
            {
                IWLANSessionService wss = sf.CreateWLANSessionService();

                List<WLANSession> entries = await Task.Run(() =>
                {
                    return wss.GetWLANSessions();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "wlan_sessions.json"), serialized);
                }
            }

            if (IsInstalledProgramsChecked)
            {
                IInstalledProgramService ips = sf.CreateInstalledProgramService();

                List<InstalledProgram> entries = await Task.Run(() =>
                {
                    return ips.GetInstalledPrograms();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "installed_programs.json"), serialized);
                }
            }

            if (IsUSBDeviceHistoryChecked)
            {
                IUSBDeviceHistoryService uds = sf.CreateUSBDeviceHistoryService();

                List<USBDeviceHistoryEntry> entries = await Task.Run(() =>
                {
                    return uds.GetUSBDeviceHistory();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "usb_device_history.json"), serialized);
                }
            }

            if (IsUsersAndGroupsChecked)
            {
                IGroupUserService ugs = sf.CreateGroupUserService();

                Dictionary<UserGroup,List<User>> entries = await Task.Run(() =>
                {
                    return ugs.GetGroupUserDictionary();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "users_and_groups.json"), serialized);
                }
            }

            if (IsUserAssistChecked)
            {
                IUserAssistService uae = sf.CreateUserAssistService();

                List<UserAssistEntry> entries = await Task.Run(() =>
                {
                    return uae.GetUserAssistEntries();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "user_assist.json"), serialized);
                }
            }

            if (IsRecentAppsChecked)
            {
                IRecentAppService ras = sf.CreateRecentAppService();

                List<RecentAppEntry> entries = await Task.Run(() =>
                {
                    return ras.GetRecentAppEntries();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "recent_apps.json"), serialized);
                }
            }

            if (IsShimCacheChecked)
            {
                IShimCacheService scs = sf.CreateShimCacheService();

                List<ShimCacheEntry> entries = await Task.Run(() =>
                {
                    return scs.GetShimCacheEntries();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "shim_cache.json"), serialized);
                }
            }

            if (IsPrefetchChecked)
            {
                IPrefetchService ats = sf.CreatePrefetchService();

                List<PrefetchEntry> entries = await Task.Run(() =>
                {
                    return ats.GetPrefetchEntries();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "prefetch.json"), serialized);
                }
            }

            if (IsBAMDataChecked)
            {
                IBAMService bds = sf.CreateBAMService();

                List<BAMEntry> entries = await Task.Run(() =>
                {
                    return bds.GetBAMEntries();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName, "bam.json"), serialized);
                }
            }

            if (IsRecycleBinDataChecked)
            {
                IRecycleBinService rbs = sf.CreateRecycleBinService();

                List<RecycleBinEntry> entries = await Task.Run(() =>
                {
                    return rbs.GetRecycleBinEntries();
                });

                if (entries != null && entries.Count > 0)
                {
                    string serialized = new MyJSONSerializer().Serialize(entries);
                    File.WriteAllText(Path.Combine(exportDirName,"recycle_bin.json"), serialized);
                }
            }

        }

        private bool CanExportData(object parameter = null)
        {
            return true;
        }

        public DataExportViewModel()
        {
            ExportDataCommand = new ViewModelCommand(ExportData, CanExportData);
        }

    }
}
