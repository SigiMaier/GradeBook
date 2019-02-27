// <copyright file="GradingViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Wpf.MVVM.ViewModel
{
    using Basics.MVVM;
    using GradeBook.Rating.Contracts;
    using GradeBook.Wpf.MVVM.Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    /// <summary>
    /// ViewModel for the <see cref="Views.GradingView"/>
    /// </summary>
    public class GradingViewModel : ViewModelBase
    {
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxService messageBoxService;

        private ICommand getGradingFormCommand;
        private ICommand calculateExamGradingCommand;
        private ICommand saveGradingCommand;

        private ObservableCollection<GradingModel> gradings;
        private List<string> problemList;

        public GradingViewModel()
        {
            this.gradings = new ObservableCollection<GradingModel>();
            this.fileDialogService = new FileDialogService();
            this.messageBoxService = new MessageBoxService();
        }

        public ICommand GetGradingFormCommand
        {
            get
            {
                return this.getGradingFormCommand ??
                    (this.getGradingFormCommand = new RelayCommand(_ => this.GetGradingForm()));
            }
        }

        public ICommand CalculateExamGradingCommand
        {
            get
            {
                return this.calculateExamGradingCommand ??
                    (this.calculateExamGradingCommand = new RelayCommand(_ => this.CalculateGrading()));
            }
        }

        public ICommand SaveGradingCommand
        {
            get
            {
                return this.saveGradingCommand ??
                    (this.saveGradingCommand = new RelayCommand(_ => this.SaveGrading()));
            }
        }

        public ObservableCollection<GradingModel> Gradings
        {
            get
            {
                return this.gradings;
            }

            set
            {
                this.gradings = value;
                this.OnPropertyChanged(nameof(this.Gradings));
            }
        }

        public List<string> ProblemList
        {
            get
            {
                return this.problemList;
            }

            set
            {
                this.problemList = value;
                this.OnPropertyChanged(nameof(this.ProblemList));
            }
        }

        public bool CalculateExamGradingEnabled
        {
            get
            {
                return this.Gradings.Count > 0;
            }
        }

        public bool SaveGradingEnabled { get; private set; }

        private void GetGradingForm()
        {
            ExamRatingDTO examRatingModel = new ExamRatingDTO();
            List<StudentDTO> students = new List<StudentDTO>();

            this.messageBoxService.ShowInfoMessage(
                "Please Select a xml-File containing the Exam Ratings.",
                "Select a Exam Rating File.");
            try
            {
               examRatingModel = this.fileDialogService.OpenLoadFileDialog<ExamRatingDTO>();
            }
            catch (InvalidOperationException ex)
            {
                this.messageBoxService.ShowErrorMessage("Please select a correct Exam Rating File.", ex);
            }

            this.messageBoxService.ShowInfoMessage(
                "Please Select a xml-File containing the Students Information.",
                "Select a Student Information File.");
            try
            {
                students = this.fileDialogService.OpenLoadFileDialog<List<StudentDTO>>();
            }
            catch (InvalidOperationException ex)
            {
                this.messageBoxService.ShowErrorMessage("Please select a correct Students File.", ex);
            }

            ObservableCollection<GradingModel> tmpGradings = new ObservableCollection<GradingModel>();

            foreach (var student in students)
            {
                tmpGradings.Add(new GradingModel()
                {
                    MatriculationNumber = student.MatriculationNumber.ToString(),
                    PointsPerProblems = new List<double>(),
                    Grade = 0.0,
                    TotalScore = 0.0
                });
            }

            List<string> tmpProblemList = new List<string>();

            foreach (var problem in examRatingModel.PointsPerProblems)
            {
                tmpProblemList.Add(problem.ProblemName);
            }

            this.Gradings = tmpGradings;
            this.ProblemList = tmpProblemList;
        }

        private void CalculateGrading()
        {
            this.SaveGradingEnabled = true;
            this.OnPropertyChanged(nameof(this.SaveGradingEnabled));
        }

        private void SaveGrading()
        {

        }
    }
}
