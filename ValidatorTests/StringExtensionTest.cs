using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatorUtil;

namespace ValidatorTests
{
    [TestClass]
    public class StringExtensionTest
    {
        [TestMethod]
        public void RemoveExtraWhiteSpace_Success()
        {
            // Arrange
            string a = "12345                 1111111111 21";
            string b = "12345 1111111111 21";

            //Act
            string c = a.RemoveExtraWhiteSpace();

            // Assert
            Assert.IsTrue(b.Length == c.Length);
            Assert.IsTrue(string.Compare(b, c) == 0);
        }

        [TestMethod]
        public void Reverse_Success()
        {
            // Arrange
            string a = "12345";

            //Act // Assert
            Assert.IsTrue(string.Compare(a.Reverse().Reverse(), a) == 0);
        }


        [TestMethod]
        public void Reverse_Empty_Success()
        {
            // Arrange
            string a = "";

            //Act // Assert
            Assert.IsTrue(a.Reverse() == string.Empty);
        }
    }
}
