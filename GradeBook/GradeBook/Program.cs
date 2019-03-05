// <copyright file="Program.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook
{
    using System;
    using System.Windows;
    using GradeBook.Wpf;

    /// <summary>
    /// The Program Class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The Entrance of the Application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new MainWindow());
        }
    }
}
