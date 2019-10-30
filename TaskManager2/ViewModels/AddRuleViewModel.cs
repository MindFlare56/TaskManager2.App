using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager2.ViewModels
{
    class AddRuleViewModel : Screen
    {

        private BindableCollection<string> processes = new BindableCollection<string>();
        private BindableCollection<string> types = new BindableCollection<string>();
        private BindableCollection<string> selctors = new BindableCollection<string>();
        private string value = "0";
        private BindableCollection<string> triggers = new BindableCollection<string>();

        public AddRuleViewModel(Process[] systemProcesses)
        {
            //processes = systemProcesses.Select
        }

        public BindableCollection<string> Processes
        {
            get => processes;
            set => Set(ref processes, value);
        }

        public BindableCollection<string> Types
        {
            get => types;
            set => Set(ref types, value);
        }

        public BindableCollection<string> Selctors
        {
            get => selctors;
            set => Set(ref selctors, value);
        }

        public string Value
        {
            get => this.value;
            set => Set(ref this.value, value);
        }

        public BindableCollection<string> Triggers
        {
            get => triggers;
            set => Set(ref triggers, value);
        }
    }
}
