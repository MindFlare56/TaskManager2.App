using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Common
{
    public static class ExtensionMethods
    {       
        public static ObservableCollection<T> AddAll<T>(this ObservableCollection<T> observableCollection, T[] array)
        {            
            for (int i = 0; i < array.Length; ++i) {
                observableCollection.Add(array[i]);
            }
            return observableCollection;
        }

        public static ObservableCollection<T> AddAll<T>(this ObservableCollection<T> observableCollection, List<T> array)
        {            
            for (int i = 0; i < array.Count; ++i) {
                observableCollection.Add(array[i]);
            }
            return observableCollection;
        }

        public static ObservableCollection<T> AddAll<T>(this ObservableCollection<T> observableCollection, ObservableCollection<T> array)
        {            
            for (int i = 0; i < array.Count; ++i) {
                observableCollection.Add(array[i]);
            }
            return observableCollection;
        }        

        public static List<T> AddAll<T>(this List<T> list, T[] array)
        {
            list = new List<T>();
            for (int i = 0; i < array.Length; ++i) {
                list.Add(array[i]);
            }
            return list;
        }

        public static Action CallOnlyOnce(this Action action)
        {
            var context = new ContextCallOnlyOnce();
            Action ret = () => {
                if (false == context.AlreadyCalled) {
                    action();
                    context.AlreadyCalled = true;
                }
            };
            return ret;
        }

        public static List<string> AddBefore(this List<string> list, string text)
        {
            for (int i = 0; i < list.Count; ++i) {
                list[i] = text + list[i];
            }
            return list;
        }

        public static List<string> AddAfter(this List<string> list, string text)
        {
            for (int i = 0; i < list.Count; ++i) {
                list[i] = list[i] + text;
            }
            return list;
        }

        public static int ToInt(this string data)
        {
            return int.Parse(data);
        }
    }

    internal class ContextCallOnlyOnce
    {
        public bool AlreadyCalled;
    }
}
