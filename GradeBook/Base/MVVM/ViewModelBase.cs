using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Base.MVVM
{
    /// <summary>
    /// BaseClasse for ViewModels.
    /// Depending on the needs of the project this Class can be extended.
    /// But better, derive from this class and create a new BaseClass.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event for signalizing the View when a Property in the ViewModel changes
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method to set values from the ViewModel.
        /// Can be extended in order to e.g. trigger validation
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">The property that should be set.</param>
        /// <param name="value">The value the property has to be set to.</param>
        /// <param name="propertyName">The name of the property to be set.</param>
        /// <returns><c>true</c>when the value is changed,
        /// otherwise <c>false</c></returns>
        protected virtual bool SetProperty<T>(
            ref T storage,
            T value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;

            this.OnPropertyChanged(propertyName);

            return true;
        }
    }
}
