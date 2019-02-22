// <copyright file="XmlSerializerTests.cs" company="Sigi Maier">
// No copyright
// </copyright>

namespace Base.Tests.FileHandling
{
    using System;
    using System.IO;
    using Basics.FileHandling;
    using Base.Tests.FileHandling.TestObjects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// TestClass for <see cref="XmlSerializer"/>.
    /// </summary>
    [TestClass]
    public class XmlSerializerTests
    {
        private readonly string applicationPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> <seealso cref="SimpleTestClassWithoutAttributes"/> instance,
        /// <i>when</i> serializing the object,
        /// <i>then</i> the File is created.
        /// </summary>
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

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> <see cref="SimpleTestClassWithUsageOfXmlElement"/> instance,
        /// <i>when</i> serializing the object,
        /// <i>then</i> the File is created.
        /// </summary>
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

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> <see cref="SimpleTestClassWithUsageOfXmlAttribute"/> instance,
        /// <i>when</i> serializing the object,
        /// <i>then</i> the file is created.
        /// </summary>
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

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> <see cref="SimpleTestClassWithUsageOfXmlRoot"/> instance,
        /// <i>when</i> serializing the object,
        /// <i>then</i> the file is created.
        /// </summary>
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

        /// <summary>
        /// Scenario under test:
        /// <i>Given</i> <see cref="SimpleTestClassDirectory"/> instance,
        /// <i>when</i> serializing the object,
        /// <i>then</i> the file is created.
        /// </summary>
        [TestMethod]
        public void XmlSerializeSimpleTestClassDirectory_TestClassExists_FileExists()
        {
            // Arrange
            SimpleTestClassDirectory testClassDirectory = new SimpleTestClassDirectory
            {
                TestClasses = new System.Collections.Generic.List<SimpleTestClassWithoutAttributes>
            {
                new SimpleTestClassWithoutAttributes()
                { HouseNumber = 4, StreetName = "Edlinger Str.", City = "Baierbarch" },
                new SimpleTestClassWithoutAttributes()
                { HouseNumber = 5, StreetName = "Simssee Straße", City = "Stephanskirchen" }
            }
            };

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
