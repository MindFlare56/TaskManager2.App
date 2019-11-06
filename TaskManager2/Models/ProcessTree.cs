using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Computer;

namespace TaskManager2.Models
{
    class ProcessTree
    {
        private Dictionary<string, Dictionary<string, ComputerUsage>> datas;
        private string memory;
        private string cpu;

        public ProcessTree()
        {

        }

        public ProcessTree(Dictionary<string, Dictionary<string, ComputerUsage>> datas)
        {
            this.datas = datas;
        }

        public string Pid
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Cpu
        {
            get => cpu + "%";
            set => cpu = value;
        }

        public string Memory
        {
            get => memory + "%";
            set => memory = value;
        }

        public List<ProcessTree> Children
        {
            get; set;
        }

        internal static ProcessTree FromCollection(KeyValuePair<string, Dictionary<string, ComputerUsage>> data)
        {
            if (data.Value.Count > 1) {
                return AddMultiple(data);
            }
            return AddSingle(data);            
        }

        private static ProcessTree AddMultiple(KeyValuePair<string, Dictionary<string, ComputerUsage>> datas)
        {            
            var value = GetValuesSum(datas.Value);            
            var root = new ProcessTree {
                Name = datas.Key,
                Pid = "",
                Cpu = value.Key,
                Memory = value.Value,
                Children = GetChilds(datas)
            };
            return root;
        }

        private static List<ProcessTree> GetChilds(KeyValuePair<string, Dictionary<string, ComputerUsage>> datas)
        {
            var list = new List<ProcessTree>();
            foreach (var data in datas.Value) {
                var key = new ProcessKey(data.Key);
                list.Add(new ProcessTree {
                    Name = key.Name,
                    Pid = key.Id,
                    Cpu = data.Value.CpuUsage,
                    Memory = data.Value.MemoryUsage
                });                                
            }
            return list;
        }

        private static KeyValuePair<string, string> GetValuesSum(Dictionary<string, ComputerUsage> value)
        {
            var cpuSum = value.Sum(v => v.Value.CpuUsage.ToDouble()).ToString();
            var memorySum = value.Sum(v => v.Value.MemoryUsage.ToDouble()).ToString();
            return new KeyValuePair<string, string>(cpuSum, memorySum);
        }

        private static ProcessTree AddSingle(KeyValuePair<string, Dictionary<string, ComputerUsage>> datas)
        {
            var data = datas.Value.ElementAt(0);
            var key = new ProcessKey(data.Key);            
            return new ProcessTree {
                Name = key.Name,
                Pid = key.Id,
                Cpu = data.Value.CpuUsage,
                Memory = data.Value.MemoryUsage
            };            
        }        
    }
}
