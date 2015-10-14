using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Management;

namespace ConsoleApplication1
{
    class Program
    {
        // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Wbem\CIMOM\AllowAnonymousCallback = 1 !!! and run app as admin!!!
        static void Main(string[] args)
        {
            ManagementEventWatcher pstart = new ManagementEventWatcher("select * from win32_processstarttrace where");
            ManagementEventWatcher pstop = new ManagementEventWatcher("select * from win32_processstoptrace where");

            pstart.EventArrived += pstart_EventArrived;
            pstart.Start();
            pstop.EventArrived += pstop_EventArrived;
            pstop.Start();

            Console.WriteLine("Listening to NOTEPAD processes start/stop events...");
            for (ConsoleKeyInfo ki = new ConsoleKeyInfo(); ki.Key != ConsoleKey.Enter; ki = Console.ReadKey()) ;
            Console.WriteLine("Stopping listeners...");

            pstart.Stop();
            pstop.Stop();
        }

        static void pstop_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            string pid = e.NewEvent.Properties["ProcessID"].Value.ToString();
            string ppid = e.NewEvent.Properties["ParentProcessID"].Value.ToString();
            Console.WriteLine("Process STOPPED:> '" + name + "'[" + pid + "," + ppid + "]");
        }

        static void pstart_EventArrived(object sender, EventArrivedEventArgs e)
        {
            string name = e.NewEvent.Properties["ProcessName"].Value.ToString();
            string pid = e.NewEvent.Properties["ProcessID"].Value.ToString();
            string ppid = e.NewEvent.Properties["ParentProcessID"].Value.ToString();
            Console.WriteLine("Process STARTED:> '" + name +"'[" + pid + "," + ppid + "]");
        }
    }
}
