using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Common
{
    public class TimedTask
    {

        private DispatcherTimer dispatcherTimer;        

        public TimedTask(int refreshRate = 500)
        {            
            RefreshRate = refreshRate;
            dispatcherTimer = new DispatcherTimer();            
        }

        public TimedTask(Task task, int refreshRate = 500)
        {
            new TimedTask(refreshRate);
            Watch(task);
            Start();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Watch(Task task)
        {
            dispatcherTimer.Tick += new EventHandler(Event);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(RefreshRate);
            dispatcherTimer.Start();
            throw new NotImplementedException();
        }

        private void Event(object sender, EventArgs e)
        {

        }

        public int RefreshRate
        {
            get; set;
        }
    }
}
