using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArriviSoft.AsyncTasksApp.Model
{
    /// <summary>
    /// Defines the contract of an asynchronous task.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Gets the name of the task.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Starts the task in asynchronous mode.
        /// </summary>
        Task<TaskResult> StartAsync(IProgress<double> progress, CancellationToken cancellationToken);
    }
}
