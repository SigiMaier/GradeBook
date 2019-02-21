using System;
using Base.FileHandling;
using Base.Tests.FileHandling.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Base.Tests.FileHandling
{
    [TestClass]
    public class XmlDeserializerTest
    {
        private string applicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

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
