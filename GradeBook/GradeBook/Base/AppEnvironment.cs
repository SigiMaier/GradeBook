// <copyright file="AppEnvironment.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Base
{
    using System;
    using System.IO;

    /// <summary>
    /// Class that contains all required Information regarding the Application.
    /// Implemented as Singleton with Lazy initialization as in "Http://csharpindepth.com/Articles/General/Singleton.aspx"
    /// </summary>
    public sealed class AppEnvironment
    {
        private static readonly Lazy<AppEnvironment> InstanceValue = new Lazy<AppEnvironment>(() => new AppEnvironment());

        private AppEnvironment()
        {
            this.ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            this.ExamRatingsFolder = this.ApplicationPath + "\\ExamRatings";

            this.CreateDirectories();
        }

        /// <summary>
        /// Gets the Instance of the AppEnvironment.
        /// </summary>
        public static AppEnvironment Instance => InstanceValue.Value;

        /// <summary>
        /// Gets the ApplicationPath of the Application.
        /// </summary>
        public string ApplicationPath { get; }

        /// <summary>
        /// Gets the ExamRatingsFolder of the Application.
        /// </summary>
        public string ExamRatingsFolder { get; }

        private void CreateDirectories()
        {
            if (!Directory.Exists(this.ExamRatingsFolder))
            {
                Directory.CreateDirectory(this.ExamRatingsFolder);
            }
        }
    }
}
