using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Fields
{
    public class Username //todo suport other regex format
    {        

        public string Value
        {
            get; set;
        }

        public bool IsValid()
        {
            return IsUsername(Value);
        }

        public Username(string value = "")
        {
            Value = value;
        }

        public static implicit operator string(Username username)
        {
            return username?.Value;
        }

        public static implicit operator Username(string username)
        {
            return new Username(username);
        }

        public Boolean IsUsername()
        {            
            return Regex().IsMatch(Value);
        }

        public static Boolean IsUsername(string value)
        {            
            return Regex().IsMatch(value);
        }

        public static Regex Regex()
        {
            return new Regex(@"^[a-zA-Z0-9\s,]+$"); //alphaNumeric
        }
    }
}
