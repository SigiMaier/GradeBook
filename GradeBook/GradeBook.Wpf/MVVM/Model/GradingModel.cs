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

    /// <summary>
    /// Wrapper Class for double Values to be able to bind the PointsPerProblems via the ViewModel to the View.
    /// <see href="https://stackoverflow.com/questions/5972990/wpf-how-to-make-datagrid-binding-with-dynamic-columns-editable"/>
    /// answer EDIT 2
    /// </summary>
    public class PointsPerProblemItem
    {
        private double pointsPerProblemValue;

        public event EventHandler PointsPerProblemValueChanged;

        public double PointsPerProblemValue
        {
            get
            {
                return this.pointsPerProblemValue;
            }

            set
            {
                this.pointsPerProblemValue = value;

                EventHandler handler = this.PointsPerProblemValueChanged;

                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }
    }
}
