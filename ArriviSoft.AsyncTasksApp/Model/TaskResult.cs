using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArriviSoft.AsyncTasksApp.Model
{
    /// <summary>
    /// Describes the result of an execution task.
    /// </summary>
    public class TaskResult
    {

        /// <summary>
        /// Creates a new instance of TaskResult
        /// </summary>
        /// <param name="time">A time duration value.</param>
        public TaskResult(System.TimeSpan time)
        {

        }

        /// <summary>
        /// Gets the time taken to finish the task.
        /// </summary>
        public System.TimeSpan Time { get; private set; }
    }
}
