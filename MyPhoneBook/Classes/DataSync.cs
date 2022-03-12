using MyPhoneBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyPhoneBook.Classes
{
    public class DataSync
    {
        private string _dataPath;

        public DataSync(string dataPath)
        {
            _dataPath = dataPath;
        }

        public PhoneBook LoadSavedPhoneBook
        {
            get
            {
                var phoneBook = new PhoneBook();
                if (File.Exists(_dataPath))
                {
                    try
                    {
                        using (var reader = new StreamReader(_dataPath))
                        {
                            var serializer = new XmlSerializer(phoneBook.GetType());
                            phoneBook = (PhoneBook)serializer.Deserialize(reader);

                            if (phoneBook.EncryptedContacts.Count > 0)
                            {
                                var decryptedContacts = phoneBook.EncryptedContacts.Select(x => new Contact
                                {
                                    Id = x.Id,
                                    Name = Encryption.Decrypt(x.Name),
                                    Phone = Encryption.Decrypt(x.Phone),
                                    Address = Encryption.Decrypt(x.Address)
                                }).ToList();
                                phoneBook.Contacts = new List<Contact>(decryptedContacts);
                            }

                            reader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"LoadSavedPhoneBook - {ex.Message}");
                    }
                }
                else
                {
                    phoneBook = new PhoneBook();
                }

                return phoneBook;
            }
        }

        public void SavePhoneBook(PhoneBook phoneBook)
        {
            XmlSerializer serializer = new XmlSerializer(phoneBook.GetType());

            try
            {
                using (StreamWriter writer = new StreamWriter(_dataPath))
                {
                    serializer.Serialize(writer, phoneBook);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"SavePhoneBook - {ex.Message}");
            }
        }
    }
}
