using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class CpuHandler
    {
        private Process[] processes;
        private List<PerformanceCounter> counters;
        public static readonly int BYTE_TO_MEGABYTE_DIVIDER = 1048576; //1024^2

        public CpuHandler(Process[] processes)
        {
            this.processes = processes;
            counters = new List<PerformanceCounter>();
        }

        public void CpuUsage()
        {
            Counters = CreatePerformanceCounters();
            //todo notify in           
            GetPerformanceCountersValues();
        }

        private void GetPerformanceCountersValues()
        {
            Thread.Sleep(1000);
            foreach (var counter in Counters)
            {
                GetCpuPercentage(counter);
                //todo notify here
            }
        }

        private List<PerformanceCounter> CreatePerformanceCounters()
        {
            var counters = new List<PerformanceCounter>();
            foreach (Process process in processes)
            {
                var counter = InitCounter(process.ProcessName);
                counters.Add(counter);
            }
            return counters;
        }

        private PerformanceCounter InitCounter(string processName)
        {
            var counter = new PerformanceCounter("Process", "% Processor Time", processName);
            TryAddingCounter(() => {
                counter.NextValue();
            });
            return counter;
        }

        private void TryAddingCounter(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error " + exception);
            }
        }

        private void GetCpuPercentage(PerformanceCounter counter)
        {
            Percentage = Math.Round(counter.NextValue() / Environment.ProcessorCount, 2).ToString();
        }
    }
}
