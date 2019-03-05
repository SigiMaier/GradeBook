// <copyright file="FileDialogService.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.MVVM
{
    using Microsoft.WindowsAPICodePack.Dialogs;

    /// <summary>
    /// Service for showing File Dialogs from the ViewModel.
    /// </summary>
    public class FileDialogService : IFileDialogService
    {
        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
