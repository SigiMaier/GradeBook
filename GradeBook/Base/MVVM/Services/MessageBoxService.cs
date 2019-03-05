// <copyright file="MessageBoxService.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.MVVM
{
    using System;
    using System.Windows;

    /// <summary>
    /// The MessageBoxService to show MessageBoxes from the ViewModel.
    /// </summary>
    public class MessageBoxService : IMessageBoxService
    {
        /// <inheritdoc/>
        public void ShowInfoMessage(string message, string caption)
        {
           MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <inheritdoc/>
        public void ShowErrorMessage(string message, Exception exception)
        {
            MessageBox.Show(message + "\n\rException:\n\r" + exception, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
