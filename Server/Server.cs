using Common;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{    
    class Server
    {

        private Process[] processes;
        private List<string> memoryUsages = new List<string>();
        private readonly ulong systemMemory = new ComputerInfo().TotalPhysicalMemory;        

        public Server()
        {
            WatchProcesses();
        }

        private void WatchProcesses()
        {
            var timedTask = new TimedTask(GetProcesses(), 5000);            
        }

        private void LogProcessesDatas()
        {
            var timedTask = new TimedTask();
            timedTask.Watch(MemoryUsage());
            timedTask.Watch(CpuUsage());
            timedTask.Start();
        }

        private async Task GetProcesses()
        {
            processes = Process.GetProcesses(Environment.MachineName);
        }

        private async Task MemoryUsage()
        {            
            foreach (Process process in processes) {
                var result = process.TotalProcessorTime;
                result *= 100;
                MemoryUsages.Add(result.ToString());
            }                        
        }

        private async Task CpuUsage()
        {
            foreach (Process process in processes) {
                var result = (ulong) process.WorkingSet64 / systemMemory;
                result *= 100;
                MemoryUsages.Add(result.ToString());
            }
        }

        public List<string> MemoryUsages
        {
            get => memoryUsages;
            set => memoryUsages = value;
        }
    }
}
