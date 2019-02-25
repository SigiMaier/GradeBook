using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.Rating.Contracts
{
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
