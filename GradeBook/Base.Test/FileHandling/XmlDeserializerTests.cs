// <copyright file="XmlDeserializerTests.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Base.Tests.FileHandling
{
    using System;
    using Basics.FileHandling;
    using Base.Tests.FileHandling.TestObjects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// TestClass for <see cref="XmlDeserializer"/>.
    /// </summary>
    [TestClass]
    public class XmlDeserializerTests
    {
        private readonly string applicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> <see cref="SimpleTestClassWithoutAttributes"/> instance and a .xml File without Attributes,
        /// <i>when</i> deserializing the xml File,
        /// <i>then</i> the instance of <see cref="SimpleTestClassWithoutAttributes"/> contains the expected values.
        /// </summary>
        [TestMethod]
        public void DeserializeSimpleTestClassWithoutAttributes_FileAndObjectExists_FileIsDeserializedToObjectCorrectly()
        {
            // Arrange
            SimpleTestClassWithoutAttributes testClass = new SimpleTestClassWithoutAttributes();
            string fileName = this.applicationPath + "\\FileHandling\\TestFiles\\SimpleTestClassWithoutAttributesFile.xml";

            // Act
            testClass = XmlDeserializer.Deserialize<SimpleTestClassWithoutAttributes>(fileName);

            // Assert
            Assert.IsTrue(
                testClass.HouseNumber == 4 && testClass.City == "Baierbach",
                "The deserialized object contains the expected Data.");
        }

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> <see cref="SimpleTestClassDirectory"/> instance and a .xml File with multiple Entries under the same root,
        /// <i>when</i> deserializing the xml File,
        /// <i>then</i> the instance of <see cref="SimpleTestClassDirectory"/> contains the expected Data.
        /// </summary>
        [TestMethod]
        public void DeserializeTestFileWithMultipleEntries_FileAndObjectExists_FileIsDeserializedToObjectCorrectly()
        {
            // Arrange
            SimpleTestClassDirectory simpleTestClassDirectory = new SimpleTestClassDirectory();
            string fileName = this.applicationPath + "\\FileHandling\\TestFiles\\TestFileWithMultipleEntries.xml";

            // Act
            simpleTestClassDirectory = XmlDeserializer.Deserialize<SimpleTestClassDirectory>(fileName);

            SimpleTestClassWithoutAttributes firstElement = simpleTestClassDirectory.TestClasses[0];
            SimpleTestClassWithoutAttributes secondElement = simpleTestClassDirectory.TestClasses[1];

            // Assert
            Assert.IsTrue(
                firstElement.City == "Baierbach" && secondElement.City == "Stephanskirchen",
                "The deserialized object contains the expected Data.");
        }
    }
}
