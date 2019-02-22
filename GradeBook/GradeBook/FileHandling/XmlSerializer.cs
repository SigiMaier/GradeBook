using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeBook.Base;
using Basics.FileHandling;

namespace GradeBook.FileHandling
{
    public static class XmlSerializer
    {
        public static void Serialize<T>(T objectToSerialize, string completeFilePath)
        {
            Basics.FileHandling.XmlSerializer.Serialize(objectToSerialize, completeFilePath);
        }
    }
}
