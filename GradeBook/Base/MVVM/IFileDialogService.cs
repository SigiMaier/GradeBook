using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.MVVM
{
    public interface IFileDialogService
    {
        void OpenFileDialog<T>(T model);
    }
}
