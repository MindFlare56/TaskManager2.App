using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Common
{
    public class TimedTask
    {

        private Timer timer;
        private bool debug;
        private bool wait;

        public TimedTask(int refreshRate = 1000, bool wait = true, bool debug = false)
        {
            Init(refreshRate, wait, debug);
        }

        public TimedTask(Action action, int refreshRate = 1000, bool wait = true, bool debug = false)
        {
            Init(refreshRate, wait, debug);            
            Watch(action);
            Start();
        }

        private void Init(int refreshRate, bool wait, bool debug)
        {
            RefreshRate = refreshRate;
            this.wait = wait;
            this.debug = debug;
            timer = new Timer();
        }

        public void Start()
        {
            timer.Start();            
        }

        public void Watch(Action action, bool callOnStart = true)
        {
            OnCallOnStart(callOnStart, action);
            timer.Elapsed += new ElapsedEventHandler(new EventHandler((sender, eventArgs) => EventHandler(sender, eventArgs, action)));
            timer.Interval = RefreshRate;
        }

        private void OnCallOnStart(bool callOnStart, Action action)
        {
            if (callOnStart) {
                var firstEvent = new ElapsedEventHandler(new EventHandler((sender, eventArgs) => EventHandler(sender, eventArgs, action)));
                firstEvent(null, null);
            }
        }

        private async void EventHandler(object sender, EventArgs eventArgs, Action action) {
            //Console.WriteLine(action.Method.Name);            
            if (debug) {
                Console.WriteLine(action.Method.Name + " is running!");
            }
            var task = Task.Run(action);
            if (wait) {
                task.Wait();
            }            
            await task;
        }

        public int RefreshRate
        {
            get; set;
        }
    }
}
