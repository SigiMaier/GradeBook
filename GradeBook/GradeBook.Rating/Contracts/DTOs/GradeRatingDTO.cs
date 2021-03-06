﻿// <copyright file="GradeRatingDTO.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Rating.Contracts
{
    /// <summary>
    /// Model that contains the Grade Ratings.
    /// </summary>
    public class GradeRatingDTO
    {
        /// <summary>
        /// Gets or sets the Grade.
        /// </summary>
        public double Grade { get; set; }

        /// <summary>
        /// Gets or sets the LowerBoundary of the Grade.
        /// </summary>
        public double LowerBoundary { get; set; }

        /// <summary>
        /// Gets or sets the UpperBoundary of the Grade.
        /// </summary>
        public double UpperBoundary { get; set; }
    }
}