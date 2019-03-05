// <copyright file="ProblemDTO.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Rating.Contracts
{
    /// <summary>
    /// Model that contains information for a single Problem in the Exam.
    /// </summary>
    public class ProblemDTO
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
