using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.MVVM.Model
{
    public class GradeRatingModel
    {
        public double Grade { get; set; }

        public double LowerBoundary { get; set; }

        public double UpperBoundary { get; set; }
    }
}
