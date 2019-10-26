using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;

namespace WpfLibrary {
    public static class ExtensionsMethod { //todo refactor between WpfCommon and Common     

        public static ItemCollection AddAll<T>(this ItemCollection items, List<T> list)
        {
            list.ForEach((item) => items.Add(item));
            return items;
        }
    } 
}