using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiskUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CMIV2", "select * from Win32_PerfRawData_PerfDisk_PhysicalDisk");
            var pc = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            pc.NextValue();

            while (true)
            {
                Console.WriteLine(pc.NextValue());
                Thread.Sleep(1000);
            }
        }
    }
}
