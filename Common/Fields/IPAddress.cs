using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Fields
{
    public class IPAddress
    {        

        public string Value 
        {
            get; set;
        }

        public bool IsValid()
        {
            return IsIPAddress(Value);
        }

        public IPAddress(string value = "") 
        {
            Value = value;
        }

        public static implicit operator string(IPAddress iPAddress) 
        {            
            return iPAddress?.Value;            
        }

        public static implicit operator IPAddress(string iPAddress) 
        {
            return new IPAddress(iPAddress);
        }

        public Boolean IsIPAddress()
        {
            return Regex().IsMatch(Value);
        }

        public static Boolean IsIPAddress(string value)
        {
            return Regex().IsMatch(value);
        }

        public static Regex Regex()
        {
            return new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
        }

        [Obsolete]
        public static long iPV4ToLong(string iPAddress)
        {
            return System.Net.IPAddress.Parse(iPAddress).Address;
        }

        public static byte[] ToBytes(string iPAddress)
        {
            return System.Net.IPAddress.Parse(iPAddress).GetAddressBytes();
        }

        public static System.Net.IPAddress ToNet(string iPAddress)
        {
            try {                
                return System.Net.IPAddress.Parse(iPAddress);                               
            } catch (ArgumentNullException e) {
                Console.WriteLine("ArgumentNullException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            } catch (FormatException e) {
                Console.WriteLine("FormatException caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            } catch (Exception e) {
                Console.WriteLine("Exception caught!!!");
                Console.WriteLine("Source : " + e.Source);
                Console.WriteLine("Message : " + e.Message);
            }
            return null;
        }
    }
}
