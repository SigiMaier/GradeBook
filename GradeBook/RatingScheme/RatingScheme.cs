// <copyright file="RatingScheme.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace RatingScheme
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using global::RatingScheme.Contracts;

    /// <summary>
    /// Class for Setting and Getting Values regarding the Rating Scheme for the Exam,
    /// implements <see cref="IRatingScheme"/>
    /// </summary>
    public class RatingScheme : IRatingScheme
    {
        private readonly Dictionary<double, double> percentPerGrade;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingScheme"/> class.
        /// </summary>
        public RatingScheme()
        {
            this.Ratings = new RatingSchemeDTO()
            {
                MaximumPoints = 0,
                NumberOfProblems = 0,
                PointsPerProblem = new Dictionary<int, int>()
            };

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
        }

        /// <inheritdoc/>
        public RatingSchemeDTO Ratings { get; }

        /// <inheritdoc/>
        public void SetNumberOfProblems(int numberOfProblems)
        {
            if (numberOfProblems < 1)
            {
                throw new RatingSchemeException($"The value of {nameof(numberOfProblems)} has to be > 0."
                    + $" Set {nameof(numberOfProblems)} to a correct value.");
            }

            this.Ratings.NumberOfProblems = numberOfProblems;
        }

        /// <inheritdoc/>
        public void SetPointsPerProblem(List<int> pointsPerProblem)
        {
            if (this.Ratings.NumberOfProblems < 1)
            {
                throw new RatingSchemeException(nameof(this.Ratings.NumberOfProblems)
                    + " was not set to correct "
                    + $"value > 1. Call {nameof(this.SetNumberOfProblems)} first.");
            }

            for (int i = 0; i < this.Ratings.NumberOfProblems; i++)
            {
                this.Ratings.PointsPerProblem.Add(i + 1, pointsPerProblem[i]);
            }

            this.SetMaximumPoints();
        }

        public Dictionary<double, double[]> GetPointsPerGrade()
        {
            Dictionary<double, double[]> pointsPerGrade = this.InitPointsPerGrade();

            this.InitFirstElementOfPointsPerGrade(pointsPerGrade);

            return this.CalculatePointsPerGrade(pointsPerGrade);
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

        private void InitFirstElementOfPointsPerGrade(Dictionary<double, double[]> pointsPerGrade)
        {
            pointsPerGrade.ElementAt(0).Value[0] = 0.0;
            pointsPerGrade.ElementAt(0).Value[1] = this.CalculateUpperPointsBoundaryForGrade(0);
        }

        private double CalculateUpperPointsBoundaryForGrade(int index)
        {
            return this.Ratings.MaximumPoints * this.percentPerGrade.ElementAt(index).Key / 100.0;
        }

        private Dictionary<double, double[]> CalculatePointsPerGrade(Dictionary<double, double[]> pointsPerGrade)
        {
            for (int i = 1; i < pointsPerGrade.Count - 1; i++)
            {
                pointsPerGrade.ElementAt(i).Value[0] = pointsPerGrade.ElementAt(i - 1).Value[1] + 0.5;
                pointsPerGrade.ElementAt(i).Value[1] =
                    this.CalculateUpperPointsBoundaryForGrade(i);
            }

            pointsPerGrade.ElementAt(pointsPerGrade.Count - 1).Value[0] = pointsPerGrade.ElementAt(pointsPerGrade.Count - 2).Value[1] + 0.5;
            pointsPerGrade.ElementAt(pointsPerGrade.Count - 1).Value[1] = this.Ratings.MaximumPoints;

            return pointsPerGrade;
        }
    }
}
