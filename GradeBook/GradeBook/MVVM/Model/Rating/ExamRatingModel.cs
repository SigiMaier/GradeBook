using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GradeBook.MVVM.Model
{
    //[XmlRoot("Exam Rating")]
    public class ExamRatingModel
    {
        //[XmlElement("Exam Name")]
        public string ExamName { get; set; }

        //[XmlElement("Number of Problems")]
        public int NumberOfProblems { get; set; }

        //[XmlElement("Total Points")]
        public double TotalPoints { get; set; }

        //[XmlElement("Points Per Problem")]
        public List<ProblemModel> PointsPerProblems { get; set; }

        //[XmlElement("Points Per Grade")]
        public List<GradeRatingModel> PointsPerGrade { get; set; }
    }
}
