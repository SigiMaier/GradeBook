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
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    /// ViewModel for the <see cref="Views.GradingView"/>
    /// </summary>
    public class GradingViewModel : ViewModelBase
    {
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxService messageBoxService;

        private ICommand getGradingFormCommand;
        private ICommand enterPointsPerProblemForEachStudentCommand;
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

        public ICommand EnterPointsPerProblemForEachStudentCommand
        {
            get
            {
                return this.enterPointsPerProblemForEachStudentCommand ??
                    (this.enterPointsPerProblemForEachStudentCommand = new RelayCommand(_ =>
                    this.EnterPointsPerProblemForEachStudent()));
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

                foreach (var item in this.Gradings)
                {
                    this.OnPropertyChanged(nameof(item.PointsPerProblems));
                }
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

        public bool EnterPointsPerProblemEnabled
        {
            get
            {
                return this.Gradings.Count > 0;
            }
        }

        //public bool CalculateExamGradingEnabled
        //{
        //    get
        //    {
        //      //  return this.Gradings.Any(g => g.PointsPerProblems.Any(p => p != 0.0));
        //    }
        //}

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

            foreach (var student in students)
            {
                this.Gradings.Add(new GradingModel()
                {
                    MatriculationNumber = student.MatriculationNumber.ToString(),
                    PointsPerProblems = new ObservableCollection<PointsPerProblemItem>(),
                    Grade = 0.0,
                    TotalScore = 0.0
                });
            }

            List<string> tmpProblemList = new List<string>();

            foreach (var problem in examRatingModel.PointsPerProblems)
            {
                tmpProblemList.Add(problem.ProblemName);
            }

            foreach (var grading in this.Gradings)
            {
                for (int i = 0; i < tmpProblemList.Count; i++)
                {
                    grading.PointsPerProblems.Add(new PointsPerProblemItem() { DoubleValue = 0.0 });
                }
            }

            this.ProblemList = tmpProblemList;
        }

        private void EnterPointsPerProblemForEachStudent()
        {
        }

        private void CalculateGrading()
        {
            //this.OnPropertyChanged(nameof(this.Gradings));
            this.SaveGradingEnabled = true;
            this.OnPropertyChanged(nameof(this.SaveGradingEnabled));
        }

        private void SaveGrading()
        {

        }
    }
}
