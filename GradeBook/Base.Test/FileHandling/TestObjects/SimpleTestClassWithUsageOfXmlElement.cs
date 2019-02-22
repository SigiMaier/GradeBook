// <copyright file="SimpleTestClassWithUsageOfXmlElement.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Base.Tests.FileHandling.TestObjects
{
    using System.Xml.Serialization;

    /// <summary>
    /// TestClass for testing Serialization and Deserialization of *.xml Files using XmlElement
    /// </summary>
    public class SimpleTestClassWithUsageOfXmlElement
    {
        /// <summary>
        /// Gets or sets the HouseNumber.
        /// </summary>
        [XmlElement("Number")]
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
