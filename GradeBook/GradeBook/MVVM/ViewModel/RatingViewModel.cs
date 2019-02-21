using GradeBook.Base.MVVM;
using GradeBook.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GradeBook.MVVM.ViewModel
{
    public class RatingViewModel : ViewModelBase
    {
        private ICommand addPointsToListCommand;
        private ICommand addProblemCommand;
        private ICommand removeProblemCommand;
        private ICommand calculateRatingCommand;
        private int numberOfProblems;
        private ObservableCollection<ProblemModel> problems;
        private int totalPoints;
        private int pointsForProblem;
        private RatingModel rating;
        private IMessageBoxService messageBoxService;
        private ObservableCollection<GradeRatingModel> gradeRatings;

        public RatingViewModel()
        {
            this.problems = new ObservableCollection<ProblemModel>();
            this.gradeRatings = new ObservableCollection<GradeRatingModel>();
            this.messageBoxService = new MessageBoxService();
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
                List<int> pointsPerProblem = new List<int>();

                foreach (var item in this.Problems)
                {
                    pointsPerProblem.Add(item.PointsForProblem);
                }

                this.rating = new RatingModel(this.numberOfProblems, pointsPerProblem);

                foreach (var item in this.rating.Rating.PointsPerGrade)
                {
                    this.GradeRatings.Add(new GradeRatingModel() { Grade = item.Key, LowerBoundary = item.Value[0], UpperBoundary = item.Value[1] });
                }
            }
        }
    }
}
