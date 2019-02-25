// <copyright file="SimpleTestClassWithUsageOfXmlAttribute.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.Tests.FileHandling.TestObjects
{
    using System.Xml.Serialization;

    /// <summary>
    /// TestClass to Test Serialization and Deserialization of xml Files with Attributes and Elements.
    /// </summary>
    public class SimpleTestClassWithUsageOfXmlAttribute
    {
        /// <summary>
        /// Gets or sets the HouseNumber.
        /// </summary>
        [XmlAttribute("Number")]
        public int HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the StreetName.
        /// </summary>
        [XmlElement("Street")]
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        [XmlElement("CityName")]
        public string City { get; set; }
    }
}
