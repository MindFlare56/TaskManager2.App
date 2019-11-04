using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Common
{
    public class UITimedTask
    {

        private DispatcherTimer dispatcherTimer;        

        public UITimedTask(int refreshRate = 500)
        {
            Init(refreshRate);
        }

        public UITimedTask(Task task, int refreshRate = 500)
        {
            Init(refreshRate);
            Watch(task);
            Start();
        }

        private void Init(int refreshRate)
        {
            RefreshRate = refreshRate;
            dispatcherTimer = new DispatcherTimer();
        }

        public void Start()
        {            
            dispatcherTimer.Start();             
        }

        public void Watch(Task task)
        {
            dispatcherTimer.Tick += new EventHandler((sender, eventArgs) => EventHandler(sender, eventArgs, task));
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(RefreshRate);            
        }

        private void EventHandler(object sender, EventArgs eventArgs, Task task)
        {
            
        }

        public int RefreshRate
        {
            get; set;
        }
    }
}
