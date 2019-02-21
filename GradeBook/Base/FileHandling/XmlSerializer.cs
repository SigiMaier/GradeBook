// <copyright file="XmlSerializer.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Base.FileHandling
{
    using System.IO;

    /// <summary>
    /// Class that serializes Objects to a *.xml File.
    /// </summary>
    public static class XmlSerializer
    {
        /// <summary>
        /// Serializes an Object to a *.xml File
        /// </summary>
        /// <typeparam name="T">The Type of the object that has to be serialized.</typeparam>
        /// <param name="objectToSerialize">The object that has to be serialized.</param>
        /// <param name="completeFilePath">The Path to the *.xml file.</param>
        public static void Serialize<T>(T objectToSerialize, string completeFilePath)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (TextWriter textWriter = new StreamWriter(completeFilePath))
            {
                xmlSerializer.Serialize(textWriter, objectToSerialize);
            }
        }
    }
}
