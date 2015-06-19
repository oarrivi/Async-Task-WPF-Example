using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArriviSoft.AsyncTasksApp.Model
{
    /// <summary>
    /// Provides data to the task finishing event.
    /// </summary>
    public class AsyncTaskFinishedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncTaskFinishedEventArgs"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="cancelled">if set to <c>true</c> [cancelled].</param>
        /// <param name="result">The result.</param>
        /// <param name="userState">State of the user.</param>
        public AsyncTaskFinishedEventArgs(Exception error, bool cancelled, TimeSpan result, object userState) : base (error, cancelled, userState)
        {
            this.Result = result;
        }

        public TimeSpan Result { get; private set; }
    }

    /// <summary>
    /// Represents the finish event of an asynchronous task execution.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="AsyncTaskFinishedEventArgs"/> instance containing the event data.</param>
    public delegate void AsyncTaskFinishedEventHandler(object sender, AsyncTaskFinishedEventArgs args);

    /// <summary>
    /// Provides data to the Task Progress Changed event.
    /// </summary>
    public class TaskProgressChangedEventArgs : EventArgs
    {
        public TaskProgressChangedEventArgs(double progress)
        {
            this.Progress = progress;
        }

        public double Progress { get; private set; }
    }

    /// <summary>
    /// Event raised when executing task changes its progress or status.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="TaskProgressChangedEventArgs"/> instance containing the event data.</param>
    public delegate void TaskProgressChangedEventHander(object sender, TaskProgressChangedEventArgs args);
}
