using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.MVVM
{
    /// <summary>
    /// Wrapper Class for double Values to be able to bind the PointsPerProblems via the ViewModel to the View.
    /// <see href="https://stackoverflow.com/questions/5972990/wpf-how-to-make-datagrid-binding-with-dynamic-columns-editable"/>
    /// answer EDIT 2
    /// </summary>
    // TODO: Move to Basics.MVVM and Rename to more generic Name.
    public class DoubleItem
    {
        private double doubleValue;

        public event EventHandler PointsPerProblemValueChanged;

        public double DoubleValue
        {
            get
            {
                return this.doubleValue;
            }

            set
            {
                this.doubleValue = value;

                EventHandler handler = this.PointsPerProblemValueChanged;

                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }
    }
}
