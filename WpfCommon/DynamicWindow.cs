using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace WpfLibrary
{
    public class DynamicWindow
    {

        private int millisecondsToLive;
        private Func<bool> condition;        
        private Thread dynamicWindow;
        private static Action onEnd;
        private static System.Windows.Application context;
        private static UIElement content;
        private static Window window;
        private static WrapPanel wrapPanel;

        public DynamicWindow(UIElement content, int millisecondsToLive = -1, Func<bool> condition = null, Action onEnd = null)
        {
            DynamicWindow.content = content;
            this.millisecondsToLive = millisecondsToLive;
            this.condition = condition;
            DynamicWindow.onEnd = onEnd;
        }

        public DynamicWindow(string message)
        {
            System.Windows.MessageBox.Show(message);
        }      

        public void Show(System.Windows.Application context)
        {            
            DynamicWindow.context = context;            
            dynamicWindow = new Thread(ThreadProc);
            dynamicWindow.SetApartmentState(ApartmentState.STA);
            dynamicWindow.Start();             
            ExecuteTask();            
        }

        private void ExecuteTask() //todo refactor to Common since it could get uselful
        {
            //make this a thread
            if (millisecondsToLive != -1) WatchTimeElapsed();
            if (condition != null) WatchCondition();
        }

        private void WatchTimeElapsed()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.ElapsedMilliseconds < millisecondsToLive);
            Close();
        }

        private void WatchCondition()
        {
            while (!condition());
            Close();
        }

        public void Close()
        {
            onEnd?.Invoke();                        
            window.Close();
            content = null;
            for (int i = 0; i < wrapPanel.Children.Count; ++i) {
                wrapPanel.Children.RemoveAt(i);
            }            
            dynamicWindow.Abort();            
        }

        private static void ThreadProc()
        {
            context.Dispatcher.Invoke(() => {
                window = new Window { Owner = context.MainWindow };
                window.Title = "Connections list...";
                window.Height = 300;
                window.Width = 300;
                wrapPanel = new WrapPanel();                
                wrapPanel.Children.Add(content);
                window.Content = wrapPanel;
                window.Show();
            });                        
        }
    }
}
