﻿// <copyright file="IRatingScheme.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Rating.Contracts
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
    }
}
