using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace Common
{
    public class UITimedTask
    {

        private DispatcherTimer dispatcherTimer;
        private bool debug;

        public UITimedTask(int refreshRate = 500)
        {
            Init(refreshRate);
        }

        public UITimedTask(Action action, int refreshRate = 500)
        {
            Init(refreshRate);
            Watch(action);
            Start();
        }

        private void Init(int refreshRate, bool debug = false)
        {
            RefreshRate = refreshRate;
            this.debug = debug;
            dispatcherTimer = new DispatcherTimer();
        }

        public void Start()
        {            
            dispatcherTimer.Start();             
        }

        public void Watch(Action action, bool callOnStart = true)
        {
            OnCallOnStart(callOnStart, action);
            dispatcherTimer.Tick += new EventHandler((sender, eventArgs) => EventHandler(sender, eventArgs, action));
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(RefreshRate);            
        }

        private void OnCallOnStart(bool callOnStart, Action action)
        {
            if (callOnStart) {
                var firstEvent = new ElapsedEventHandler(new EventHandler((sender, eventArgs) => EventHandler(sender, eventArgs, action)));
                firstEvent(null, null);
            }
        }

        private async void EventHandler(object sender, EventArgs eventArgs, Action action)
        {
            //Console.WriteLine(action.Method.Name);            
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
