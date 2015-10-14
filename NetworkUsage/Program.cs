using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher(
                "root\\CIMV2", "select * from win32_networkadapter where NetEnabled = True");
            foreach (var mo in mos.Get())
            {
                var maxSpeed = (Double.Parse(mo["Speed"].ToString()) / 1000) / 1000;
                var pc = new PerformanceCounter("Network Adapter", "Bytes Total/sec", mo["Name"].ToString());
                pc.NextValue();

                while (true)
                {
                    var value = (((Math.Round(pc.NextValue()) * 8) / 1024) / 1024);
                    Console.WriteLine(pc.InstanceName + ":> " + value + " B/s ({0} Mbps out of {1} Mbps, that is {2} %)", value, maxSpeed, (value * 100) / maxSpeed);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
