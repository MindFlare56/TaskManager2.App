using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Fields
{
    public class Port
    {
        public string Value
        {
            get; set;
        }

        public bool IsValid()
        {
            return IsPort(Value);
        }

        public Port(string value = "")
        {
            Value = value;
        }

        public static implicit operator string(Port port)
        {
            return port?.Value;
        }

        public static implicit operator Port(string port)
        {
            return new Port(port);
        }

        public Boolean IsPort()
        {
            return Regex().IsMatch(Value);
        }

        public static Boolean IsPort(string value)
        {
            return Regex().IsMatch(value);
        }

        public static Regex Regex()
        {
            return new Regex(@"^[0-9]+$");
        }
    }
}
