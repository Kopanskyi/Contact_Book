using ContactBook.API.Exceptions;
using ContactBook.API.Models;
using ContactBook.API.Models.Dto;
using ContactBook.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBook.API.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository repository)
        {
            _contactRepository = repository;
        }

        public async Task<IEnumerable<ListContactDto>> GetContacts()
        {
            var contacts = await _contactRepository.GetAllAsync();

            return contacts.Select(contact => new ListContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName
            });
        }

        public async Task<ContactDto> GetContact(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id); // GetContact(id);

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

        public async Task UpdateContact(ContactDto contactDto)
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

            try
            {
                _contactRepository.Update(contact); //.UpdateContact(contact);
                await _contactRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<int> AddContact(ContactDto contactDto)
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

            try
            {
                var result = _contactRepository.Add(contact); //.AddContact(contact);
                await _contactRepository.SaveChangesAsync();

                return result.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteContact(int id)
        {
            try
            {
                await _contactRepository.SoftRemove(id); //.DeleteContact(id);
                await _contactRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
