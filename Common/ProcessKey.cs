using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ProcessKey
    {

        public ProcessKey(string composedKey)
        {
            var parts = composedKey.Split('#');
            Name = parts[0];
            Id = parts[1];
        }

        public ProcessKey(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }

        public string Id { get; set; }
    }
}