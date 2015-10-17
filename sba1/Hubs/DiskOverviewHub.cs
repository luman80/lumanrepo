using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using System.Threading;

namespace sba1.Hubs
{
    public class DiskOverviewHub : Hub
    {
        private PerformanceCounter pc;
        private Timer timer;

        public void Init(int speed)
        {
            pc = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            pc.NextValue();
            timer = new Timer(OnTime, null, 0, speed);
        }

        private void OnTime(object state)
        {
            Clients.All.report(Math.Round(pc.NextValue()));
        }
    }
}