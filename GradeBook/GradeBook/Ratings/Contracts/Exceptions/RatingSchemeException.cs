// <copyright file="RatingSchemeException.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Ratings.Contracts
{
    using System;

    /// <summary>
    /// Exception that is thrown by instances using <see cref="IRatingScheme"/>
    /// </summary>
    public class RatingSchemeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingSchemeException"/> class.
        /// </summary>
        public RatingSchemeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingSchemeException"/> class.
        /// </summary>
        /// <param name="message">The Exception Message.</param>
        public RatingSchemeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingSchemeException"/> class.
        /// </summary>
        /// <param name="message">The Exception Message.</param>
        /// <param name="innerException">The inner Exception.</param>
        public RatingSchemeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
