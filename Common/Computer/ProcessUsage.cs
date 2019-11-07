using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Machine
{
    public class ProcessUsage : Process
    {

        private Process process;

        public ProcessUsage(Process process = null)
        {
            Process = process;
        }

        public static List<ProcessUsage> GetUsages(string machineName = "")
        {
            var list = new List<ProcessUsage>();
            machineName = machineName == "" ? Environment.MachineName : machineName;          
            foreach (var process in GetProcesses(machineName)) {
                list.Add(new ProcessUsage(process));
            }
            return list;
        }

        private void SetKey()
        {
            Key = Name + "#" + Id;
        }
        public string Name
        {
            get => ProcessName;            
        }        
        public string Key
        {
            get; private set;
        }
        public MemoryUsage MemoryUsage
        {
            get; private set;
        }
        public CpuUsage CpuUsage
        {
            get; private set;
        }
        public Process Process
        {
            get => process;
            set => process = value;
        }        
    }
}
