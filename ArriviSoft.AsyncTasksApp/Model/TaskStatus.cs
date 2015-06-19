using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArriviSoft.AsyncTasksApp.Model
{
    /// <summary>
    /// Represents the different status a slow task can has.
    /// </summary>
    public enum TaskStatus
    {
        /// <summary>
        /// Task has not ben started yet
        /// </summary>
        Idle,
        /// <summary>
        /// Task is running
        /// </summary>
        Running,
        /// <summary>
        /// Task execution is being canceled.
        /// </summary>
        Canceling,
        /// <summary>
        /// Task execution has finished because of a cancelation.
        /// </summary>
        Canceled,
        /// <summary>
        /// Task execution finised.
        /// </summary>
        Completed
    }
}
