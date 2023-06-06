using ContactBook.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactBook.API.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly Dictionary<int, Contact> _contacts;
        private int _maxId = 0;

        private int NewId => ++_maxId;

        public ContactRepository()
        {
            _contacts = new List<Contact>
            { 
                new Contact
                {
                    Id = NewId,
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1111111111",
                    Email = "doe@mail.com",
                    Gender = "Male",
                    CountryId = 4,
                    City = "London",
                    Address = "No. 64, 25 Laleh Street 4th Floor",
                    UpdatedAt = DateTime.Now
                },
                new Contact
                {
                    Id = NewId,
                    FirstName = "Jane",
                    LastName = "Doe",
                    PhoneNumber = "33333333333",
                    Email = "jdoe@mail.com",
                    Gender = "Female",
                    CountryId = 2,
                    City = "Berlin",
                    Address = "No. 28, 2 Laleh Street 3th Floor",
                    UpdatedAt = DateTime.Now
                },
                new Contact
                {
                    Id = NewId,
                    FirstName = "Jane",
                    LastName = "Foster",
                    PhoneNumber = "2222222222222",
                    Email = "foster@mail.com",
                    Gender = "Female",
                    CountryId = 3,
                    City = "Rome",
                    Address = "No. 4, 15 Italy Street 2th Floor",
                    UpdatedAt = DateTime.Now
                }
            }.ToDictionary(k => k.Id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _contacts.Values;
        }

        public Contact GetContact(int id)
        {
            _contacts.TryGetValue(id, out var contact);

            return contact;
        }

        public bool UpdateContact(Contact contact)
        {
            if (!_contacts.TryGetValue(contact.Id, out var existingContact))
            {
                return false;
            }

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.Email = contact.Email;
            existingContact.Gender = contact.Gender;
            existingContact.CountryId = contact.CountryId;
            existingContact.City = contact.City;
            existingContact.Address = contact.Address;
            existingContact.UpdatedAt = DateTime.Now;

            return true;
        }

        public int AddContact(Contact contact)
        {
            contact.Id = NewId;
            contact.UpdatedAt = DateTime.Now;

            _contacts.Add(contact.Id, contact);

            return contact.Id;
        }

        public bool DeleteContact(int id)
        {
            if (!_contacts.ContainsKey(id))
            {
                return false;
            }

            _contacts.Remove(id);

            return true;
        }
    }
}
