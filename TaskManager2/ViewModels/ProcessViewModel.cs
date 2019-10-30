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
using System.Windows.Controls;
using System.Data;
using WpfCommon;
using TaskManager2.Models;
using System.Threading;
using System.Windows.Threading;
using System.Timers;
using WpfLibrary;

namespace TaskManager2.ViewModels
{
    class ProcessViewModel: Screen
    {
        
        private ObservableCollection<Process> processes = new ObservableCollection<Process>();
        private ObservableCollection<ProcessData> collection = new ObservableCollection<ProcessData>();

        public ProcessViewModel(Process[] systemProcesses)
        {
            processes = processes?.AddAll(systemProcesses);                        
            Collection = new ObservableCollection<ProcessData> {
                new ProcessData {
                    Name = "Bob",
                    Pid = "0000",                    
                    Cpu = "10%",
                    Memory = "7%",
                    Children = new List<ProcessData> {
                        new ProcessData {
                            Name = "Bob",
                            Pid = "0001",                            
                            Cpu = "2%",
                            Memory = "5%"
                        },
                        new ProcessData {
                            Name = "Bob",
                            Pid = "0002",                            
                            Cpu = "8%",
                            Memory = "2%"
                        }
                    }
                }
            };
            Collection = Collection.AddAll(GetAllProcessDatas());

            string name = processes[2].ProcessName;
            string percentage = ((((ulong) test().GetAwaiter().GetResult()) / new ComputerInfo().TotalPhysicalMemory) * 100).ToString();
            Console.WriteLine(name + ": " + percentage + "%");
            //https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process.workingset64?view=netframework-4.8
            //todo use server to get data from .WorkingSet64 in await method

            OnUpdate();
        }

        private void OnUpdate()
        {
            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            myTimer.Interval = 30000;
            myTimer.Start();
        }

        private void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("updating");
            Collection = Collection.UpdateAll(GetAllProcessDatas(), App.Current);
        }

        private ObservableCollection<ProcessData> GetAllProcessDatas()
        {
            //todo replace with server values
            var collection = new ObservableCollection<ProcessData>();
            for (int i = 0; i < processes.Count; ++i) {
                var process = processes[i];
                collection.Add(new ProcessData {
                    Name = process.ProcessName,
                    Pid = process.Id.ToString(),                            
                    Cpu = "_2%",
                    Memory = "_5%"
                });
            }
            return collection;
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

        public ObservableCollection<ProcessData> Collection
        {
            get => collection;
            set => Set(ref collection, value);
        }
    }    
}
