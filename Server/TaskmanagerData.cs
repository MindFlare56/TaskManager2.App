using Common;

namespace Server
{    
    public class TaskmanagerData
    {
        public TaskmanagerData()
        {            
            
        }        

        public void Start()
        {
            Init();
        }

        private void Init()
        {            
            var timedTask = new TimedTask(1000);
            //timedTask.Watch();
            timedTask.Start();
            while (true);
        }        
    }
}
