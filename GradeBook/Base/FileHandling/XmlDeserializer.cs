using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.FileHandling
{
    public static class XmlDeserializer
    {
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
