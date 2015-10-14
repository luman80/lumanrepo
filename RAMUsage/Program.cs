using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Threading;
using System.Diagnostics;

namespace RAMUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_OperatingSystem");

                while (true)
                {
                    foreach (var mo in searcher.Get())
                    {
                        double free = Double.Parse(mo["FreePhysicalMemory"].ToString());
                        double total = Double.Parse(mo["TotalVisibleMemorySize"].ToString());
                        Console.WriteLine("Percentage used: {0}%", Math.Round(((total - free) / total * 100), 2));
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine(value: e.Message);
            }

        }
    }
}
