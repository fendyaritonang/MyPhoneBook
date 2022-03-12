using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyPhoneBook.Classes;
using MyPhoneBook.Models;
using System;
using System.IO;

namespace MyPhoneBook.Test
{
    [TestClass]
    public class PhoneBookTesting
    {
        private static string _phoneBookPath => "phonebook.testing.xml";
        private DataSync _dataSync = new DataSync(_phoneBookPath);
        private static Contact _contactJohnDoe = new Contact
        {
            Id = 0,
            Name = "John Doe",
            Phone = "88889999",
            Address = "Singapore"
        };
        private static Contact _contactMary = new Contact
        {
            Id = 0,
            Name = "Mary",
            Phone = "22223333",
            Address = "Canada"
        };
        private static Contact _contactJane = new Contact
        {
            Id = 0,
            Name = "Jane",
            Phone = "123123123",
            Address = "Japan"
        };
        private static Contact _contactHugo = new Contact
        {
            Id = 0,
            Name = "Hugo",
            Phone = "22322323",
            Address = "Spain"
        };

        public PhoneBookTesting()
        {
            if (File.Exists(_phoneBookPath))
                File.Delete(_phoneBookPath);

            var phoneBook = _dataSync.LoadSavedPhoneBook;
            phoneBook.AddContact(_contactMary);
            phoneBook.AddContact(_contactJane);
            phoneBook.AddContact(_contactHugo);
            _dataSync.SavePhoneBook(phoneBook);
        }

        [TestMethod]
        public void CreateNewContact_JohnDoe()
        {
            // arrange                                    
            var phoneBook = _dataSync.LoadSavedPhoneBook;            

            // act
            phoneBook.AddContact(_contactJohnDoe);
            _dataSync.SavePhoneBook(phoneBook);
            var phoneBookResult = _dataSync.LoadSavedPhoneBook;
            var result = phoneBookResult.Contacts.FindAll(x => x.Name == _contactJohnDoe.Name && x.Phone == _contactJohnDoe.Phone && x.Address == _contactJohnDoe.Address);

            // assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void ModifyContact_Mary()
        {
            // arrange
            var phoneBook = _dataSync.LoadSavedPhoneBook;
            var existingRecord = phoneBook.Contacts.FindLast(x => x.Name == _contactMary.Name && x.Phone == _contactMary.Phone && x.Address == _contactMary.Address);
            var maryContactId = existingRecord.Id;
            var modifiedMary = new Contact
            {
                Id = maryContactId,
                Name = _contactMary.Name + " (modified)",
                Phone = _contactMary.Phone + " (modified)",
                Address = _contactMary.Address + " (modified)"
            };

            // act
            phoneBook.ModifyContact(modifiedMary);
            _dataSync.SavePhoneBook(phoneBook);
            var phoneBookResult = _dataSync.LoadSavedPhoneBook;
            var result = phoneBookResult.Contacts.FindAll(x => x.Name == modifiedMary.Name && x.Phone == modifiedMary.Phone && x.Address == modifiedMary.Address);

            // assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void FindContact_Jane()
        {
            // arrange
            var phoneBook = _dataSync.LoadSavedPhoneBook;

            // act
            var result = phoneBook.FindContact(_contactJane.Name);            

            // assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void DeleteContact_Hugo()
        {
            // arrange
            var phoneBook = _dataSync.LoadSavedPhoneBook;
            var deleteSuccessful = false;

            // act
            var searchResult = phoneBook.FindContact(_contactHugo.Name);
            if (searchResult.Count == 1)
            {
                phoneBook.DeleteContact(searchResult[0].Id);
                _dataSync.SavePhoneBook(phoneBook);

                var phoneBookResult = _dataSync.LoadSavedPhoneBook;
                var result = phoneBookResult.FindContact(_contactHugo.Name);

                if (result.Count == 0)
                    deleteSuccessful = true;
            }

            // assert
            Assert.AreEqual(true, deleteSuccessful);
        }
    }
}
