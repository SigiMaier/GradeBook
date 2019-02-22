// <copyright file="XmlDeserializer.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.FileHandling
{
    using System.IO;

    /// <summary>
    /// Class that Deserializes *.xml Files to Objects.
    /// The File will be deserialized to any object.
    /// </summary>
    public static class XmlDeserializer
    {
        /// <summary>
        /// Deserializes a file to an Object.
        /// </summary>
        /// <typeparam name="T">The Type of the Object to which the *.xml file should be deserialized.</typeparam>
        /// <param name="pathToFile">The complete Path to the *.xml file.</param>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string pathToFile)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            TextReader textReader = new StreamReader(pathToFile);
            object deserializedObject = xmlSerializer.Deserialize(textReader);

            T xmlData = (T)deserializedObject;

            textReader.Close();

            return xmlData;
        }
    }
}
