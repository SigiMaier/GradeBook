// <copyright file="StudentsViewModel.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.Wpf.MVVM.ViewModel
{
    using Basics.MVVM;
    using GradeBook.Rating.Contracts;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    /// <summary>
    /// ViewModel for <see cref="Views.StudentsView"/>.
    /// </summary>
    public class StudentsViewModel : ViewModelBase
    {
        private readonly IFileDialogService fileDialogService;

        private ICommand addStudentCommand;
        private ICommand removeStudentCommand;
        private ICommand saveStudentsCommand;

        private ObservableCollection<StudentDTO> students;
        private StudentDTO selectedStudent;

        private string studentName;
        private string studentFirstName;
        private int matriculationNumber;
        private bool studentAttended;

        public StudentsViewModel()
        {
            this.fileDialogService = new FileDialogService();
            this.students = new ObservableCollection<StudentDTO>();
            this.selectedStudent = new StudentDTO();
        }

        public ICommand AddStudentCommand
        {
            get
            {
                return this.addStudentCommand ??
                    (this.addStudentCommand = new RelayCommand(_ => this.AddStudent()));
            }
        }

        public ICommand RemoveStudentCommand
        {
            get
            {
                return this.removeStudentCommand ??
                    (this.removeStudentCommand = new RelayCommand(_ => this.RemoveStudent()));
            }
        }

        public ICommand SaveStudentsCommand
        {
            get
            {
                return this.saveStudentsCommand ??
                    (this.saveStudentsCommand = new RelayCommand(_ => this.SaveStudents()));
            }
        }

        public ObservableCollection<StudentDTO> Students
        {
            get
            {
                return this.students;
            }

            set
            {
                this.students = value;
                this.OnPropertyChanged(nameof(this.Students));
            }
        }

        public StudentDTO SelectedStudent
        {
            get
            {
                return this.selectedStudent;
            }

            set
            {
                this.selectedStudent = value;
                this.OnPropertyChanged(nameof(this.SelectedStudent));
                this.OnPropertyChanged(nameof(this.RemoveStudentEnabled));
            }
        }

        public string StudentName
        {
            get
            {
                return this.studentName;
            }

            set
            {
                this.studentName = value;
                this.OnPropertyChanged(nameof(this.StudentName));
                this.OnPropertyChanged(nameof(this.AddStudentEnabled));
            }
        }

        public string StudentFirstName
        {
            get
            {
                return this.studentFirstName;
            }

            set
            {
                this.studentFirstName = value;
                this.OnPropertyChanged(nameof(this.StudentFirstName));
                this.OnPropertyChanged(nameof(this.AddStudentEnabled));
            }
        }

        public int MatriculationNumber
        {
            get
            {
                return this.matriculationNumber;
            }

            set
            {
                this.matriculationNumber = value;
                this.OnPropertyChanged(nameof(this.MatriculationNumber));
                this.OnPropertyChanged(nameof(this.AddStudentEnabled));
            }
        }

        public bool StudentAttended
        {
            get
            {
                return this.studentAttended;
            }

            set
            {
                this.studentAttended = value;
                this.OnPropertyChanged(nameof(this.StudentAttended));
            }
        }

        public bool AddStudentEnabled
        {
            get
            {
                return this.SetAddStudentEnabled();
            }
        }

        public bool RemoveStudentEnabled
        {
            get
            {
                if (this.SelectedStudent != null)
                {
                    return !string.IsNullOrEmpty(this.SelectedStudent.FirstName);
                }
                else
                {
                    return false;
                }
            }
        }

        public bool SaveStudentsEnabled
        {
            get
            {
                return this.Students.Count > 0;
            }
        }

        private void AddStudent()
        {
            this.Students.Add(new StudentDTO()
            {
                Name = this.StudentName,
                FirstName = this.StudentFirstName,
                MatriculationNumber = this.MatriculationNumber,
                Attended = this.StudentAttended
            });

            this.OnPropertyChanged(nameof(this.AddStudentEnabled));
            this.OnPropertyChanged(nameof(this.SaveStudentsEnabled));
        }

        private void RemoveStudent()
        {
            if (this.SelectedStudent != null)
            {
                this.Students.Remove(this.SelectedStudent);
            }

            this.OnPropertyChanged(nameof(this.AddStudentEnabled));
            this.OnPropertyChanged(nameof(this.SaveStudentsEnabled));
        }

        private void SaveStudents()
        {
            this.fileDialogService.OpenFileDialog(this.Students);
        }

        private bool SetAddStudentEnabled()
        {
            return !string.IsNullOrEmpty(this.StudentName)
                && !string.IsNullOrEmpty(this.StudentFirstName)
                && this.MatriculationNumber > 0
                && !this.Students.Any(n => n.MatriculationNumber == this.MatriculationNumber);
        }
    }
}
