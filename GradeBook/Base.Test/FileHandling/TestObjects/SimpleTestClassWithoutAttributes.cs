// <copyright file="SimpleTestClassWithoutAttributes.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Base.Tests.FileHandling.TestObjects
{
    /// <summary>
    /// A TestClass for Testing Serialization and deserialization of xml files without attributes.
    /// </summary>
    public class SimpleTestClassWithoutAttributes
    {
        /// <summary>
        /// Gets or sets the HouseNumber.
        /// </summary>
        public int HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the StreetName.
        /// </summary>
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        public string City { get; set; }
    }
}
