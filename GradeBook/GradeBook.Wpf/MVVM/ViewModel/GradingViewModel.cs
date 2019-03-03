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

        private ExamRatingDTO examRating;
        private List<StudentDTO> students;

        public GradingViewModel()
        {
            this.gradings = new ObservableCollection<GradingModel>();
            this.fileDialogService = new FileDialogService();
            this.messageBoxService = new MessageBoxService();
            this.examRating = new ExamRatingDTO();
            this.students = new List<StudentDTO>();
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

        public bool SaveGradingEnabled { get; private set; }

        private void GetGradingForm()
        {
            this.messageBoxService.ShowInfoMessage(
                "Please Select a xml-File containing the Exam Ratings.",
                "Select a Exam Rating File.");
            try
            {
               this.examRating = this.fileDialogService.OpenLoadFileDialog<ExamRatingDTO>();
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
                this.students = this.fileDialogService.OpenLoadFileDialog<List<StudentDTO>>();
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

            foreach (var problem in this.examRating.PointsPerProblems)
            {
                tmpProblemList.Add(problem.ProblemName);
            }

            foreach (var grading in this.Gradings)
            {
                for (int i = 0; i < tmpProblemList.Count; i++)
                {
                    grading.PointsPerProblems.Add(new PointsPerProblemItem() { PointsPerProblemValue = 0.0 });
                }

                foreach (var pointsPerProblem in grading.PointsPerProblems)
                {
                    pointsPerProblem.PointsPerProblemValueChanged += this.PointsPerProblem_PointsPerProblemValueChanged;
                }
            }

            this.ProblemList = tmpProblemList;
        }

        private void PointsPerProblem_PointsPerProblemValueChanged(object sender, EventArgs e)
        {
            PointsPerProblemItem item = sender as PointsPerProblemItem;

            foreach (var grading in this.gradings)
            {
                for (int i = 0; i < grading.PointsPerProblems.Count; i++)
                {
                    if (grading.PointsPerProblems[i].PointsPerProblemValue < 0
                        || grading.PointsPerProblems[i].PointsPerProblemValue > this.examRating.PointsPerProblems[i].PointsForProblem)
                    {
                        this.messageBoxService.ShowInfoMessage(
                                $"Please enter a Valid Value for Problem {this.examRating.PointsPerProblems[i].ProblemName}!"
                                + $"\n\rValue has to be non negative and smaller than {this.examRating.PointsPerProblems[i].PointsForProblem}",
                                "Invalid Value");

                        return;
                    }
                }
            }
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
