using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class MemoryHandler
    {
        private Process[] processes;
        public static readonly ulong SYSTEM_MEMORY = new ComputerInfo().TotalPhysicalMemory;

        public MemoryHandler(Process[] processes)
        {
            this.processes = processes;
        }

        public string GetPercentage(Process process)
        {
            double memory = process.WorkingSet64;
            double percentage = (memory / SYSTEM_MEMORY) * 100;
            return percentage.ToString();
        }

        public string GetMB(Process Process)
        {
            return (Process.WorkingSet64 / BYTE_TO_MEGABYTE_DIVIDER).ToString();
        }

        public void MemoryUsage(bool withIdle = false)
        {
            foreach (var process in processes)
            {
                if (CanAddUsage(process.ProcessName, withIdle))
                {
                    GetMB(process);
                    GetPercentage(process);
                    //todo notify
                }
            }
        }
    }
}
