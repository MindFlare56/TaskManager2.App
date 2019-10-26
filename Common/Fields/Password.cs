using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Fields
{
    public class Password //todo suport other regex format
    {

        public string Value
        {
            get; set;
        }

        public bool IsValid()
        {
            return IsPassword(Value);
        }

        public Password(string value = "")
        {
            Value = value;
        }

        public static implicit operator string(Password password)
        {
            return password?.Value;
        }

        public static implicit operator Password(string password)
        {
            return new Password(password);
        }

        public Boolean IsPassword()
        {
            return Validation(Value);
        }

        public static Boolean IsPassword(string value)
        {
            return Validation(value);
        }

        public static bool Validation(string value)
        {
            var uppercase = value.Any(char.IsUpper);            
            var lowercase = value.Any(char.IsLower);
            var number = value.Any(char.IsDigit);
            return value.Length >= 8 && uppercase && lowercase && number;            
        }
    }
}
