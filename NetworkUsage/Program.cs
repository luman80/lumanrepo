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
        enum Scales
        {
            Kb1 = 125,
            Kb100 = Kb1 * 100,
            Mb1 = Kb100 * 10,
            Mb100 = 100 * Mb1,
            Gb1 = 10 * Mb100
        };

        static void Main(string[] args)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "select * from win32_networkadapter where netenabled = true");
            foreach (var mo in mos.Get())
            {
                Console.WriteLine("Network Adapter: " + mo["Name"]);
                var maxSpeed = (Double.Parse(mo["Speed"].ToString()) / 1000) / 1000;

                var pc = new PerformanceCounter("Network Adapter", "Bytes Total/sec", mo["Name"].ToString().Replace('(', '[').Replace(')', ']'));
                pc.NextValue();

                while (true)
                {
                    float origValue = pc.NextValue();
                    Console.WriteLine(pc.InstanceName + ":> " + origValue + " Bps. Axis: [{0},{1}]. Percentage: {2}", 0, p.scale, percent);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
