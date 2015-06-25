using ArriviSoft.AsyncTasksApp.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace ArriviSoft.AsyncTasksApp.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WorkItemViewModel : ViewModelBase
    {
        private ITaskService workItem = null;
        private CancellationTokenSource cancellationTokenSource = null;

        /// <summary>
        /// Initializes a new instance of the WorkItemViewModel class.
        /// </summary>
        public WorkItemViewModel(ITaskService workItem)
        {
            this.workItem = workItem;
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkItemViewModel"/> class.
        /// </summary>
        public WorkItemViewModel() : this(ServiceLocator.Current.GetInstance<IServiceFactory>().CreateTask())
        {
        }

        #region Property Title

        /// <summary>
        /// The <see cref="Title" /> property's name.
        /// </summary>
        public const string TitlePropertyName = "Title";

        private string title = null;

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (title == value)
                {
                    return;
                }
                title = value;
                RaisePropertyChanged(TitlePropertyName);
            }
        }

        #endregion

        #region Property Progress

        /// <summary>
        /// The <see cref="Progress" /> property's name.
        /// </summary>
        public const string ProgressPropertyName = "Progress";

        private double progress = 0;

        /// <summary>
        /// Sets and gets the Progress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Progress
        {
            get
            {
                return progress;
            }
            set
            {
                if (progress == value)
                {
                    return;
                }
                progress = value;
                RaisePropertyChanged(ProgressPropertyName);
            }
        }

        #endregion

        #region Property IsRunning

        /// <summary>
        /// The <see cref="IsRunning" /> property's name.
        /// </summary>
        public const string IsRunningPropertyName = "IsRunning";

        private bool isRunning = false;

        /// <summary>
        /// Sets and gets the IsRunning property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if (isRunning == value)
                {
                    return;
                }
                isRunning = value;
                RaisePropertyChanged(IsRunningPropertyName);
                RaiseCommandsCanExecuteChanged();
            }
        }

        #endregion

        #region Property IsCancelled

        /// <summary>
        /// The <see cref="IsCancelled" /> property's name.
        /// </summary>
        public const string IsCancelledPropertyName = "IsCancelled";

        private bool isCancelled = false;

        /// <summary>
        /// Sets and gets the IsCancelled property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsCancelled
        {
            get
            {
                return isCancelled;
            }
            set
            {
                if (isCancelled == value)
                {
                    return;
                }
                isCancelled = value;
                RaisePropertyChanged(IsCancelledPropertyName);
                RaiseCommandsCanExecuteChanged();
            }
        }

        #endregion

        #region Property Outcome

        /// <summary>
        /// The <see cref="Outcome" /> property's name.
        /// </summary>
        public const string OutcomePropertyName = "Outcome";

        private TimeSpan outcome = TimeSpan.Zero;

        /// <summary>
        /// Sets and gets the Outcome property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TimeSpan Outcome
        {
            get
            {
                return outcome;
            }
            set
            {
                if (outcome == value)
                {
                    return;
                }
                outcome = value;
                RaisePropertyChanged(OutcomePropertyName);
            }
        }

        #endregion

        #region Property Commands

        /// <summary>
        /// The <see cref="Commands" /> property's name.
        /// </summary>
        public const string CommandsPropertyName = "Commands";

        private ReadOnlyCollection<CommandViewModel> commands = null;

        /// <summary>
        /// Sets and gets the Commands property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                return commands;
            }
            set
            {
                if (commands == value)
                {
                    return;
                }
                commands = value;
                RaisePropertyChanged(CommandsPropertyName);
            }
        }

        #endregion

        #region Command CancelCommand

        private RelayCommand cancelCommand;

        /// <summary>
        /// Gets the CancelCommand.
        /// </summary>
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand
                    ?? (cancelCommand = new RelayCommand(
                    () =>
                    {
                        if (!CancelCommand.CanExecute(null))
                        {
                            return;
                        }
                        if (this.cancellationTokenSource != null)
                        {
                            this.cancellationTokenSource.Cancel();
                        }
                    },
                    () => this.IsRunning && !this.IsCancelled));
            }
        }

        #endregion

        #region Command StartCommand

        private RelayCommand startCommand;

        /// <summary>
        /// Gets the StartCommand.
        /// </summary>
        public RelayCommand StartCommand
        {
            get
            {
                return startCommand
                    ?? (startCommand = new RelayCommand(
                    async () =>
                    {
                        if (!StartCommand.CanExecute(null))
                        {
                            return;
                        }
                        await ExecuteStartAsync();
                    },
                    () => !this.IsRunning));
            }
        }

        private async Task ExecuteStartAsync()
        {
            this.IsRunning = true;
            this.IsCancelled = false;
            this.Outcome = TimeSpan.Zero;

            var progressIndicator = new Progress<double>(progress => this.Progress = progress);
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                var result = await this.workItem.StartAsync(progressIndicator, cancellationTokenSource.Token);
                this.Outcome = result.Time;
            }
            catch (OperationCanceledException)
            {
                this.IsCancelled = true;
            }
            
            this.IsRunning = false;
        }

        #endregion

        private void Initialize()
        {
            if (this.IsInDesignMode)
            {
                this.Title = "Task title";
                this.Progress = 0.3;
            }
            else
            {
                this.Title = this.workItem.Name;
                this.Progress = 0;
            }
            this.InitializeCommands();
        }

        private void InitializeCommands()
        {
            List<CommandViewModel> cmds = new List<CommandViewModel>();
            cmds.Add(new CommandViewModel()
            {
                Text = "Restart",
                Hint = "Restart the execution of the task.",
                Command = this.StartCommand
            });
            cmds.Add(new CommandViewModel()
            {
                Text = "Cancel",
                Hint = "Stops the execution of the task.",
                Command = this.CancelCommand
            });

            this.Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
        }

        private void RaiseCommandsCanExecuteChanged()
        {
            this.StartCommand.RaiseCanExecuteChanged();
            this.CancelCommand.RaiseCanExecuteChanged();
        }

    }
}