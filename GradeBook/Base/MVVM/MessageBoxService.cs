using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Base.MVVM
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowInfoMessage(string text, string caption)
        {
           MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
