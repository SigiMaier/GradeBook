// <copyright file="GradingViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Wpf.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Basics.MVVM;
    using GradeBook.Rating.Contracts;
    using GradeBook.Wpf.MVVM.Model;

    /// <summary>
    /// ViewModel for the <see cref="Views.GradingView"/>
    /// </summary>
    public class GradingViewModel : ViewModelBase
    {
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxService messageBoxService;

        private ICommand getGradingFormCommand;
        private ICommand saveGradingCommand;
        private List<ProblemDTO> problemList;

        private ExamRatingDTO examRating;
        private List<StudentDTO> students;
        private double totalExamPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradingViewModel"/> class.
        /// </summary>
        public GradingViewModel()
        {
            this.Gradings = new ObservableCollection<GradingModel>();
            this.fileDialogService = new FileDialogService();
            this.messageBoxService = new MessageBoxService();
            this.examRating = new ExamRatingDTO();
            this.students = new List<StudentDTO>();
        }

        /// <summary>
        /// Gets the Command for Getting the Grading Form.
        /// </summary>
        public ICommand GetGradingFormCommand
        {
            get
            {
                return this.getGradingFormCommand ??
                    (this.getGradingFormCommand = new RelayCommand(_ => this.GetGradingForm()));
            }
        }

        /// <summary>
        /// Gets the Command for Saving the Grading.
        /// </summary>
        public ICommand SaveGradingCommand
        {
            get
            {
                return this.saveGradingCommand ??
                    (this.saveGradingCommand = new RelayCommand(_ => this.SaveGrading()));
            }
        }

        /// <summary>
        /// Gets or sets the Gradings.
        /// </summary>
        public ObservableCollection<GradingModel> Gradings { get; set; }

        /// <summary>
        /// Gets or sets the ProblemList.
        /// </summary>
        public List<ProblemDTO> ProblemList
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

        /// <summary>
        /// Gets or sets the TotalExamPoints to visualize in the View.
        /// </summary>
        public double TotalExamPoints
        {
            get
            {
                return this.totalExamPoints;
            }

            set
            {
                this.totalExamPoints = value;
                this.OnPropertyChanged(nameof(this.TotalExamPoints));
            }
        }

        private void GetGradingForm()
        {
            this.LoadExamRating();

            this.LoadStudentInformation();

            this.CreateGradingForm();

            this.CreateProblemList();
        }

        private void LoadExamRating()
        {
            this.messageBoxService.ShowInfoMessage(
                            "Please Select a XML-File containing the Exam Ratings.",
                            "Select a Exam Rating File.");
            try
            {
                this.examRating = this.fileDialogService.OpenLoadFileDialog<ExamRatingDTO>();
            }
            catch (InvalidOperationException ex)
            {
                this.messageBoxService.ShowErrorMessage("Please select a correct Exam Rating File.", ex);
            }
        }

        private void LoadStudentInformation()
        {
            this.messageBoxService.ShowInfoMessage(
                            "Please Select a XML-File containing the Students Information.",
                            "Select a Student Information File.");
            try
            {
                this.students = this.fileDialogService.OpenLoadFileDialog<List<StudentDTO>>();
            }
            catch (InvalidOperationException ex)
            {
                this.messageBoxService.ShowErrorMessage("Please select a correct Students File.", ex);
            }
        }

        private void CreateGradingForm()
        {
            foreach (var student in this.students)
            {
                this.Gradings.Add(new GradingModel()
                {
                    MatriculationNumber = student.MatriculationNumber.ToString(),
                    PointsPerProblems = new ObservableCollection<DoubleItem>(),
                    Grade = 0.0,
                    TotalScore = 0.0
                });
            }
        }

        private void CreateProblemList()
        {
            List<ProblemDTO> tmpProblemList = new List<ProblemDTO>();

            foreach (var problem in this.examRating.PointsPerProblems)
            {
                tmpProblemList.Add(problem);
            }

            foreach (var grading in this.Gradings)
            {
                for (int i = 0; i < tmpProblemList.Count; i++)
                {
                    grading.PointsPerProblems.Add(new DoubleItem() { DoubleValue = 0.0 });
                }

                foreach (var pointsPerProblem in grading.PointsPerProblems)
                {
                    pointsPerProblem.DoubleValueChanged += this.PointsPerProblem_PointsPerProblemValueChanged;
                }
            }

            this.ProblemList = tmpProblemList;
            this.TotalExamPoints = this.examRating.TotalPoints;
        }

        private void PointsPerProblem_PointsPerProblemValueChanged(object sender, EventArgs e)
        {
            DoubleItem item = sender as DoubleItem;

            try
            {
                this.CheckInputValues();
            }
            catch (InputValueException)
            {
                // TODO: Implement Logging....
                return;
            }

            this.CalculateGradeAndTotalScore();
        }

        private void SaveGrading()
        {
            this.fileDialogService.OpenSaveFileDialog(this.Gradings);
        }

        private void CheckInputValues()
        {
            foreach (var grading in this.Gradings)
            {
                for (int i = 0; i < grading.PointsPerProblems.Count; i++)
                {
                    if (grading.PointsPerProblems[i].DoubleValue < 0
                        || grading.PointsPerProblems[i].DoubleValue > this.examRating.PointsPerProblems[i].PointsForProblem)
                    {
                        grading.TotalScore = 0.0;
                        grading.Grade = 5.0;
                        this.messageBoxService.ShowInfoMessage(
                                $"Please enter a Valid Value for Problem {this.examRating.PointsPerProblems[i].ProblemName}!"
                                + $"\n\rValue has to be non negative and smaller than {this.examRating.PointsPerProblems[i].PointsForProblem}",
                                "Invalid Value");

                        throw new InputValueException("Input Value for Problem was not in range!");
                    }
                }
            }
        }

        private void CalculateGradeAndTotalScore()
        {
            foreach (var grading in this.Gradings)
            {
                grading.CalculateGradeAndTotalScore(this.examRating.PointsPerGrade);
            }
        }
    }
}
