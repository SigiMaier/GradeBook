using System;
using System.IO;
using Base.FileHandling;
using Base.Tests.FileHandling.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Base.Tests.FileHandling
{
    [TestClass]
    public class XmlSerializerTests
    {
        private readonly string applicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        [TestMethod]
        public void XmlSerializeSimpleClassToFile_SimpleTestClassExists_FileIsCreated()
        {
            // Arrange
            SimpleTestClassWithoutAttributes testClass = new SimpleTestClassWithoutAttributes()
            {
                HouseNumber = 4,
                StreetName = "Edlinger Str.",
                City = "Baierbach"
            };

            string fileName = this.applicationPath + "\\simpleTestClass.xml";

            // Act
            XmlSerializer.Serialize(testClass, fileName);

            // Assert
            Assert.IsTrue(File.Exists(fileName), "The *.xml File was created");

            // CleanUp
            File.Delete(fileName);
        }

        [TestMethod]
        public void XmlSerializeTestClassWithUsageOfXmlElement_TestClassExists_FileIsCreated()
        {
            // Arrange
            SimpleTestClassWithUsageOfXmlElement testClass = new SimpleTestClassWithUsageOfXmlElement()
            {
                HouseNumber = 4,
                StreetName = "Edlinger Str.",
                City = "Baierbach"
            };

            string fileName = this.applicationPath + "\\simpleTestClassWithXmlElement.xml";

            // Act
            XmlSerializer.Serialize(testClass, fileName);

            // Assert
            Assert.IsTrue(File.Exists(fileName), "The *.xml File was created");

            // CleanUp
            File.Delete(fileName);
        }

        [TestMethod]
        public void XmlSerializeTestClassWithUsageOfXmlAttribute_TestClassExists_FileExists()
        {
            // Arrange
            SimpleTestClassWithUsageOfXmlAttribute testClass = new SimpleTestClassWithUsageOfXmlAttribute()
            {
                HouseNumber = 4,
                StreetName = "Edlinger Str.",
                City = "Baierbach"
            };

            string fileName = this.applicationPath + "\\simpleTestClassWithXmlAttribute.xml";

            // Act
            XmlSerializer.Serialize(testClass, fileName);

            // Assert
            Assert.IsTrue(File.Exists(fileName), "The *.xml File was created");

            // CleanUp
            File.Delete(fileName);
        }

        [TestMethod]
        public void XmlSerializeTestClassWithUsageOfXmlRoot_TestClassExists_FileExists()
        {
            // Arrange
            SimpleTestClassWithUsageOfXmlRoot testClass = new SimpleTestClassWithUsageOfXmlRoot()
            {
                HouseNumber = 4,
                StreetName = "Edlinger Str.",
                City = "Baierbach"
            };

            string fileName = this.applicationPath + "\\simpleTestClassWithXmlAttribute.xml";

            // Act
            XmlSerializer.Serialize(testClass, fileName);

            // Assert
            Assert.IsTrue(File.Exists(fileName), "The *.xml File was created");

            // CleanUp
            File.Delete(fileName);
        }

        [TestMethod]
        public void XmlSerializeSimpleTestClassDirectory_TestClassExists_FileExists()
        {
            // Arrange
            SimpleTestClassDirectory testClassDirectory = new SimpleTestClassDirectory();
            testClassDirectory.TestClasses.Add(new SimpleTestClassWithoutAttributes()
            { HouseNumber = 4, StreetName = "Edlinger Str.", City = "Baierbarch" });
            testClassDirectory.TestClasses.Add(new SimpleTestClassWithoutAttributes()
            { HouseNumber = 5, StreetName = "Simssee Straße", City = "Stephanskirchen" });

            string fileName = this.applicationPath + "\\simpleTestClassDirectory.xml";

            // Act
            XmlSerializer.Serialize(testClassDirectory, fileName);

            // Assert
            Assert.IsTrue(File.Exists(fileName));

            // Cleanup
            File.Delete(fileName);

        }
    }
}
