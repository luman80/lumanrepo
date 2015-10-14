using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EventLog elog = new EventLog("System"))
            {
                for (int i = elog.Entries.Count - 1; i >= 0; i--)
                {
                    var entry = elog.Entries[i];
                    Console.WriteLine(entry.EntryType + " " + entry.TimeGenerated + " " + entry.Source + " " + entry.Message);
                }
                Console.WriteLine("Dumped " + elog.Entries.Count + " system events. Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
