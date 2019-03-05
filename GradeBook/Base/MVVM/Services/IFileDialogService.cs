// <copyright file="IFileDialogService.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.MVVM
{
    /// <summary>
    /// Interface for the FileDialogService
    /// </summary>
    public interface IFileDialogService
    {
        /// <summary>
        /// Opens a Save File Dialog.
        /// </summary>
        /// <typeparam name="T">The Type of the model that should be saved.</typeparam>
        /// <param name="model">The model that should be saved.</param>
        void OpenSaveFileDialog<T>(T model);

        /// <summary>
        /// Opens a Load File Dialog.
        /// </summary>
        /// <typeparam name="T">The Type of the model that should be loaded.</typeparam>
        /// <returns>An Object of the wanted Type.</returns>
        T OpenLoadFileDialog<T>();
    }
}
