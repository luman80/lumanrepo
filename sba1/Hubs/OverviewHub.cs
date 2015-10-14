using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using System.Threading;

namespace sba1.Hubs
{
    public class OverviewHub : Hub
    {
        private PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private Timer timer;

        public void Init(int speed)
        {
            cpu.NextValue();
            timer = new Timer(OnTime, null, 0, speed);
        }

        private void OnTime(object state)
        {
            Clients.All.report(cpu.NextValue());
        }
    }
}