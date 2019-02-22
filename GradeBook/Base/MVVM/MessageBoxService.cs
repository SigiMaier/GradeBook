// <copyright file="MessageBoxService.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.MVVM
{
    using System.Windows;

    /// <summary>
    /// The MessageBoxService to show MessageBoxes from the ViewModel.
    /// </summary>
    public class MessageBoxService : IMessageBoxService
    {
        /// <summary>
        /// Shows an Info MessageBox.
        /// </summary>
        /// <param name="message">The Message to display.</param>
        /// <param name="caption">The caption of the Message Box.</param>
        public void ShowInfoMessage(string message, string caption)
        {
           MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
