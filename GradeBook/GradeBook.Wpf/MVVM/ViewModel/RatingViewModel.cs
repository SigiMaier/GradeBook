// <copyright file="RatingViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Wpf.MVVM.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Basics.MVVM;
    using GradeBook.Wpf.MVVM.Model;

    /// <summary>
    /// ViewModel for <see cref="Views.RatingView"/>.
    /// </summary>
    public class RatingViewModel : ViewModelBase
    {
        private readonly IMessageBoxService messageBoxService;
        private readonly IFileDialogService fileDialogService;

        private RatingSchemeModel rating;

        private ICommand addProblemCommand;
        private ICommand removeProblemCommand;
        private ICommand calculateRatingCommand;
        private ICommand saveRatingCommand;

        private ObservableCollection<ProblemModel> problems;
        private ObservableCollection<GradeRatingModel> gradeRatings;

        private string examName;
        private int numberOfProblems;
        private int totalPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingViewModel"/> class.
        /// </summary>
        public RatingViewModel()
        {
            this.messageBoxService = new MessageBoxService();
            this.fileDialogService = new FileDialogService();
            this.problems = new ObservableCollection<ProblemModel>();
            this.gradeRatings = new ObservableCollection<GradeRatingModel>();
        }

        /// <summary>
        /// Gets the Command for Adding a Problem.
        /// </summary>
        public ICommand AddProblemCommand
        {
            get
            {
                return this.addProblemCommand ??
                    (this.addProblemCommand = new RelayCommand(_ => this.AddProblem()));
            }
        }

        /// <summary>
        /// Gets the Command for Removing a Problem.
        /// </summary>
        public ICommand RemoveProblemCommand
        {
            get
            {
                return this.removeProblemCommand ??
                    (this.removeProblemCommand = new RelayCommand(_ => this.RemoveProblem()));
            }
        }

        /// <summary>
        /// Gets the Command for Calculating the Rating.
        /// </summary>
        public ICommand CalculateRatingCommand
        {
            get
            {
                return this.calculateRatingCommand ??
                    (this.calculateRatingCommand = new RelayCommand(_ => this.CalculateRating()));
            }
        }

        /// <summary>
        /// Gets the Command for Saving the Rating.
        /// </summary>
        public ICommand SaveRatingCommand
        {
            get
            {
                return this.saveRatingCommand ??
                    (this.saveRatingCommand = new RelayCommand(_ => this.SaveRating()));
            }
        }

        /// <summary>
        /// Gets or sets the Values for the Problems and raises OnPropertyChanged for this property and the
        /// SaveRatingsEnabled Property.
        /// </summary>
        public ObservableCollection<ProblemModel> Problems
        {
            get
            {
                return this.problems;
            }

            set
            {
                this.problems = value;
                this.OnPropertyChanged(nameof(this.Problems));
                this.OnPropertyChanged(nameof(this.SaveRatingsEnabled));
            }
        }

        /// <summary>
        /// Gets or sets the Values for GradeRatings and raises OnPropertyChanged for this Property.
        /// </summary>
        public ObservableCollection<GradeRatingModel> GradeRatings
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

        /// <summary>
        /// Gets or sets the Value for ExamName and raises OnPropertyChanged for this Property and the
        /// SaveRatingEnabled Property.
        /// </summary>
        public string ExamName
        {
            get
            {
                return this.examName;
            }

            set
            {
                this.examName = value;
                this.OnPropertyChanged(nameof(this.ExamName));
                this.OnPropertyChanged(nameof(this.SaveRatingsEnabled));
            }
        }

        /// <summary>
        /// Gets or sets the Value for NumberOfProblems and raises OnPropertyChanged for this Property.
        /// </summary>
        public int NumberOfProblems
        {
            get
            {
                return this.numberOfProblems;
            }

            set
            {
                this.numberOfProblems = value;
                this.OnPropertyChanged(nameof(this.NumberOfProblems));
            }
        }

        /// <summary>
        /// Gets or sets the Value for TotalPoints and raises OnPropertyChanged for this Property.
        /// </summary>
        public int TotalPoints
        {
            get
            {
                return this.totalPoints;
            }

            set
            {
                this.totalPoints = value;
                this.OnPropertyChanged(nameof(this.TotalPoints));
            }
        }

        /// <summary>
        /// Gets a value indicating whether saving of ratings in enabled or not.
        /// </summary>
        public bool SaveRatingsEnabled
        {
            get
            {
                return this.SetSaveRatingsEnabled();
            }
        }

        private void AddProblem()
        {
            this.numberOfProblems++;
            this.Problems.Add(new ProblemModel()
            { ProblemName = $"Problem{this.numberOfProblems}", PointsForProblem = 0 });
        }

        private void RemoveProblem()
        {
            if (this.numberOfProblems > 0)
            {
                this.Problems.RemoveAt(this.numberOfProblems - 1);
                this.numberOfProblems--;
            }
        }

        private void CalculateRating()
        {
            if (this.numberOfProblems < 1)
            {
                this.messageBoxService.ShowInfoMessage("There are no problems to calculate the Rating", "Information");
            }
            else
            {
                this.GradeRatings.Clear();
                List<int> pointsPerProblem = this.GetPointsPerProblem();

                this.GetRating(pointsPerProblem);
            }
        }

        private void SaveRating()
        {
            ExamRatingModel examRatingModel = new ExamRatingModel
            {
                ExamName = this.ExamName,
                NumberOfProblems = this.numberOfProblems,
                PointsPerGrade = this.gradeRatings.ToList(),
                PointsPerProblems = this.problems.ToList(),
                TotalPoints = this.totalPoints
            };

            this.fileDialogService.OpenFileDialog(examRatingModel);
        }

        private List<int> GetPointsPerProblem()
        {
            List<int> pointsPerProblem = new List<int>();

            foreach (var item in this.Problems)
            {
                pointsPerProblem.Add(item.PointsForProblem);
            }

            return pointsPerProblem;
        }

        private void GetRating(List<int> pointsPerProblem)
        {
            this.rating = new RatingSchemeModel(this.numberOfProblems, pointsPerProblem);

            foreach (var item in this.rating.Rating.PointsPerGrade)
            {
                this.GradeRatings.Add(new GradeRatingModel()
                {
                    Grade = item.Key,
                    LowerBoundary = item.Value[0],
                    UpperBoundary = item.Value[1]
                });
            }

            this.TotalPoints = this.rating.Rating.MaximumPoints;
        }

        private bool SetSaveRatingsEnabled()
        {
            return !string.IsNullOrEmpty(this.ExamName) && this.TotalPoints > 0;
        }
    }
}
