﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.Base.MVVM
{
    public interface IMessageBoxService
    {
        void ShowInfoMessage(string text, string caption);
    }
}
