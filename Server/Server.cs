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
            var counters = new List<PerformanceCounter>();            
            processes = Process.GetProcesses(Environment.MachineName);            
            WatchProcesses();
            LogProcessesDatas();
            while (true) ;
        }

        private void WatchProcesses()
        {
            var timedTask = new TimedTask(5000);
            timedTask.Watch(GetProcesses);
            timedTask.Watch(MemoryUsage);
            timedTask.Watch(CpuUsage);
        }

        private void LogProcessesDatas()
        {
            var timedTask = new TimedTask(refreshRate: 5000);
            //todo fix the second thread not activating properly
            //timedTask.Watch(MemoryUsage);
            timedTask.Watch(CpuUsage);
            timedTask.Start();
        }        

        private void GetProcesses()
        {
            processes = Process.GetProcesses(Environment.MachineName);            
        }                  

        private void WriteToJson(Dictionary<string, string> datas, string fileName = "")
        {
            FileHandler.WriteJsonInFile(datas, fileName);
        }

        private void MemoryUsage()
        {
            foreach (var process in processes) {
                var result = (double) process.WorkingSet64 / (double) systemMemory;
                result *= 100;
                MemoryUsages[process.ProcessName] = string.Format("{0:0.00}", result);
            }
            WriteToJson(MemoryUsages, "Memory.json");
        }        

        private void CpuUsage()
        {            
            GetPerformanceCountersValues(CreatePerformanceCounters());
            WriteToJson(processorUsages, "Cpu.json");
        }

        private void GetPerformanceCountersValues(List<PerformanceCounter> counters)
        {
            int i = 0;            
            int id = 0;
            Thread.Sleep(1000);
            foreach (var counter in counters) {
                if (processes[i].ProcessName != "Idle") {
                    if (processes[i].HandleCount != 1) { //id algorith doesn't work at all
                        ++id;
                        processorUsages[processes[i].ProcessName + "#" + id] = Math.Round(counter.NextValue() / Environment.ProcessorCount, 2).ToString();                                                                      
                    } else {
                        id = 0;
                        processorUsages[processes[i].ProcessName + "#" + id] = Math.Round(counter.NextValue() / Environment.ProcessorCount, 2).ToString();
                    }                    
                    ++i;
                }                
            }
        }

        private List<PerformanceCounter> CreatePerformanceCounters()
        {
            var counters = new List<PerformanceCounter>();
            foreach (Process process in processes) {
                var counter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                try {
                    if (counter.CategoryName == "Process") {
                        counter.NextValue();
                        counters.Add(counter);
                    } else {
                        Console.WriteLine("Found you nigga");
                    }
                } catch (Exception exception) {
                    Console.WriteLine(counter.CategoryName);
                    Console.WriteLine("Error " + exception);
                }                             
            }
            return counters;
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
