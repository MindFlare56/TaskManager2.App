using Caliburn.Micro;
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
        private Process[] systemProcesses = Process.GetProcesses(Environment.MachineName);

        public TaskManagerViewModel()
        {
            windowManager = new WindowManager();            
        }

        public void ProcessButton()
        {
            ActivateItem(new ProcessViewModel(systemProcesses));
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
            windowManager.ShowDialog(new AddRuleViewModel(systemProcesses), null, settings);
        }

        public void AddProgram()
        {
            Console.WriteLine("Add program");            
        }
    }   
}
