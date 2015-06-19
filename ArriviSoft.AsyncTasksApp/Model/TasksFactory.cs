using System;

namespace ArriviSoft.AsyncTasksApp.Model
{

    /// <summary>
    /// Represents a factory that creates slow tasks.
    /// </summary>
    public class TasksFactory : IServiceFactory
    {
        /// <summary>
        /// Creates a new task instance.
        /// </summary>
        /// <returns>
        /// A task instance.
        /// </returns>
        public ITaskService CreateTask()
        {
            throw new NotImplementedException();
        }
    }
}