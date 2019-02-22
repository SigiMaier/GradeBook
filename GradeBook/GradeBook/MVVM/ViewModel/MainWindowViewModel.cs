// <copyright file="MainWindowViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.MVVM.ViewModel
{
    using System.Windows.Input;
    using Basics.MVVM;
    using GradeBook.MVVM.Views;

    /// <summary>
    /// View Model for the Main Window.
    /// Creates and shows all SubViews.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly object ratingView;
        private readonly object studentsView;
        private readonly object gradingView;
        private readonly object statisticView;

        private ICommand gotoRatingViewCommand;
        private ICommand gotoStudentsViewCommand;
        private ICommand gotoGradingViewCommand;
        private ICommand gotoStatisticViewCommand;

        private object currentView;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.ratingView = new RatingView();
            this.studentsView = new StudentsView();
            this.gradingView = new GradingView();
            this.statisticView = new StatisticView();
            this.currentView = this.ratingView;
        }

        /// <summary>
        /// Gets or sets the CurrentView.
        /// </summary>
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

        /// <summary>
        /// Gets the GotoRatingViewCommand and executes the <see cref="GotoRatingView"/> Method.
        /// </summary>
        public ICommand GotoRatingViewCommand
        {
            get
            {
                return this.gotoRatingViewCommand ??
                    (this.gotoRatingViewCommand = new RelayCommand(_ => this.GotoRatingView()));
            }
        }

        /// <summary>
        /// Gets the GotoStudentsViewCommand and executes the <see cref="GotoStudentsView"/> Method.
        /// </summary>
        public ICommand GotoStudentsViewCommand
        {
            get
            {
                return this.gotoStudentsViewCommand ??
                    (this.gotoStudentsViewCommand = new RelayCommand(_ => this.GotoStudentsView()));
            }
        }

        /// <summary>
        /// Gets the GotoGradingViewCommand and executes the <see cref="GotoGradingView"/> Method.
        /// </summary>
        public ICommand GotoGradingViewCommand
        {
            get
            {
                return this.gotoGradingViewCommand ??
                    (this.gotoGradingViewCommand = new RelayCommand(_ => this.GotoGradingView()));
            }
        }

        /// <summary>
        /// Gets the GotoStatisticViewCommand and executes the <see cref="GotoStatisticView"/> Method.
        /// </summary>
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
