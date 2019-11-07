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
        private string percentage;
        private List<PerformanceCounter> counters;
        private CpuHandler cpuHandler;
        private MemoryHandler memoryHandler;

        public Server()
        {
            processes = Process.GetProcesses(Environment.MachineName);
            cpuHandler = new CpuHandler(processes);
            memoryHandler = new MemoryHandler(processes);                                   
            WatchProcesses();            
            while (true) ;
        }

        private void WatchProcesses()
        {
            var timedTask = new TimedTask(5000);
            timedTask.Watch(GetProcesses);
            timedTask.Watch(cpuHandler.CpuUsage);
            timedTask.Watch(() => memoryHandler.MemoryUsage());
            timedTask.Start();
        }  

        private void GetProcesses()
        {
            processes = Process.GetProcesses(Environment.MachineName);            
            //todo return process as list and remove idle
        }                                                      

        public string FormatPercentage(string percentage, int decimals)
        {
            string decimalsChar = "";
            for (int i = 0; i < decimals; ++i)
            {
                decimalsChar += "0";
            }
            return string.Format("{0:0." + decimalsChar + "}", percentage.ToDouble());
        }        

        public string Percentage
        {
            get => percentage;
            set => percentage = value;
        }
        public List<PerformanceCounter> Counters
        {
            get => counters;
            set => counters = value;
        }
    }
}
