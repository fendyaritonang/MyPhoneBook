using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhoneBook.Models;
using System.Xml.Serialization;
using System.IO;

namespace MyPhoneBook.Classes
{
    public class PhoneBook
    {
        public List<Contact> EncryptedContacts { get; set; }

        [XmlIgnore]
        public List<Contact> Contacts { get; set; }

        public PhoneBook()
        {
            Contacts = new List<Contact>();
            EncryptedContacts = new List<Contact>();
        }

        /// <summary>
        /// Find index of Contacts List Item based on Contact Id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        private int ContactIndex(int contactId)
        {
            return Contacts.FindIndex(c => c.Id == contactId);
        }

        /// <summary>
        /// Find contact based on search keyword
        /// </summary>
        /// <param name="searchKeyword"></param>
        /// <returns></returns>
        public List<Contact> FindContact(string searchKeyword)
        {
            var result = Contacts.FindAll(item => 
                item.Name.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                item.Phone.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                item.Address.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0);

            return result;
        }

        /// <summary>
        /// Adding new list item to Contacts object
        /// </summary>
        /// <param name="contact"></param>
        public void AddContact(Contact contact)
        {
            int contactId = 1;
            if (Contacts.Count > 0)
            {
                contactId = Contacts.Max(c => c.Id) + 1;
            }
            contact.Id = contactId;
            Contacts.Add(contact);
            EncryptedContacts.Add(new Contact
            {
                Id = contactId,
                Name = Encryption.Encrypt(contact.Name),
                Phone = Encryption.Encrypt(contact.Phone),
                Address = Encryption.Encrypt(contact.Address)
            });
        }
        
        /// <summary>
        /// Modify existing list item based on Contact Id
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool ModifyContact(Contact contact)
        {
            var index = ContactIndex(contact.Id);

            if (index >= 0)
            {
                Contacts[index] = contact;
                EncryptedContacts[index] = new Contact
                {
                    Id = contact.Id,
                    Name = Encryption.Encrypt(contact.Name),
                    Phone = Encryption.Encrypt(contact.Phone),
                    Address = Encryption.Encrypt(contact.Address)
                };
                
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Delete existing list item based on Contact Id
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public bool DeleteContact(int contactId)
        {
            var index = ContactIndex(contactId);

            if (index >= 0)
            {
                Contacts.RemoveAt(index);
                EncryptedContacts.RemoveAt(index);                
                return true;
            }
            else
                return false;
        }
    }
}
