using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using System.Threading;
using System.Management;

namespace sba1.Hubs
{
    public class RamOverviewHub : Hub
    {
        private ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from win32_operatingsystem");
        private Timer timer;

        public void Init(int speed)
        {
            timer = new Timer(OnTime, null, 0, speed);
        }

        private void OnTime(object state)
        {
            foreach (var mo in searcher.Get())
            {
                double free = Double.Parse(mo["FreePhysicalMemory"].ToString());
                double total = Double.Parse(mo["TotalVisibleMemorySize"].ToString());
                Clients.All.report(Math.Round(((total - free) / total * 100)));
            }
        }
    }
}