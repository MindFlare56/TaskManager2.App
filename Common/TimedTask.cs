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
            Init(refreshRate);
        }

        public TimedTask(Task task, int refreshRate = 500)
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

        private async void EventHandler(object sender, EventArgs eventArgs, Task task)
        {
            Console.WriteLine(DateTime.Now.Second);
            try {
                task.Start();
                Console.WriteLine("?");
            } catch(Exception execption) {
                await ShowMessageAsync("An error as occured", execption);
            }
        }

        private async Task ShowMessageAsync(string message, Exception execption)
        {
            throw new NotImplementedException();
        }

        public int RefreshRate
        {
            get; set;
        }
    }
}
