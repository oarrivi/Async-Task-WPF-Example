using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArriviSoft.AsyncTasksApp.Model
{
    /// <summary>
    /// Represents a slow task that does nothing.
    /// </summary>
    public class AsyncSlowTask : ITaskService
    {
        private object userData = null;

        public event TaskProgressChangedEventHander StatusChanged;
        public event AsyncTaskFinishedEventHandler TaskFinished;

        public AsyncSlowTask(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public double Progress { get; private set; }

        public TimeSpan Result { get; private set; }

        public void CancelAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TimeSpan> StartAsync(object userData)
        {
            this.userData = userData;

            var task = Task.Run<TimeSpan>(() =>
                {
                    Stopwatch sw = Stopwatch.StartNew();





                    sw.Stop();
                    return sw.Elapsed;
                });

            // Await here until task finishes...

            this.Result = await task;
            
            // Note: It isn't necessary to raise events

            return this.Result;
        }

        private void OnTaskFinished(Exception error, bool cancelled, TimeSpan result)
        {
            var handler = this.TaskFinished;
            if (handler != null)
            {
                handler(this, new AsyncTaskFinishedEventArgs(error, cancelled, result, this.userData));
            }
        }

        private void OnStatusChanged(double progress)
        {
            var handler = this.StatusChanged;
            if (handler != null)
            {
                handler(this, new TaskProgressChangedEventArgs(progress));
            }
        }

    }
}
