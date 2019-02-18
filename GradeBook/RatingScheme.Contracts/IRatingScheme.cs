// <copyright file="IRatingScheme.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace RatingScheme.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for the Rating Scheme.
    /// Via this Interface it is possible to Set the Number of Problems, Set the Points per Problem and
    /// get the set Ratings.
    /// </summary>
    public interface IRatingScheme
    {
        /// <summary>
        /// Gets the Ratings for the Exam.
        /// </summary>
        RatingSchemeDTO Ratings { get; }

        /// <summary>
        /// Sets the Number Of Problems for the Exam.
        /// Throws a <see cref="RatingSchemeException"/> when the <paramref name="numberOfProblems"/> is smaller than 1
        /// </summary>
        /// <param name="numberOfProblems">The number of Problems in the Exam. Has to be greater than 1.</param>
        void SetNumberOfProblems(int numberOfProblems);

        /// <summary>
        /// Sets the Points Per Problem for the Exam.
        /// Throws a <see cref="RatingSchemeException"/> when <see cref="SetNumberOfProblems(int)"/> was not called
        /// beforehand in order to set the Number of Problems.
        /// </summary>
        /// <param name="pointsPerProblem">List containing the Points for each Problem. First element contains
        /// Points for first problem, second element for second problem and so on.</param>
        void SetPointsPerProblem(List<int> pointsPerProblem);

        Dictionary<double, double[]> GetPointsPerGrade();
    }
}
