// <copyright file="RatingSchemeTest.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace RatingScheme.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GradeBook.Ratings;
    using GradeBook.Ratings.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test fixture that contains automated unit tests for the <see cref="RatingScheme"/> class.
    /// </summary>
    /// <remarks>
    /// This class makes use of naming conventions suggested by
    /// <see href="http://osherove.com/blog/2005/4/3/naming-standards-for-unit-tests.html">
    /// Roy Osherove
    /// </see>.
    /// The test methods are structured according to the Arrange Act Assert Pattern by
    /// <see href="http://www.arrangeactassert.com/why-and-what-is-arrange-act-assert">
    /// Jag Reehal
    /// </see>.
    /// The documentation text is structured according to the Given When Then Pattern by
    /// <see href="http://martinfowler.com/bliki/GivenWhenThen.html">Martin Fowler</see>.
    /// </remarks>
    [TestClass]
    public class RatingSchemeTest
    {
        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> A correctly intitialized <see cref="RatingScheme"/> object,
        /// <i>when</i> getting the <see cref="RatingScheme.Ratings"/>,
        /// <i>then</i> the <see cref="RatingScheme.Ratings"/> contains the expected values.
        /// </summary>
        [TestMethod]
        public void GetRaitingsForExam_RaitingSchemeObjectIsCorrectlyInitialized_TheRaitingsPropertyContainsTheExpectedValues()
        {
            // Arrange
            IRatingScheme ratingScheme = new RatingScheme(3, new List<int>() { 2, 1, 2 });
            RatingSchemeDTO ratings = new RatingSchemeDTO();

            // Act
            ratings = ratingScheme.Ratings;

            // Assert
            Assert.IsTrue(
                ratings.MaximumPoints == 5 && ratings.NumberOfProblems == 3,
                "The RatingSchemeDTO contains the expected Values.");
        }

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> Different values for the numberOfProblems and the Count of the pointsPerProblem List,
        /// <i>when</i> creating a <see cref="RatingScheme"/> object,
        /// <i>then</i> a <see cref="RatingSchemeException"/> is thrown.
        /// </summary>
        [TestMethod]
        public void CreateRaitingSchemeObject_RatingSchemeObjectWithDifferentValuesForNumberOfProblemsAndCountOfPointsPerProblemList_RatingSchemeExceptionIsThrown()
        {
            try
            {
                // Arrange
                // Act
                IRatingScheme ratingScheme = new RatingScheme(3, new List<int>() { 1, 2 });
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(
                    ex is RatingSchemeException,
                    $"The thrown Exception is a {nameof(RatingSchemeException)}, when the numberOfProblems and Count of pointsPerProblem do not match.");
            }
        }

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> The numberOfProblems = 0,
        /// <i>when</i> creating a <see cref="RatingScheme"/> object,
        /// <i>then</i> a <see cref="RatingSchemeException"/> is thrown.
        /// </summary>
        [TestMethod]
        public void CreatingRatingSchemeObject_RatingSchemeObjectWithNumberOfProblemsIs0_RatingSchemeExceptionIsThrown()
        {
            try
            {
                // Arrange
                // Act
                IRatingScheme ratingScheme = new RatingScheme(0, new List<int>());
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(
                       ex is RatingSchemeException,
                       $"The thrown Exception is a {nameof(RatingSchemeException)}, when the numberOfProblems is 0.");
            }
        }

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> A correctly intialized <see cref="RatingScheme"/> object,
        /// <i>when</i> getting the points per grade,
        /// <i>then</i> the received values match the expected values.
        /// </summary>
        [TestMethod]
        public void GetPointsPerGrade_RatingSchemeObjectCorrectlyInitialized_ReceivedValuesMatchExpectedValues()
        {
            // Arrange
            IRatingScheme ratingScheme = new RatingScheme(4, new List<int>() { 10, 20, 30, 40 });
            Dictionary<double, double[]> pointsPerGrade = new Dictionary<double, double[]>();

            for (int i = 0; i < 3; i++)
            {
                pointsPerGrade.Add(i + 1, new double[2]);
            }

            // Act
            pointsPerGrade = ratingScheme.Ratings.PointsPerGrade;

            // Assert
            Assert.IsTrue(
                pointsPerGrade.ElementAt(0).Value[0] == 0.0
                && pointsPerGrade.ElementAt(0).Value[1] == 39.5
                && pointsPerGrade.ElementAt(1).Value[0] == 40.0
                && pointsPerGrade.ElementAt(1).Value[1] == 49.5,
                "The received values match the expected values");
        }
    }
}
