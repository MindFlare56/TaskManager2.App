using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TaskManager2.ViewModels
{
    class ProcessViewModel : Screen
    {

        //todo
        public ProcessViewModel()
        {
            var list = new List<string>();
            list.Add("bob");
            list.Add("bob2");
            list.Add("bob3");
            GridData = new ListCollectionView(list);
        }

        ListCollectionView GridData
        {
            get; set;
        }
    }
}
