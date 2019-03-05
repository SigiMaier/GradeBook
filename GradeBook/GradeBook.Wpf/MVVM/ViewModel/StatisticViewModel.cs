// <copyright file="StatisticViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Wpf.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Basics.MVVM;
    using GradeBook.Rating.Contracts;
    using GradeBook.Wpf.MVVM.Model;

    /// <summary>
    /// ViewModel for <see cref="Views.StatisticView"/>.
    /// </summary>
    public class StatisticViewModel : ViewModelBase
    {
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxService messageBoxService;

        private ICommand loadStatisticFormCommand;

        private List<GradingModel> gradings;
        private List<GradeRatingDTO> gradeRatings;
        private ExamRatingDTO examRating;
        private List<StatisticModel> statistics;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticViewModel"/> class.
        /// </summary>
        public StatisticViewModel()
        {
            this.fileDialogService = new FileDialogService();
            this.messageBoxService = new MessageBoxService();
            this.gradings = new List<GradingModel>();
            this.gradeRatings = new List<GradeRatingDTO>();
            this.examRating = new ExamRatingDTO();
            this.statistics = new List<StatisticModel>();
        }

        /// <summary>
        /// Gets the command for Loading the Statistic Form
        /// </summary>
        public ICommand LoadStatisticFormCommand
        {
            get
            {
                return this.loadStatisticFormCommand ??
                    (this.loadStatisticFormCommand = new RelayCommand(_ => this.LoadStatisticForm()));
            }
        }

        /// <summary>
        /// Gets or sets the Statistics.
        /// </summary>
        public List<StatisticModel> Statistics
        {
            get
            {
                return this.statistics;
            }

            set
            {
                this.statistics = value;
                this.OnPropertyChanged(nameof(this.Statistics));
            }
        }

        /// <summary>
        /// Gets or sets the GradeRatings.
        /// </summary>
        public List<GradeRatingDTO> GradeRatings
        {
            get
            {
                return this.gradeRatings;
            }

            set
            {
                this.gradeRatings = value;
                this.OnPropertyChanged(nameof(this.GradeRatings));
            }
        }

        private void LoadStatisticForm()
        {
            this.ClearCollections();
            this.GetGradeRatings();
            this.LoadGrading();
            this.GetGradeNumbers();
        }

        private void ClearCollections()
        {
            this.gradeRatings.Clear();
            this.gradings.Clear();
            this.statistics.Clear();
        }

        private void GetGradeRatings()
        {
            this.messageBoxService.ShowInfoMessage(
                            "Please Select a XML-File containing the Exam Ratings.",
                            "Select an Exam Rating File.");
            try
            {
                this.examRating = this.fileDialogService.OpenLoadFileDialog<ExamRatingDTO>();
            }
            catch (InvalidOperationException ex)
            {
                this.messageBoxService.ShowErrorMessage("Please select a correct Exam Rating File.", ex);
            }

            this.GradeRatings = this.examRating.PointsPerGrade;
        }

        private void LoadGrading()
        {
            this.messageBoxService.ShowInfoMessage(
                "Please Select a XML-File containing the Exam Grading.",
                "Select a Grading File.");

            try
            {
                this.gradings = this.fileDialogService.OpenLoadFileDialog<List<GradingModel>>();
            }
            catch (InvalidOperationException ex)
            {
                this.messageBoxService.ShowErrorMessage("Please select a correct Grading File.", ex);
            }
        }

        private void GetGradeNumbers()
        {
            List<StatisticModel> tmpGradingNumbers = new List<StatisticModel>();

            foreach (var gradeRating in this.gradeRatings)
            {
                tmpGradingNumbers.Add(new StatisticModel() { Grade = gradeRating.Grade, Amount = 0, Percentage = 0.0 });
            }

            foreach (var grading in this.gradings)
            {
                tmpGradingNumbers.First(g => g.Grade == grading.Grade).Amount++;
            }

            foreach (var grading in this.gradings)
            {
                tmpGradingNumbers.First(g => g.Grade == grading.Grade).Percentage =
                    (double)tmpGradingNumbers.First(g => g.Grade == grading.Grade).Amount / this.gradings.Count * 100;
            }

            this.Statistics = tmpGradingNumbers;
        }
    }
}
