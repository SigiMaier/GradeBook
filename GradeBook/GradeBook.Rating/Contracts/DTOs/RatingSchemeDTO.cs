// <copyright file="RatingSchemeDTO.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Rating.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// DTO that contains all required RatingScheme data.
    /// </summary>
    public class RatingSchemeDTO
    {
        /// <summary>
        /// Gets or sets the Number Of Problems for the Exam.
        /// </summary>
        public int NumberOfProblems { get; set; }

        /// <summary>
        /// Gets or sets the Points Per Problem for the Exam.
        /// </summary>
        public Dictionary<int, int> PointsPerProblem { get; set; }

        /// <summary>
        /// Gets or sets the Maximum Points of the Exam.
        /// </summary>
        public int MaximumPoints { get; set; }

        public Dictionary<double, double[]> PointsPerGrade { get; set; }
    }
}
