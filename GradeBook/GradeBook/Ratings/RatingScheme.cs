// <copyright file="RatingScheme.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Ratings
{
    using System.Collections.Generic;
    using System.Linq;
    using GradeBook.Ratings.Contracts;

    /// <summary>
    /// Class for Setting and Getting Values regarding the Rating Scheme for the Exam,
    /// implements <see cref="IRatingScheme"/>
    /// </summary>
    public class RatingScheme : IRatingScheme
    {
        private readonly Dictionary<double, double> percentPerGrade;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingScheme"/> class.
        /// Throws RatingSchemeExceptions when the numberOfProblems == 0 or pointsPerProblem.Count != numberOfProblems.
        /// </summary>
        /// <param name="numberOfProblems">The number of Problems in the Exam.</param>
        /// <param name="pointsPerProblem">The points per Problem for the Exam</param>
        public RatingScheme(int numberOfProblems, List<int> pointsPerProblem)
        {
            this.percentPerGrade = new Dictionary<double, double>
            {
                { 39.5, 5.0 },
                { 49.5, 4.0 },
                { 55.0, 3.7 },
                { 60.0, 3.3 },
                { 65.0, 3.0 },
                { 70.0, 2.7 },
                { 75.0, 2.3 },
                { 80.0, 2.0 },
                { 85.0, 1.7 },
                { 90.0, 1.3 }
            };

            this.Ratings = new RatingSchemeDTO()
            {
                MaximumPoints = 0,
                NumberOfProblems = 0,
                PointsPerProblem = new Dictionary<int, int>(),
                PointsPerGrade = this.InitPointsPerGrade()
            };

            if (numberOfProblems == 0)
            {
                throw new RatingSchemeException($"The value of {nameof(numberOfProblems)} has to be > 0."
                    + $" Set {nameof(numberOfProblems)} to a correct value.");
            }

            this.Ratings.NumberOfProblems = numberOfProblems;

            if (pointsPerProblem.Count != numberOfProblems)
            {
                throw new RatingSchemeException($"The Count of {nameof(pointsPerProblem)} does not match the "
                    + $"{nameof(numberOfProblems)}. Check both Values.");
            }

            this.SetPointsPerProblem(pointsPerProblem);
            this.SetPointsPerGrade();
        }

        /// <inheritdoc/>
        public RatingSchemeDTO Ratings { get; }

        private void SetPointsPerProblem(List<int> pointsPerProblem)
        {
            for (int i = 0; i < this.Ratings.NumberOfProblems; i++)
            {
                this.Ratings.PointsPerProblem.Add(i + 1, pointsPerProblem[i]);
            }

            this.SetMaximumPoints();
        }

        private void SetPointsPerGrade()
        {
            this.InitFirstElementOfPointsPerGrade();
            this.CalculatePointsPerGrade();
        }

        private void SetMaximumPoints()
        {
            if (this.Ratings.PointsPerProblem.Count > 0)
            {
                this.Ratings.MaximumPoints = this.Ratings.PointsPerProblem.Sum(n => n.Value);
            }
        }

        private Dictionary<double, double[]> InitPointsPerGrade()
        {
            Dictionary<double, double[]> pointsPerGrade = new Dictionary<double, double[]>();

            foreach (var item in this.percentPerGrade)
            {
                pointsPerGrade.Add(item.Value, new double[2]);
            }

            pointsPerGrade.Add(1.0, new double[2]);

            return pointsPerGrade;
        }

        private void InitFirstElementOfPointsPerGrade()
        {
            this.Ratings.PointsPerGrade.ElementAt(0).Value[0] = 0.0;
            this.Ratings.PointsPerGrade.ElementAt(0).Value[1] = this.CalculateUpperPointsBoundaryForGrade(0);
        }

        private double CalculateUpperPointsBoundaryForGrade(int index)
        {
            return this.Ratings.MaximumPoints * this.percentPerGrade.ElementAt(index).Key / 100.0;
        }

        private void CalculatePointsPerGrade()
        {
            for (int i = 1; i < this.Ratings.PointsPerGrade.Count - 1; i++)
            {
                this.Ratings.PointsPerGrade.ElementAt(i).Value[0] = this.Ratings.PointsPerGrade.ElementAt(i - 1).Value[1] + 0.5;
                this.Ratings.PointsPerGrade.ElementAt(i).Value[1] =
                    this.CalculateUpperPointsBoundaryForGrade(i);
            }

            this.Ratings.PointsPerGrade.ElementAt(this.Ratings.PointsPerGrade.Count - 1).Value[0]
                = this.Ratings.PointsPerGrade.ElementAt(this.Ratings.PointsPerGrade.Count - 2).Value[1] + 0.5;
            this.Ratings.PointsPerGrade.ElementAt(this.Ratings.PointsPerGrade.Count - 1).Value[1] = this.Ratings.MaximumPoints;
        }
    }
}
