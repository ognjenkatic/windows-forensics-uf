using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using WinFo.Model.Configuration;
using WinFo.Service.MyDebug;

namespace WinFo.Service.Configuration.Win7
{
    /// <summary>
    /// Service responsable for fetching information about the routing
    /// </summary>
    public class Win7IP4RoutingTableService : IIP4RoutingTableService
    {
        /// <summary>
        /// Gets a list of existing routes
        /// </summary>
        /// <returns>The list of routes</returns>
        public List<IP4Route> GetRoutes()
        {
            List<IP4Route> routes = new List<IP4Route>();
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT Age, Caption, Destination, InterfaceIndex, " +
                    "Mask, Metric1, Metric2, Metric3, Metric4, Metric5, Name, Protocol FROM Win32_IP4RouteTable");
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {

                    IP4Route rt         = new IP4Route();
                    rt.Age              = Convert.ToInt32(mo["Age"]);
                    rt.Caption          = Convert.ToString(mo["Caption"]);
                    rt.Destination      = Convert.ToString(mo["Destination"]);
                    rt.InterfaceIndex   = Convert.ToInt32(mo["InterfaceIndex"]);
                    rt.Mask             = Convert.ToString(mo["Mask"]);
                    rt.Metric1          = Convert.ToInt32(mo["Metric1"]);
                    rt.Metric2          = Convert.ToInt32(mo["Metric2"]);
                    rt.Metric3          = Convert.ToInt32(mo["Metric3"]);
                    rt.Metric4          = Convert.ToInt32(mo["Metric4"]);
                    rt.Metric5          = Convert.ToInt32(mo["Metric5"]);
                    rt.Name             = Convert.ToString(mo["Name"]);

                    UInt32 id        = Convert.ToUInt32(mo["Protocol"]);

                    // Decode the id to string (based on online documentation)
                    switch (id)
                    {
                        case (1):
                            {
                                rt.Protocol = "Other";
                                break;
                            }
                        case (2):
                            {
                                rt.Protocol = "Local";
                                break;
                            }
                        case (3):
                            {
                                rt.Protocol = "Netmgmt";
                                break;
                            }
                        case (4):
                            {
                                rt.Protocol = "Icmp";
                                break;
                            }
                        case (5):
                            {
                                rt.Protocol = "Egp";
                                break;
                            }
                        case (6):
                            {
                                rt.Protocol = "Ggp";
                                break;
                            }
                        case (7):
                            {
                                rt.Protocol = "Hello";
                                break;
                            }
                        case (8):
                            {
                                rt.Protocol = "Rip";
                                break;
                            }
                        case (9):
                            {
                                rt.Protocol = "Is-is";
                                break;
                            }
                        case (10):
                            {
                                rt.Protocol = "Es-is";
                                break;
                            }
                        case (11):
                            {
                                rt.Protocol = "CiscoIgrp";
                                break;
                            }
                        case (12):
                            {
                                rt.Protocol = "BbnSpfigp";
                                break;
                            }
                        case (13):
                            {
                                rt.Protocol = "Ospf";
                                break;
                            }
                        case (14):
                            {
                                rt.Protocol = "Bgp";
                                break;
                            }
                    }
                    routes.Add(rt);
                }



            }
            catch (Exception exc)
            {
                MyDebugger.Instance.LogMessage(exc, DebugVerbocity.Exception);
            }

            return routes;
        }
    }
}
