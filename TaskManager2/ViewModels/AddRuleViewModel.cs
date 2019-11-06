using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLibrary;

namespace TaskManager2.ViewModels
{
    class AddRuleViewModel : Screen
    {

        private ObservableCollection<string> processes = new ObservableCollection<string>();
        private ObservableCollection<string> types = new ObservableCollection<string>();
        private ObservableCollection<string> selctors = new ObservableCollection<string>();
        private string value;
        private ObservableCollection<string> triggers = new ObservableCollection<string>();

        public AddRuleViewModel()
        {
            //todo get server process
        }

        public ObservableCollection<string> Processes
        {
            get => processes;
            set => Set(ref processes, value);
        }

        public ObservableCollection<string> Types
        {
            get => types;
            set => Set(ref types, value);
        }

        public ObservableCollection<string> Selctors
        {
            get => selctors;
            set => Set(ref selctors, value);
        }

        public string Value
        {
            get => this.value;
            set => Set(ref this.value, value);
        }

        public ObservableCollection<string> Triggers
        {
            get => triggers;
            set => Set(ref triggers, value);
        }
    }
}
