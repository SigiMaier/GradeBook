using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.MVVM
{
    public class InputValueException : Exception
    {
        public InputValueException()
        {
        }

        public InputValueException(string message)
            : base(message)
        {
        }

        public InputValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
