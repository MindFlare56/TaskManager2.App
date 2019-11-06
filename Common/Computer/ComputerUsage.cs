using Common.Machine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Computer
{
    public class ComputerUsage
    {

        public ComputerUsage()
        {            
            ProcessUsages = ProcessUsage.GetUsages();
        }

        public void Update()
        {
            UpdateProcesses(OnUpdate: () => {                
                UpdateMemory();
                UpdateCpu();
            });            
        }

        private void UpdateCpu()
        {
            ClearUnmatchingKeys(ProcessUsages, CpuUsages);
            CpuUsages = new CpuUsage().GetUsages(ProcessUsages);
        }

        private void UpdateMemory()
        {
            ClearUnmatchingKeys(ProcessUsages, MemoryUsages);
            MemoryUsages = new MemoryUsage().GetUsages(ProcessUsages);
        }

        private void UpdateProcesses(Action OnUpdate)
        {
            ProcessUsages = ProcessUsage.GetUsages();
            OnUpdate();
        }

        private void ClearUnmatchingKeys<T>(List<ProcessUsage> processUsages, Dictionary<string, T> usages)
        {
            foreach (var process in processUsages) {
                if (!usages.ContainsKey(process.ProcessName)) {
                    usages.Remove(process.ProcessName);
                }
            }
        }

        public List<ProcessUsage> ProcessUsages
        {
            get; set;
        }

        public Dictionary<string, MemoryUsage> MemoryUsages
        {
            get; set;
        }

        public Dictionary<string, CpuUsage> CpuUsages
        {
            get; set;
        }
    }
}
