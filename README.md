An example of WPF desktop application using asynchronous tasks with MVVM
=================================

The purpose of this repository is to have an example of source code with a Microsoft .NET WPF project using the best practices: 
- MVVM pattern with MVVM Light assemblies.
- IoC for dependency injections.
- Async/await technics for asynchronous tasks.

**The example**  
It's a small desktop application which launches slow tasks by pressing a button. These slow tasks don't do anything but delay for an random amount of time. They support cancellations by users and report progress.

**Pending of implementation**  
The sample application is complete although as usual it could be improved with:
- Show a result when tasks complete.
- Cancel all tasks with a button or when the application is being closed.
