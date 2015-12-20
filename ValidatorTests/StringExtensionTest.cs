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
            string b = "54321";

            //Act
            string c = a.Reverse();

            // Assert
            Assert.IsTrue(b.Length == c.Length);
            Assert.IsTrue(string.Compare(b, c) == 0);
        }
    }
}
