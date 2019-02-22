// <copyright file="ProblemModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.MVVM.Model
{
    /// <summary>
    /// Model that contains information for a single Problem in the Exam.
    /// </summary>
    public class ProblemModel
    {
        /// <summary>
        /// Gets or sets the ProblemName.
        /// </summary>
        public string ProblemName { get; set; }

        /// <summary>
        /// Gets or sets the PointsForProblem.
        /// </summary>
        public int PointsForProblem { get; set; }
    }
}
