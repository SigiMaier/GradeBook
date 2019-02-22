using GradeBook.Ratings;
using GradeBook.Ratings.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.MVVM.Model
{
    public class RatingSchemeModel
    {
        private IRatingScheme ratingScheme;

        public RatingSchemeModel(int numberOfProblems, List<int> pointsPerProblem)
        {
            this.ratingScheme = new RatingScheme(numberOfProblems, pointsPerProblem);
            this.Rating = this.ratingScheme.Ratings;
        }

        public RatingSchemeDTO Rating { get; set; }
    }
}
