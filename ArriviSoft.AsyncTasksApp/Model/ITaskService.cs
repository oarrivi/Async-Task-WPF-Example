using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArriviSoft.AsyncTasksApp.Model
{
    /// <summary>
    /// Defines the contract of an asynchronous task.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Occurs when [status changed].
        /// </summary>
        event TaskProgressChangedEventHander StatusChanged;

        /// <summary>
        /// Occurs when [task finished].
        /// </summary>
        event AsyncTaskFinishedEventHandler TaskFinished;

        /// <summary>
        /// Gets the name of the task.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the task.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the progress of the task. This values range is from 0 to 1.
        /// </summary>
        double Progress { get; }

        /// <summary>
        /// Gets the result of the execution: The time taken to execute the slow task.
        /// </summary>
        /// <value>
        /// The time elapsed.
        /// </value>
        TimeSpan Result { get; }

        /// <summary>
        /// Request the cancelation of the task.
        /// </summary>
        void CancelAsync();

        /// <summary>
        /// Starts the task in asynchronous mode.
        /// </summary>
        /// <param name="userData">The user data.</param>
        Task<TaskResult> StartAsync(object userData);
    }
}
