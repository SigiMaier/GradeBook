// <copyright file="InputValueException.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.MVVM
{
    using System;

    /// <summary>
    /// Exception that is thrown when the InputValue for a InputControl is invalid.
    /// </summary>
    public class InputValueException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputValueException"/> class.
        /// </summary>
        public InputValueException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValueException"/> class.
        /// </summary>
        /// <param name="message">The message of the Exception.</param>
        public InputValueException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputValueException"/> class.
        /// </summary>
        /// <param name="message">The message of the Exception.</param>
        /// <param name="innerException">The Inner Exception.</param>
        public InputValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
