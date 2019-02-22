using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.Base
{
    public sealed class AppEnvironment
    {
        private static readonly Lazy<AppEnvironment> instance = new Lazy<AppEnvironment>(() => new AppEnvironment());

        private AppEnvironment()
        {
            this.ApplicationPath = AppDomain.CurrentDomain.BaseDirectory;
            this.ExamRatingsFolder = this.ApplicationPath + "\\ExamRatings";

            this.CreateDirectories();
        }

        

        public static AppEnvironment Instance => instance.Value;

        public string ApplicationPath { get; }
        
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
