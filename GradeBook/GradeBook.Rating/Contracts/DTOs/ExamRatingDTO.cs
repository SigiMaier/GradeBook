// <copyright file="ExamRatingDTO.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Rating.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Model for Serializing and Deserializing of the ExamRatings.
    /// </summary>
    public class ExamRatingDTO
    {
        /// <summary>
        /// Gets or sets the ExamName.
        /// </summary>
        public string ExamName { get; set; }

        /// <summary>
        /// Gets or sets the NumberOfProblems.
        /// </summary>
        public int NumberOfProblems { get; set; }

        /// <summary>
        /// Gets or sets the TotalPoints.
        /// </summary>
        public double TotalPoints { get; set; }

        /// <summary>
        /// Gets or sets the PointsPerProblems as in <see cref="ProblemDTO"/>.
        /// </summary>
        public List<ProblemDTO> PointsPerProblems { get; set; }

        /// <summary>
        /// Gets or sets the PointsPerGrade as in <see cref="GradeRatingDTO"/>.
        /// </summary>
        public List<GradeRatingDTO> PointsPerGrade { get; set; }
    }
}
