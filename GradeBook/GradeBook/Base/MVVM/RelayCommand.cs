// <copyright file="RelayCommand.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Base.MVVM
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Base-Class for executing Commands from the ViewModel.
    /// When creating a Command in the ViewModel the Constructor takes two parameters
    /// The first one defines, what to execute and the second defines when to execute
    /// the action.
    /// </summary>
    public class RelayCommand : ICommand
    {
        // What
        private readonly Action<object> executeHandler;

        // When
        private readonly Func<object, bool> canExecuteHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        /// <param name="execute">Defines WHAT to execute</param>
        /// the action.<param name="canExecute">Defines WHEN the WHAT</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.executeHandler = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecuteHandler = canExecute;
        }

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            if (this.canExecuteHandler == null)
            {
                return true;
            }

            return this.canExecuteHandler(parameter);
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            this.executeHandler(parameter);
        }
    }
}
