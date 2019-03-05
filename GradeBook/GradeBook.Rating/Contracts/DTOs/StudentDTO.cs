// <copyright file="StudentDTO.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Rating.Contracts
{
    /// <summary>
    /// Represents Student Data.
    /// </summary>
    public class StudentDTO
    {
        /// <summary>
        /// Gets or sets the Name of the Student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the FirstName of the Student.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the Matriculation Number of the Student.
        /// </summary>
        public int MatriculationNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Student attended the Exam.
        /// </summary>
        public bool Attended { get; set; }
    }
}
