using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager2.ViewModels
{
    public class TaskManagerViewModel : Conductor<object>
    {
        public TaskManagerViewModel()
        {

        }

        public void ProcessButton()
        {
            ActivateItem(new ProcessViewModel());
        }

        public void RuleButton()
        {
            ActivateItem(new RuleViewModel());
        }
    }   
}
