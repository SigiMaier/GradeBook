using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Base.Tests.FileHandling.TestObjects
{
    public class SimpleTestClassWithUsageOfXmlElement
    {
        [XmlElement("Number")]
        public int HouseNumber { get; set; }

        [XmlElement("Street")]
        public string StreetName { get; set; }

        [XmlElement("CityName")]
        public string City { get; set; }
    }
}
