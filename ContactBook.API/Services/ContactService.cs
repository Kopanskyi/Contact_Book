using ContactBook.API.Exceptions;
using ContactBook.API.Models;
using ContactBook.API.Models.Dto;
using ContactBook.API.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ContactBook.API.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository repository)
        {
            _contactRepository = repository;
        }

        public IEnumerable<ListContactDto> GetContacts()
        {
            return _contactRepository.GetContacts().Select(contact => new ListContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            });
        }

        public ContactDto GetContact(int id)
        {
            var contact = _contactRepository.GetContact(id);

            if (contact == null)
            {
                throw new ItemNotFoundException($"The contact with Id {id} is not found.");
            }

            var contactDto = new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email,
                Gender = contact.Gender,
                CountryId = contact.CountryId,
                City = contact.City,
                Address = contact.Address
            };

            return contactDto;
        }        

        public void UpdateContact(ContactDto contactDto)
        {
            var contact = new Contact
            {
                Id = contactDto.Id,
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                PhoneNumber = contactDto.PhoneNumber,
                Email = contactDto.Email,
                Gender = contactDto.Gender,
                CountryId = contactDto.CountryId,
                City = contactDto.City,
                Address = contactDto.Address
            };

            var updated = _contactRepository.UpdateContact(contact);

            if (!updated)
            {
                throw new ItemNotFoundException($"The contact with Id {contact.Id} is not found.");
            }
        }

        public int AddContact(ContactDto contactDto)
        {
            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                PhoneNumber = contactDto.PhoneNumber,
                Email = contactDto.Email,
                Gender = contactDto.Gender,
                CountryId = contactDto.CountryId,
                City = contactDto.City,
                Address = contactDto.Address
            };

            return _contactRepository.AddContact(contact);
        }

        public void DeleteContact(int id)
        {
            var deleted = _contactRepository.DeleteContact(id);

            if (!deleted)
            {
                throw new ItemNotFoundException($"The contact with Id {id} is not found.");
            }
        }
    }
}
