// <copyright file="Program.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook
{
    using System;
    using System.Windows;
    using GradeBook.Wpf;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new Application();
            app.Run(new MainWindow());
        }
    }
}
