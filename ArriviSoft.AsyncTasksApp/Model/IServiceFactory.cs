using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArriviSoft.AsyncTasksApp.Model
{
    /// <summary>
    /// Represents the interface of a Task services provider.
    /// </summary>
    public interface IServiceFactory
    {
        /// <summary>
        /// Creates a new task instance.
        /// </summary>
        /// <returns>A task instance.</returns>
        ITaskService CreateTask();
    }
}
