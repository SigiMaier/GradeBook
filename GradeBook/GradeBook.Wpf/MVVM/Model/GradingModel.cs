using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.Wpf.MVVM.Model
{
    public class GradingModel
    {
        public string MatriculationNumber { get; set; }

        public List<double> PointsPerProblems { get; set; }

        public double Grade { get; set; }

        public double TotalScore { get; set; }
    }
}
