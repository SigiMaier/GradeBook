// <copyright file="RatingSchemeTest.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace RatingScheme.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::RatingScheme.Contracts;
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
        /// <i>Given</i> An instance of <see cref="RatingScheme"/>,
        /// <i>when</i> setting the Number of Problems for the Exam,
        /// <i>then</i> the ratingSchemeDto contains the set Number of Problems.
        /// </summary>
        [TestMethod]
        public void SetNumberOfProblemsWithLegalValue_RatingSchemeObjectIsCreated_TheReturnedDTOContainsTheSetNumberOfProblems()
        {
            // Arrange
            IRatingScheme ratingScheme = new RatingScheme();
            RatingSchemeDTO ratingSchemeDto = new RatingSchemeDTO();
            const int setNumberOfProblems = 4;

            // Act
            ratingScheme.SetNumberOfProblems(setNumberOfProblems);
            ratingSchemeDto = ratingScheme.Ratings;

            // Assert
            Assert.IsTrue(
                ratingSchemeDto.NumberOfProblems == setNumberOfProblems,
                "The ratingSchemeDto contains the set number of Problems");
        }

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> An instance of <see cref="RatingScheme"/>,
        /// <i>when</i> setting the number of Problems to an illegal value,
        /// <i>then</i> a <see cref="RatingSchemeException"/> is thrown.
        /// </summary>
        [TestMethod]
        public void SetNumberOfProblemsWithIllegalValue_RatingSchemeObjectIsCreated_RatingSchemeExceptionIsThrown()
        {
            // Arrange
            IRatingScheme ratingScheme = new RatingScheme();

            try
            {
                // Act
                ratingScheme.SetNumberOfProblems(0);
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(
                    ex is RatingSchemeException,
                    "When setting the number of Problems to an illegal value, a RatingSchemeException is thrown");
            }
        }

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> An instance of <see cref="RatingScheme"/> and the Number of Problems is set beforehand,
        /// <i>when</i> setting the Points per Problem,
        /// <i>then</i> the ratingSchemeDTO contains the set Values.
        /// </summary>
        [TestMethod]
        public void SetPointsPerProblem_RatingSchemeObjectIsCreatedAndNumberOfProblemsIsSetBeforeHand_TheReturnedValuesMatchTheSentValuesAndNoExceptionIsThrown()
        {
            // Arrange
            IRatingScheme ratingScheme = new RatingScheme();
            RatingSchemeDTO ratingSchemeDTO = new RatingSchemeDTO();

            ratingScheme.SetNumberOfProblems(4);

            List<int> ratings = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                ratings.Add(30);
            }

            // Act
            ratingScheme.SetPointsPerProblem(ratings);

            // Assert
            ratingSchemeDTO = ratingScheme.Ratings;

            List<int> ratingSchemeDTOValues = ratingSchemeDTO.PointsPerProblem.Values.ToList();

            Assert.IsTrue(
                ratings.SequenceEqual(ratingSchemeDTOValues),
                "The values from the DTO equal the set values");
        }

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> An instance of <see cref="RatingScheme"/> and the number of Problems is not set beforehand,
        /// <i>when</i> trying to set the points per Problem,
        /// <i>then</i> then a <see cref="RatingSchemeException"/> is thrown.
        /// </summary>
        [TestMethod]
        public void SetPointsPerProblem_RatingSchemeObjectIsCreatedAndNumberOfProblemsIsNotSetBeforeHand_RatingSchemeExceptionIsThrown()
        {
            // Arrange
            IRatingScheme ratingScheme = new RatingScheme();

            try
            {
                // Act
                ratingScheme.SetPointsPerProblem(new List<int>());
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(
                    ex is RatingSchemeException,
                    "A RatingSchemeException is thrown, when trying to set the points per Problem without setting the number of Problems beforehand.");
            }
        }

        [TestMethod]
        public void GetMaximumPoints_RatingSchemeObjectIsCreatedNumberOfProblemsIsSetCorrectlyAndPointsPerProblemWasSetCorrectly_TheExpectedMaximumPointsMatchesTheReceivedValue()
        {
            // Arrange
            IRatingScheme ratingScheme = new RatingScheme();
            ratingScheme.SetNumberOfProblems(2);
            List<int> pPp = new List<int>
            {
                10,
                30
            };
            ratingScheme.SetPointsPerProblem(pPp);

            // Act
            RatingSchemeDTO ratingSchemeDTO = ratingScheme.Ratings;

            // Assert
            Assert.IsTrue(ratingSchemeDTO.MaximumPoints == 40);
        }

        [TestMethod]
        public void GetPointsPerGrade___()
        {
            IRatingScheme ratingScheme = new RatingScheme();
            ratingScheme.SetNumberOfProblems(4);
            List<int> pPp = new List<int>
            {
                10,
                30,
                50,
                10
            };

            ratingScheme.SetPointsPerProblem(pPp);

            var result = ratingScheme.GetPointsPerGrade();
        }
    }
}
