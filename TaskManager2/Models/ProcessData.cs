using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager2.Models
{
    class ProcessData
    {

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
            get; set;
        }

        public string Memory
        {
            get; set;
        }

        public List<ProcessData> Children
        {
            get; set;
        }
    }
}
