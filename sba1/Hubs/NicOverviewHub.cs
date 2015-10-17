using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Management;
using System.Diagnostics;
using System.Threading;

namespace sba1.Hubs
{
    public class NicOverviewHub : Hub
    {
        ManagementObjectSearcher mos;
        ManagementBaseObject mbo;
        PerformanceCounter pc;
        Timer timer;
        double max;

        public void Init(int speed)
        {
            mos = new ManagementObjectSearcher("root\\CIMV2", "select * from win32_networkadapter where netenabled = true");
            foreach(var o in mos.Get()) {
                mbo = o;
                break;
            }

            timer = new Timer(OnTime, null, 0, speed);

            string pcName = mbo["Name"].ToString().Replace('(','[').Replace(')',']');
            pc = new PerformanceCounter("Network Adapter", "Bytes Total/sec", pcName);
            pc.NextValue();

            max = Double.Parse(mbo["Speed"].ToString());
        }

        private void OnTime(object state)
        {
            Clients.All.report(Math.Round(pc.NextValue() * 8), max);
        }
    }
}