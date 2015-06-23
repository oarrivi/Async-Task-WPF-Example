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
    public class WorkItemService : ITaskService
    {
        private object userData = null;

        public event TaskProgressChangedEventHander StatusChanged;
        public event AsyncTaskFinishedEventHandler TaskFinished;

        public WorkItemService(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
        
        public double Progress { get; private set; }

        public TimeSpan Result { get; private set; }

        public void CancelAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TaskResult> StartAsync(IProgress<TaskProgressChangedEventArgs> progress)
        {

            var task = Task.Run<TimeSpan>(async () =>
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    for (int i = 0; i <= 100; i++)
                    {
                        await Task.Delay(100);
                        progress.Report(new TaskProgressChangedEventArgs(Convert.ToDouble(i) / 100.0));
                    }


                    sw.Stop();

                    return sw.Elapsed;
                });

            // Await here until task finishes...
            TimeSpan ts = await task;

            var result = new TaskResult(ts);
            
            // Note: It isn't necessary to raise events

            return result;
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
