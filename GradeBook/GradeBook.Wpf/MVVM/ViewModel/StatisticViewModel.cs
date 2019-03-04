// <copyright file="StatisticViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Wpf.MVVM.ViewModel
{
    using Basics.MVVM;
    using GradeBook.Rating.Contracts;
    using GradeBook.Wpf.MVVM.Model;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

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
        private Dictionary<double, int> gradeNumbers;
        private ExamRatingDTO examRating;

        public StatisticViewModel()
        {
            this.fileDialogService = new FileDialogService();
            this.messageBoxService = new MessageBoxService();
            this.gradings = new List<GradingModel>();
            this.gradeRatings = new List<GradeRatingDTO>();
            this.gradeNumbers = new Dictionary<double, int>();
            this.examRating = new ExamRatingDTO();
        }

        public ICommand LoadStatisticFormCommand
        {
            get
            {
                return this.loadStatisticFormCommand ??
                    (this.loadStatisticFormCommand = new RelayCommand(_ => this.LoadStatisticForm()));
            }
        }

        public Dictionary<double, int> GradeNumbers
        {
            get
            {
                return this.gradeNumbers;
            }

            set
            {
                this.gradeNumbers = value;
                this.OnPropertyChanged(nameof(this.GradeNumbers));
            }
        }

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
            this.GetGradeRatings();
            this.LoadGrading();
            this.GetGradeNumbers();
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
            foreach (var gradeRating in this.gradeRatings)
            {
                this.GradeNumbers.Add(gradeRating.Grade, 0);
            }

            foreach (var grading in this.gradings)
            {
                this.GradeNumbers[grading.Grade]++;
            }

            this.OnPropertyChanged(nameof(this.GradeNumbers));
        }
    }
}
