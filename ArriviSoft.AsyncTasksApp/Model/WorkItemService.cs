using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArriviSoft.AsyncTasksApp.Model
{
    /// <summary>
    /// Represents a slow task that does nothing.
    /// </summary>
    public class WorkItemService : ITaskService
    {

        public WorkItemService(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }


        /// <summary>
        /// Starts the task in asynchronous mode.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TaskResult> StartAsync(IProgress<double> progress, CancellationToken cancellationToken)
        {

            var task = Task.Run<TimeSpan>(async () =>
                {
                    Random rand = new Random();
                    int delay = Convert.ToInt32(rand.NextDouble() * 150);

                    Stopwatch sw = Stopwatch.StartNew();

                    for (int i = 0; i <= 100; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Delay(delay);
                        progress.Report(Convert.ToDouble(i) / 100.0);
                    }

                    sw.Stop();

                    return sw.Elapsed;
                });

            // Await here until task finishes...
            TimeSpan ts = await task;

            return new TaskResult(ts);
        }
    }
}
