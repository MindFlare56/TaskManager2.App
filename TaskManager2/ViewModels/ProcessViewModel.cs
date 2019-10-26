using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Diagnostics;
using Common;
using Microsoft.VisualBasic.Devices;

namespace TaskManager2.ViewModels
{
    class ProcessViewModel : Screen
    {

        private Process[] systemProcesses = Process.GetProcesses(Environment.MachineName);
        private ObservableCollection<Process> processes = new ObservableCollection<Process>();

        public ProcessViewModel()
        {
            if (systemProcesses != null)
            processes = processes.AddAll(systemProcesses);
            string name = processes[2].ProcessName;
            string percentage = ((((ulong) test().GetAwaiter().GetResult()) / new ComputerInfo().TotalPhysicalMemory ) * 100).ToString();
            Console.WriteLine(name + ": " + percentage + "%");
            //https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.workingset64?view=netframework-4.8
            //todo use server to get data from .WorkingSet64 in await method
        }

        private async Task<long> test()
        {
            return processes[0].WorkingSet64;
        }

        public ObservableCollection<Process> Processes
        {
            get => processes;
            set => Set(ref processes, value);
        }
    }
}
