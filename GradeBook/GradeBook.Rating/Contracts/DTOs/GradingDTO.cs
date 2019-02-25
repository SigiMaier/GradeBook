using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.Rating.Contracts
{
    public class GradingDTO
    {
        /// <summary>
        /// Gets or sets the Points per Problem that the Student reached. First Element is for the first Problem,
        /// second Element for the second Problem and so on.
        /// The Key contains the Matriculation Number and the Values contain the Points.
        /// </summary>
        public Dictionary<int, List<double>> StudentPointsPerProblem { get; set; }

        /// <summary>
        /// Gets or sets the Students Grade. The key contains the Matriculation Number and Values consists of an Array
        /// with 2 Elements. The first Element contains the Grade and the second Element contains the total score of the
        /// student.
        /// </summary>
        public Dictionary<int, double[]> StudentsGrade { get; set; }
    }
}
