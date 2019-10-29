using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfLibrary {
    public static class ExtensionsMethod { //todo refactor between WpfCommon and Common             

        public static ItemCollection AddAll<T>(this ItemCollection items, List<T> list)
        {
            list.ForEach((item) => items.Add(item));
            return items;
        }

        public static ObservableCollection<T> UpdateAll<T>(this ObservableCollection<T> observableCollection, ObservableCollection<T> array, Application context)
        {
            for (int i = 0; i < array.Count; ++i) {
                if (!observableCollection.Contains(array[i])) {
                    context.Dispatcher.Invoke(delegate
                    {
                        observableCollection.Add(array[i]);
                    });
                }
            }
            return observableCollection;
        }
    } 
}