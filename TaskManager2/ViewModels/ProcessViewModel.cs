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
        
        private ObservableCollection<ProcessTree> collection = new ObservableCollection<ProcessTree>();
        private ComputerInfo computerInfo;

        public ProcessViewModel()
        {                                                
            OnUpdate();
        }

        private void OnUpdate()
        {
            //var uITimedTask = new UITimedTask(UpdateDatas, 3000);
        }
        /*
        private void UpdateDatas()
        {            
            Collection = new ObservableCollection<ProcessTree>(GetProcessTree());
        }

        private ComputerInfo GetRawDatas()
        {
            string path = FileHandler.GetPathFromSolution(@"Server\bin\Debug\Data.json");
            return FileHandler.ReadJsonInFile<ComputerInfo>(path: path);
        }

        
        private Dictionary<string, Dictionary<string, ProcessDatas>> GroupDatas()
        {            
            return GetRawDatas().GroupBy(pair => new ProcessKey(pair.Key).Name).ToDictionary(
                group => group.Key, group => group.ToDictionary(pair => pair.Key, pair => pair.Value)
            );
        }
        
        private ObservableCollection<ProcessTree> GetProcessTree()
        {
            var collection = new Collection<ProcessTree>();
            datas = GroupDatas();
            foreach (var data in datas) {
                collection.Add(ProcessTree.FromCollection(data));                
            }
            return new ObservableCollection<ProcessTree>(collection);
        }
        */
        public ObservableCollection<ProcessTree> Collection
        {
            get => collection;
            set => Set(ref collection, value);
        }
    }    
}
