// <copyright file="XmlSerializer.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace GradeBook.FileHandling
{
    /// <summary>
    /// Class that serializes Objects to a *.xml File.
    /// This Class routes the serialization to the <see cref="Basics.FileHandling.XmlSerializer"/>.
    /// </summary>
    public static class XmlSerializer
    {
        /// <summary>
        /// Serializes an Object to a *.xml File.
        /// The serialization is routed to <see cref="Basics.FileHandling.XmlSerializer.Serialize{T}(T, string)"/>.
        /// </summary>
        /// <typeparam name="T">The Type of the object that has to be serialized.</typeparam>
        /// <param name="objectToSerialize">The object that has to be serialized.</param>
        /// <param name="completeFilePath">The Path to the *.xml file.</param>
        public static void Serialize<T>(T objectToSerialize, string completeFilePath)
        {
            Basics.FileHandling.XmlSerializer.Serialize(objectToSerialize, completeFilePath);
        }
    }
}
