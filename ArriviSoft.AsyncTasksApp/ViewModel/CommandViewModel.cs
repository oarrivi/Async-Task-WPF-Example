using GalaSoft.MvvmLight;
using System.Windows.Input;

namespace ArriviSoft.AsyncTasksApp.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CommandViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the CommandViewModel class.
        /// </summary>
        public CommandViewModel()
        {
        }

        #region Property Command

        /// <summary>
        /// The <see cref="Command" /> property's name.
        /// </summary>
        public const string CommandPropertyName = "Command";

        private ICommand command = null;

        /// <summary>
        /// Sets and gets the Command property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ICommand Command
        {
            get
            {
                return command;
            }
            set
            {
                if (command == value)
                {
                    return;
                }
                command = value;
                RaisePropertyChanged(CommandPropertyName);
            }
        }

        #endregion

        #region Property Text

        /// <summary>
        /// The <see cref="Text" /> property's name.
        /// </summary>
        public const string TextPropertyName = "Text";

        private string text = null;

        /// <summary>
        /// Sets and gets the Text property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text == value)
                {
                    return;
                }
                text = value;
                RaisePropertyChanged(TextPropertyName);
            }
        }

        #endregion

        #region Property Hint

        /// <summary>
        /// The <see cref="Hint" /> property's name.
        /// </summary>
        public const string HintPropertyName = "Hint";

        private string hint = null;

        /// <summary>
        /// Sets and gets the Hint property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Hint
        {
            get
            {
                return hint;
            }
            set
            {
                if (hint == value)
                {
                    return;
                }
                hint = value;
                RaisePropertyChanged(HintPropertyName);
            }
        }

        #endregion

    }
}