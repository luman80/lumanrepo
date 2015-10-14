using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNet.SignalR.Hubs;

namespace sba1.Hubs
{
    public class CpuInfoHub : Hub
    {
        private Timer timer;
        private Dictionary<PerformanceCounter, Queue<float>> data = new Dictionary<PerformanceCounter, Queue<float>>();

        public float[][] Init(int size, int speed)
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                var pc = new PerformanceCounter("Processor", "% Processor Time", Convert.ToString(i));
                pc.NextValue();
                data.Add(pc, new Queue<float>());
                for (int j = 0; j < size; j++)
                {
                    data[pc].Enqueue(0);
                }

            }
            var pct = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            pct.NextValue();
            data.Add(pct, new Queue<float>());
            for (int i = 0; i < size; i++)
            {
                data[pct].Enqueue(0);
            }
            timer = new Timer(OnTime, null, 0, speed);
            return ToArray();
        }

        public void OnTime(object info)
        {
            foreach (PerformanceCounter pc in data.Keys)
            {
                data[pc].Dequeue();
                data[pc].Enqueue(pc.NextValue());
            }
            Clients.All.getPoints(ToArray());
        }

        private float[][] ToArray()
        {
            float[][] result = new float[data.Count][];
            for (int i = 0; i < data.Count; i++)
            {
                result[i] = data.ElementAt(i).Value.ToArray();
            }
            return result;
        }
    }
}