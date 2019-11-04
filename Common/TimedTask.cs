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

        public TimedTask(int refreshRate = 1000, bool debug = false)
        {
            Init(refreshRate, debug);
        }

        public TimedTask(Action action, int refreshRate = 1000, bool debug = false)
        {
            Init(refreshRate, debug);
            Watch(action);
            Start();
        }

        private void Init(int refreshRate, bool debug)
        {
            RefreshRate = refreshRate;
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
            if (debug) {
                Console.WriteLine(action.Method.Name + " is running!");
            }            
            await Task.Run(action);
        }

        public int RefreshRate
        {
            get; set;
        }
    }
}
