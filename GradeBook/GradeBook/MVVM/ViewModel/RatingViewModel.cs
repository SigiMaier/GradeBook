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
        private int numberOfProblems;
        private ObservableCollection<Problem> problems;
        private int totalPoints;
        private int pointsForProblem;

        public RatingViewModel()
        {
            this.problems = new ObservableCollection<Problem>();
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

        public ObservableCollection<Problem> Problems
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

        public int PointsForProblem
        {
            get
            {
                return this.pointsForProblem;
            }

            set
            {
                this.pointsForProblem = value;
                this.OnPropertyChanged(nameof(this.PointsForProblem));
            }
        }

        private void AddProblem()
        {
            this.numberOfProblems++;
            this.Problems.Add(new Problem() { ProblemName = $"Problem{this.numberOfProblems}", PointsForProblem = 0 });
        }

        private void RemoveProblem()
        {
            if (this.numberOfProblems > 0)
            {
                this.Problems.RemoveAt(this.numberOfProblems - 1);
                this.numberOfProblems--;
            }
            
        }
    }
}
