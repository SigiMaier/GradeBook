﻿// <copyright file="IMessageBoxService.cs" company="Sigi Maier">
// No copyright
// </copyright>

using System;

namespace Basics.MVVM
{
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

        void ShowErrorMessage(string message, Exception exception);

        string ShowInputMessage(string message);
    }
}
