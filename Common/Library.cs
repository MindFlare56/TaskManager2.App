using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class Library
    {

        /* todo convert to method
        Group
        var result = rawDatas.GroupBy(pair => new ProcessKey(pair.Key).Name).ToDictionary(
            group => group.Key, group => group.ToDictionary(pair => pair.Key, pair => pair.Value)
        );
        */

        public static Dictionary<Key, Value> BytesToJson<Key, Value>(byte[] bytes)
        {
            string bytesString = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<Dictionary<Key, Value>>(bytesString);
        }

        public static string ListToString<T>(Collection<T> list)
        {            
            return string.Join(Environment.NewLine, list);
        }

        public static string ArrayToString<T>(T[] array)
        {
            return string.Join(Environment.NewLine, array);
        }

        public static string ToMultilinesString<T>(this Collection<T> list)
        {
            return ListToString<T>(list);
        }

        public static string SubArray<T>(this T[] array, string propertyName)
        {
            return ArrayToString<T>(array);
        }      

        public static Collection<FieldType> SubList<ListType, FieldType>(this Collection<ListType> list, Expression<Func<ListType, FieldType>> property)
        {
            var subList = new Collection<FieldType>();
            foreach (object item in list) {
                foreach (var field in GetProperties(item)) {
                    MemberExpression expressionBody = (MemberExpression) property.Body;                    
                    var propertyName = expressionBody.Member.Name;
                    if (field.Name.Equals(propertyName)) {                        
                        subList.Add((FieldType) field.GetValue(item));
                    }
                }
            }            
            return subList;
        }

        public static Collection<FieldInfo> GetFields(object instance)
        {
            var fieldsList = new Collection<FieldInfo>();
            Type type = instance.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var field in fields) {
                fieldsList.Add(field);
            }
            return fieldsList;
        }

        public static Collection<PropertyInfo> GetProperties(object instance)
        {
            var fieldsList = new Collection<PropertyInfo>();
            Type type = instance.GetType();
            var fields = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields) {
                fieldsList.Add(field);
            }
            return fieldsList;
        }

        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression) memberExpression.Body;
            return expressionBody.Member.Name;
        }

        public static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            MemberExpression memberExpression = (MemberExpression) property.Body;
            return memberExpression.Member.Name;
        }

        public static string PropertyName(this object field)
        {            
            return nameof(field);
        }

        public static string DataDirectory() 
        {
            return ProjectDirectory() + "/Data/";
        }

        public static string PresentDirectory()
        {
            return Environment.CurrentDirectory;                 
        }

        public static string ProjectDirectory() 
        {
            return Directory.GetParent(PresentDirectory()).Parent.FullName;
        }

        public static Boolean IsNumeric(string value) 
        {
            var regex = new Regex(@"^\d+$");
            return regex.IsMatch(value);
        }

        public static Boolean IsDotOrNumeric(string value)
        {
            var regex = new Regex(@"^[0-9.]+$");
            return regex.IsMatch(value);
        }

        public static Boolean IsAlpha(string value) 
        {
            var regex = new Regex(@"^[a-zA-Z]+$");
            return regex.IsMatch(value);
        }

        public static Boolean IsAlphaNumeric(string value) 
        {
            var regex = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return regex.IsMatch(value);
        }

        public static bool IsPasswordCompliant(string value)
        {            
            var uppercase = new Regex(@"^[A-Z]$").IsMatch(value);
            var lowercase = new Regex(@"^[a-z]$").IsMatch(value);            
            var number = new Regex(@"^[0-9]$").IsMatch(value);
            return value.Length >= 8 && uppercase && lowercase && number;            
        }        

        public static bool IsDecimal(string value)
        {
            var regex = new Regex(@"^[0-9]+((\.|,)[0-9]+)?$");
            return regex.IsMatch(value);
        }

        public static bool IsInteger(string value)
        {
            var regex = new Regex(@"^[0-9]+$");
            return regex.IsMatch(value);
        }

        public static bool IsSignedDecimal(string value)
        {
            var regex = new Regex(@"^-?[0-9]+((\.|,)[0-9]+)?$");
            return regex.IsMatch(value);
        }

        public static bool IsSignedInteger(string value)
        {
            var regex = new Regex(@"^-?[0-9]+$");
            return regex.IsMatch(value);
        }        
    }
}
