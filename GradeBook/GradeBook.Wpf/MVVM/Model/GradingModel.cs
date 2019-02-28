using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.Wpf.MVVM.Model
{
    public class GradingModel
    {
        public string MatriculationNumber { get; set; }

        public ObservableCollection<PointsPerProblemItem> PointsPerProblems { get; set; }

        public double Grade { get; set; }

        public double TotalScore { get; set; }
    }

    public class PointsPerProblemItem
    {
        public double DoubleValue { get; set; }
    }
}
