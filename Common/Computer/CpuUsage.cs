using Common.Machine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class CpuUsage
    {

        private string percentage;
        private List<PerformanceCounter> counters;

        public CpuUsage(ProcessUsage processUsage = null, PerformanceCounter counter = null)
        {
            if (processUsage != null && counter != null) {
                SetPercentage(processUsage, counter);
            }
        }                

        private void SetPercentage(ProcessUsage processUsage, PerformanceCounter counter)
        {
            Percentage = Math.Round(counter.NextValue() / Environment.ProcessorCount, 2).ToString();
        }

        private bool CanAddUsage(string processName, bool withIdle)
        {
            return !withIdle && processName == "Idle";
        }

        public Dictionary<string, CpuUsage> GetUsages(List<ProcessUsage> processUsages, bool withIdle = false)
        {
            var dictionary = new Dictionary<string, CpuUsage>();
            Counters = CreatePerformanceCounters(processUsages);
            dictionary = GetPerformanceCountersValues(processUsages);            
            return dictionary;
        }

        private Dictionary<string, CpuUsage> GetPerformanceCountersValues(List<ProcessUsage> processUsages)
        {
            var dictionary = new Dictionary<string, CpuUsage>();            
            int i = 0;
            Thread.Sleep(1000);
            foreach (var counter in Counters) {
                var processUsage = processUsages[i];
                dictionary[processUsage.Key] = new CpuUsage(processUsage, counter);
                ++i;
            }
            return dictionary;
        }      

        private List<PerformanceCounter> CreatePerformanceCounters(List<ProcessUsage> processUsages)
        {
            var counters = new List<PerformanceCounter>();
            foreach (ProcessUsage processUsage in processUsages) {
                var counter = InitCounter(processUsage.Name);
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
            try {
                action();
            } catch (Exception exception) {                
                Console.WriteLine("Error " + exception);
            }
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
