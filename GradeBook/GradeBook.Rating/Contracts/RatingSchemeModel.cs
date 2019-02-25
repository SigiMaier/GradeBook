// <copyright file="RatingSchemeModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Rating.Contracts
{
    using System.Collections.Generic;
    using GradeBook.Rating;

    /// <summary>
    /// Class that handles the RatingSchemeModel.
    /// </summary>
    public class RatingSchemeModel
    {
        private readonly IRatingScheme ratingScheme;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingSchemeModel"/> class.
        /// </summary>
        /// <param name="numberOfProblems">The number of Problems in the Exam.</param>
        /// <param name="pointsPerProblem">The Points per Problem.
        /// The First Element contains the Points for the Problem and so on.</param>
        public RatingSchemeModel(int numberOfProblems, List<int> pointsPerProblem)
        {
            this.ratingScheme = new RatingScheme(numberOfProblems, pointsPerProblem);
            this.Rating = this.ratingScheme.Ratings;
        }

        /// <summary>
        /// Gets or sets the Rating.
        /// </summary>
        public RatingSchemeDTO Rating { get; set; }
    }
}
