using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.MVVM
{
    public class FileDialogService : IFileDialogService
    {
        public void OpenSaveFileDialog<T>(T model)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog
            {
                InitialDirectory = @"C:\Desktop",
                IsFolderPicker = false,
                Title = "Please select a Folder and insert a Full Filename (Filename + .*)"
            };

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FileHandling.XmlSerializer.Serialize(model, commonOpenFileDialog.FileName);
            }
        }

        public T OpenLoadFileDialog<T>()
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog
            {
                InitialDirectory = @"C:\Desktop",
                IsFolderPicker = false,
                Title = "Please select a File to load"
            };

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return FileHandling.XmlDeserializer.Deserialize<T>(commonOpenFileDialog.FileName);
            }
            else
            {
                return (T)new object();
            }
        }
    }
}
