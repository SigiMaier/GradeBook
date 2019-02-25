// <copyright file="GradingViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Wpf.MVVM.ViewModel
{
    using Basics.MVVM;
    using GradeBook.Rating.Contracts;
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

        private ObservableCollection<GradingDTO> gradings;

        public GradingViewModel()
        {
            this.gradings = new ObservableCollection<GradingDTO>();
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

        public ObservableCollection<GradingDTO> Gradings
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
            this.messageBoxService.ShowInfoMessage(
                "Please Select a xml-File containing the Exam Ratings.",
                "Select a Exam Rating File.");
            ExamRatingDTO examRatingModel = this.fileDialogService.OpenLoadFileDialog<ExamRatingDTO>();
            this.messageBoxService.ShowInfoMessage(
                "Please Select a xml-File containing the Students Information.",
                "Select a Student Information File.");
            List<StudentDTO> students = this.fileDialogService.OpenLoadFileDialog<List<StudentDTO>>();


            this.OnPropertyChanged(nameof(this.CalculateExamGradingEnabled));
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
