using GalaSoft.MvvmLight;
using ArriviSoft.AsyncTasksApp.Model;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using GalaSoft.MvvmLight.CommandWpf;

namespace ArriviSoft.AsyncTasksApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IServiceFactory serviceFactory;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IServiceFactory service)
        {
            this.WorkItems = new ObservableCollection<WorkItemViewModel>();

            serviceFactory = service;
         
            this.InitializeCommands();

            // Sample work items
            if (this.IsInDesignMode)
            {
                this.WorkItems.Add(new WorkItemViewModel(null)
                    {
                        Title = "Work Item 001",
                        Progress = 0.8,
                    });

                this.WorkItems.Add(new WorkItemViewModel(null)
                {
                    Title = "Work Item 002",
                    Progress = 0.4,
                });
            }
        }

        private void InitializeCommands()
        {
            var cmds = new List<CommandViewModel>();
            cmds.Add(new CommandViewModel()
            {
                Text = "Launch",
                Hint = "Launches a new slot task in asynchronous mode",
                Command = this.LaunchTaskCommand
            });

            this.Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
        }

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

        #region Property WorkItems

        /// <summary>
        /// The <see cref="WorkItems" /> property's name.
        /// </summary>
        public const string WorkItemsPropertyName = "WorkItems";

        private ObservableCollection<WorkItemViewModel> workItems = null;

        /// <summary>
        /// Sets and gets the WorkItems property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<WorkItemViewModel> WorkItems
        {
            get
            {
                return workItems;
            }
            set
            {
                if (workItems == value)
                {
                    return;
                }
                workItems = value;
                RaisePropertyChanged(WorkItemsPropertyName);
            }
        }

        #endregion

        #region Command LaunchTaskCommand

        private RelayCommand launchTaskCommand;

        /// <summary>
        /// Gets the LaunchTaskCommand.
        /// </summary>
        public RelayCommand LaunchTaskCommand
        {
            get
            {
                return launchTaskCommand
                    ?? (launchTaskCommand = new RelayCommand(
                    () =>
                    {
                        if (!LaunchTaskCommand.CanExecute(null))
                        {
                            return;
                        }
                        this.ExecuteLaunchTaskCommand();
                    },
                    () => true));
            }
        }


        private void ExecuteLaunchTaskCommand()
        {
            WorkItemViewModel vm = new WorkItemViewModel(this.serviceFactory.CreateTask());
            this.WorkItems.Add(vm);
        }

        #endregion

        public override void Cleanup()
        {
            // Clean up if needed

            base.Cleanup();
        }
    }
}