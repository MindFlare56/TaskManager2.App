using Common;
using Common.Computer;
using Common.Machine;
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

        private List<ProcessUsage> processUsages = new List<ProcessUsage>();        
        private ComputerUsage computerUsage;

        public Server()
        {
            Start();
        }

        public void Start()
        {
            Init();
        }

        private void Init()
        {
            ClearFiles();
            computerUsage = new ComputerUsage();            
            WatchProcesses();
            while (true);
        }

        private void ClearFiles()
        {
            FileHandler.ClearFile();
            FileHandler.ClearFile("Cpu.json");
            FileHandler.ClearFile("Memory.json");            
        }

        private void WatchProcesses()
        {
            var timedTask = new TimedTask(1000);
            timedTask.Watch(computerUsage.Update);            
            timedTask.Watch(LogComputerUsage);
            timedTask.Start();            
        }

        private void LogComputerUsage()
        {            
            WriteToJson(computerUsage);
        }              

        private void WriteToJson(ComputerUsage computerUsage, string fileName = "")
        {
            //todo handle being used by another process exception
            try {
                FileHandler.WriteJsonInFile(computerUsage, fileName);
            } catch (Exception exception) {
                Console.WriteLine("Error " + exception);
            }            
        }                              
    }
}
