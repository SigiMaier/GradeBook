using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Base.Tests.FileHandling.TestObjects
{
    public class SimpleTestClassDirectory
    {
        [XmlElement("SimpleTestClassWithoutAttributes")]
        public List<SimpleTestClassWithoutAttributes> TestClasses = new List<SimpleTestClassWithoutAttributes>();
    }
}
