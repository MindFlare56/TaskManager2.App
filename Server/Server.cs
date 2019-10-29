using Common;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{    
    class Server
    {

        private Process[] processes;
        private Dictionary<string, string> memoryUsages = new Dictionary<string, string>();
        private Dictionary<string, string> processorUsages = new Dictionary<string, string>();
        private readonly ulong systemMemory = new ComputerInfo().TotalPhysicalMemory;

        /*
           processes = Process.GetProcesses(Environment.MachineName);
           foreach (var process in processes) {
               double value = (double) (process.WorkingSet64 * 100) / (double) systemMemory;       
               var result = string.Format("{0:0.00}", value);
               if (MemoryUsages.ContainsKey(process.ProcessName)) {
                   MemoryUsages[process.ProcessName] = result.ToString();
               } else {
                   MemoryUsages.Add(process.ProcessName, result.ToString());
               }                
           }
           var counters = new List<PerformanceCounter>();
           foreach (Process process in processes) {
               var counter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
               counter.NextValue();
               counters.Add(counter);
           }
           int i = 0;
           Thread.Sleep(1000);
           foreach (var counter in counters) {
               Console.WriteLine(processes[i].ProcessName + "       | CPU% " + (Math.Round(counter.NextValue(), 1)));
               ++i;
           }            
           */
        //todo test to see if process are being changed after 5 seconds

        public Server()
        {           
            WatchProcesses();            
            while (true) ;
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
            processes = await new Task<Process[]>(() => Process.GetProcesses(Environment.MachineName));
        }

        private async Task MemoryUsage()
        {
            foreach (var process in processes) {
                var result = (ulong) process.WorkingSet64 / systemMemory;
                result *= 100;
                MemoryUsages.Add(process.ProcessName, result.ToString());
            }            
        }

        private async Task CpuUsage()
        {
            //todo refactor
            //public PerformanceCounter(string categoryName, string counterName, string instanceName, string machineName);
            foreach (Process process in processes) {
                var performanceCounter = new PerformanceCounter(
                    "Process", "% Processor Time", process.ProcessName, Environment.MachineName
                );
                double value = performanceCounter.NextValue();
                processorUsages.Add(process.ProcessName, value.ToString());
                Thread.Sleep(9);
            }          
        }

        public Dictionary<string, string> MemoryUsages
        {
            get => memoryUsages;
            set => memoryUsages = value;
        }

        public Dictionary<string, string> ProcessorUsages
        {
            get => processorUsages;
            set => processorUsages = value;
        }
    }
}
