// <copyright file="ExamRatingModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.MVVM.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Model for Serializing and Deserializing of the ExamRatings.
    /// </summary>
    public class ExamRatingModel
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
        /// Gets or sets the PointsPerProblems as in <see cref="ProblemModel"/>.
        /// </summary>
        public List<ProblemModel> PointsPerProblems { get; set; }

        /// <summary>
        /// Gets or sets the PointsPerGrade as in <see cref="GradeRatingModel"/>.
        /// </summary>
        public List<GradeRatingModel> PointsPerGrade { get; set; }
    }
}
