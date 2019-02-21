using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.FileHandling
{
    public static class XmlSerializer
    {
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
