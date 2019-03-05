// <copyright file="GradingModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Wpf.MVVM.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Basics.MVVM;
    using GradeBook.Rating.Contracts;

    /// <summary>
    /// Class containing all required Values and Methods to calculate the Grading for an Exam.
    /// This class needs to implement INotifyPropertyChanged (and is therefore a derivative of ViewModelBase),
    /// since the Property in <see cref="ViewModel.GradingViewModel"/> containing these Values is bound in the
    /// <see cref="Views.GradingView"/> to the DataGridTextColumns and was not updated correctly.
    /// </summary>
    // TODO: Find a better solution for the Updating Problem of the View or move this class to ViewModel-Folder.
    public class GradingModel : ViewModelBase
    {
        private double grade;
        private double totalScore;

        /// <summary>
        /// Gets or sets the MatriculationNumber.
        /// </summary>
        public string MatriculationNumber { get; set; }

        /// <summary>
        /// Gets or sets the PointsPerProblems.
        /// </summary>
        public ObservableCollection<DoubleItem> PointsPerProblems { get; set; }

        /// <summary>
        /// Gets or sets the Grade.
        /// </summary>
        public double Grade
        {
            get
            {
                return this.grade;
            }

            set
            {
                this.grade = value;
                this.OnPropertyChanged(nameof(this.Grade));
            }
        }

        /// <summary>
        /// Gets or sets the TotalScore.
        /// </summary>
        public double TotalScore
        {
            get
            {
                return this.totalScore;
            }

            set
            {
                this.totalScore = value;
                this.OnPropertyChanged(nameof(this.TotalScore));
            }
        }

        /// <summary>
        /// Calculates the Grade and TotalScore.
        /// </summary>
        /// <param name="gradeRatings">Contains the values to determine the Grade.</param>
        public void CalculateGradeAndTotalScore(List<GradeRatingDTO> gradeRatings)
        {
            this.TotalScore = this.PointsPerProblems.Sum(n => n.DoubleValue);

            foreach (var gradeRating in gradeRatings)
            {
                if (this.totalScore >= gradeRating.LowerBoundary && this.totalScore <= gradeRating.UpperBoundary)
                {
                    this.Grade = gradeRating.Grade;
                }
            }
        }
    }
}
