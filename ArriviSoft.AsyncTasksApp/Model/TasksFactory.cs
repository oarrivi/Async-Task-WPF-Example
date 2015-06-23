using System;

namespace ArriviSoft.AsyncTasksApp.Model
{

    /// <summary>
    /// Represents a factory that creates slow tasks.
    /// </summary>
    public class TasksFactory : IServiceFactory
    {
        private int taskCount = 1;

        /// <summary>
        /// Creates a new task instance.
        /// </summary>
        /// <returns>
        /// A task instance.
        /// </returns>
        public ITaskService CreateTask()
        {
            string name = BuildName();
            return this.CreateTask(name);
        }

        /// <summary>
        /// Creates the task.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ITaskService CreateTask(string name)
        {
            return new WorkItemService(name);
        }

        private string BuildName()
        {
            lock(this)
            {
                return string.Format("Work ID #{0:D3}", taskCount++);
            }
        }
    }
}