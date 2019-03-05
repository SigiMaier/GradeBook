// <copyright file="IMessageBoxService.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.MVVM
{
    using System;

    /// <summary>
    /// Interface for Message Service in order to show MessageBoxes from ViewModels
    /// </summary>
    public interface IMessageBoxService
    {
        /// <summary>
        /// Shows an Info Message.
        /// </summary>
        /// <param name="message">The Message to display.</param>
        /// <param name="caption">The Caption of the MessageBox.</param>
        void ShowInfoMessage(string message, string caption);

        /// <summary>
        /// Shows an Error Message.
        /// </summary>
        /// <param name="message">The Message to display.</param>
        /// <param name="exception">The Exception to display.</param>
        void ShowErrorMessage(string message, Exception exception);
    }
}
