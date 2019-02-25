// <copyright file="SimpleTestClassDirectory.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Basics.Tests.FileHandling.TestObjects
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Simple Test Class to test serialization to *.xml of a List of Objects.
    /// </summary>
    public class SimpleTestClassDirectory
    {
        /// <summary>
        /// Gets or sets  List of TestClasses that have to be serialized.
        /// </summary>
        [XmlElement("SimpleTestClassWithoutAttributes")]
        public List<SimpleTestClassWithoutAttributes> TestClasses { get; set; }
    }
}
