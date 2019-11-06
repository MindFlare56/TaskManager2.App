using Common.Machine;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MemoryUsage
    {

        public static readonly int BYTE_TO_MEGABYTE_DIVIDER = 1048576; //1024^2
        public static readonly ulong SYSTEM_MEMORY = new ComputerInfo().TotalPhysicalMemory;        

        public static implicit operator MemoryUsage(ProcessUsage processUsage) => new MemoryUsage(processUsage: processUsage);

        public MemoryUsage(ProcessUsage processUsage = null)
        {
            if (processUsage != null) {
                SetValues(processUsage);
            }            
        }

        public void SetPercentage(ProcessUsage process)
        {
            double memory = process.WorkingSet64;
            double percentage = (memory / SYSTEM_MEMORY) * 100;
            Percentage = percentage.ToString();
        }

        public void SetMB(ProcessUsage processUsage)
        {
            MB = (processUsage.WorkingSet64 / BYTE_TO_MEGABYTE_DIVIDER).ToString();
        }

        public MemoryUsage Update(ProcessUsage processUsage)
        {
            SetValues(processUsage);
            return this;
        }

        public string FormatPercentage(int decimals)
        {
            string decimalsChar = "";
            for (int i = 0; i < decimals; ++i) {
                decimalsChar += "0";
            }            
            return string.Format("{0:0." + decimalsChar + "}", Percentage.ToDouble());
        }

        public Dictionary<string, MemoryUsage> GetUsages(List<ProcessUsage> processUsages, bool withIdle = false)
        {
            var dictionary = new Dictionary<string, MemoryUsage>();
            foreach (var processUsage in processUsages) {
                MemoryUsage memoryUsage = processUsage;
                if (CanAddUsage(processUsage.Name, withIdle)) {
                    dictionary[processUsage.Key] = processUsage;
                }
            }
            return dictionary;
        }

        private bool CanAddUsage(string processName, bool withIdle)
        {
            return !withIdle && processName == "Idle";
        }

        private void SetValues(ProcessUsage processUsage)
        {
            SetMB(processUsage);
            SetPercentage(processUsage);
        }
        
        
        public string FormatedPercentage {
            get => FormatPercentage(2);
        }        
        public string MB { get; set; }
        public string Percentage { get; set; }              
    }
}
