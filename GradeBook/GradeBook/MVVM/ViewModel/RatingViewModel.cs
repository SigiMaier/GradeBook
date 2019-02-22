// <copyright file="RatingViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.MVVM.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Basics.MVVM;
    using GradeBook.Base;
    using GradeBook.MVVM.Model;

    /// <summary>
    /// ViewModel for <see cref="Views.RatingView"/>.
    /// </summary>
    public class RatingViewModel : ViewModelBase
    {
        private readonly IMessageBoxService messageBoxService;

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
            this.problems = new ObservableCollection<ProblemModel>();
            this.gradeRatings = new ObservableCollection<GradeRatingModel>();
        }

        public ICommand AddProblemCommand
        {
            get
            {
                return this.addProblemCommand ??
                    (this.addProblemCommand = new RelayCommand(_ => this.AddProblem()));
            }
        }

        public ICommand RemoveProblemCommand
        {
            get
            {
                return this.removeProblemCommand ??
                    (this.removeProblemCommand = new RelayCommand(_ => this.RemoveProblem()));
            }
        }

        public ICommand CalculateRatingCommand
        {
            get
            {
                return this.calculateRatingCommand ??
                    (this.calculateRatingCommand = new RelayCommand(_ => this.CalculateRating()));
            }
        }

        public ICommand SaveRatingCommand
        {
            get
            {
                return this.saveRatingCommand ??
                    (this.saveRatingCommand = new RelayCommand(_ => this.SaveRating()));
            }
        }

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
                this.OnPropertyChanged(nameof(this.SaveRatingsEnabled));
            }
        }

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
            this.Problems.Add(new ProblemModel() { ProblemName = $"Problem{this.numberOfProblems}", PointsForProblem = 0 });
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

            FileHandling.XmlSerializer.Serialize(examRatingModel, AppEnvironment.Instance.ExamRatingsFolder + $"\\{this.ExamName}.xml");
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
            return
                !string.IsNullOrEmpty(this.ExamName) && this.TotalPoints > 0;
        }
    }
}
