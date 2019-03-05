// <copyright file="DoubleItem.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.MVVM
{
    using System;

    /// <summary>
    /// Wrapper Class for double Values to be able to bind the PointsPerProblems via the ViewModel to the View.
    /// <see href="https://stackoverflow.com/questions/5972990/wpf-how-to-make-datagrid-binding-with-dynamic-columns-editable"/>
    /// answer EDIT 2
    /// </summary>
    public class DoubleItem
    {
        private double doubleValue;

        /// <summary>
        /// Event that is raised when the <see cref="DoubleValue"/> changed.
        /// </summary>
        public event EventHandler DoubleValueChanged;

        /// <summary>
        /// Gets or sets the DoubleValue and raises the <see cref="DoubleValueChanged"/> Event when set.
        /// </summary>
        public double DoubleValue
        {
            get
            {
                return this.doubleValue;
            }

            set
            {
                this.doubleValue = value;

                this.DoubleValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
