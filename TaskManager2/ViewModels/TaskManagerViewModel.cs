using Caliburn.Micro;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskManager2.ViewModels
{
    public class TaskManagerViewModel : Conductor<object>
    {

        private IWindowManager windowManager;
        private Process server;

        public TaskManagerViewModel()
        {
            var process = new ProcessStartInfo(FileHandler.GetPathFromSolution(@"Server\bin\Debug\Server.exe"));
            process.WindowStyle = ProcessWindowStyle.Hidden;
            server = Process.Start(process);            
            windowManager = new WindowManager();            
        }

        public void ProcessButton()
        {
            ActivateItem(new ProcessViewModel());
        }

        public void RuleButton()
        {
            ActivateItem(new RuleViewModel());
        }

        public void AddRule()
        {
            dynamic settings = new ExpandoObject();            
            settings.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            settings.Title = "Add a rule";
            windowManager.ShowDialog(new AddRuleViewModel(), null, settings);
        }

        public void AddProgram()
        {
            Console.WriteLine("Add program");            
        }

        public override void CanClose(Action<bool> callback)
        {
            server.Close();
            callback(true);
        }
    }   
}
