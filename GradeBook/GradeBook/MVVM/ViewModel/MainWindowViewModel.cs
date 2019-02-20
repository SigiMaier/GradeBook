namespace GradeBook.MVVM.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using GradeBook.Base.MVVM;
    using GradeBook.MVVM.Views;

    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand gotoRatingViewCommand;
        private ICommand gotoStudentsViewCommand;
        private ICommand gotoGradingViewCommand;
        private ICommand gotoStatisticViewCommand;

        private object currentView;
        private object ratingView;
        private object studentsView;
        private object gradingView;
        private object statisticView;

        public MainWindowViewModel()
        {
            this.ratingView = new RatingView();
            this.studentsView = new StudentsView();
            this.gradingView = new GradingView();
            this.statisticView = new StatisticView();
            this.currentView = this.ratingView;
        }

        public object CurrentView
        {
            get
            {
                return this.currentView;
            }

            set
            {
                this.currentView = value;
                this.OnPropertyChanged(nameof(this.CurrentView));
            }
        }

        public ICommand GotoRatingViewCommand
        {
            get
            {
                return this.gotoRatingViewCommand ??
                    (this.gotoRatingViewCommand = new RelayCommand(_ => this.GotoRatingView()));
            }
        }

        public ICommand GotoStudentsViewCommand
        {
            get
            {
                return this.gotoStudentsViewCommand ??
                    (this.gotoStudentsViewCommand = new RelayCommand(_ => this.GotoStudentsView()));
            }
        }

        public ICommand GotoGradingViewCommand
        {
            get
            {
                return this.gotoGradingViewCommand ??
                    (this.gotoGradingViewCommand = new RelayCommand(_ => this.GotoGradingView()));
            }
        }

        public ICommand GotoStatisticViewCommand
        {
            get
            {
                return this.gotoStatisticViewCommand ??
                    (this.gotoStatisticViewCommand = new RelayCommand(_ => this.GotoStatisticView()));
            }
        }

        private void GotoRatingView()
        {
            this.CurrentView = this.ratingView;
        }

        private void GotoStudentsView()
        {
            this.CurrentView = this.studentsView;
        }

        private void GotoGradingView()
        {
            this.CurrentView = this.gradingView;
        }

        private void GotoStatisticView()
        {
            this.CurrentView = this.statisticView;
        }
    }
}
