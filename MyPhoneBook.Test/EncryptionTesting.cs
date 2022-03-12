using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyPhoneBook.Classes;

namespace MyPhoneBook.Test
{
    [TestClass]
    public class EncryptionTesting
    {
        private string decryptedWord = "HelloWorld";
        private string encryptedWord = "DqBil7zLwOPUqmg3/L4cFKXIqqlxlxlvS8R2x/80fvY=";

        [TestMethod]
        public void Encrypt_HelloWorld()
        {
            // arrange
            var testedWord = decryptedWord;
            var expectedResult = encryptedWord;

            // act
            var result = Encryption.Encrypt(testedWord);

            // assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Decrypt_HelloWorld()
        {
            // arrange
            var testedWord = encryptedWord;
            var expectedResult = decryptedWord;

            // act
            var result = Encryption.Decrypt(testedWord);

            // assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
