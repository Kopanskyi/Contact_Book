using ContactBook.API.Models.Dto;
using System.Collections.Generic;

namespace ContactBook.API.Services
{
    public interface IContactService
    {
        IEnumerable<ListContactDto> GetContacts();
        ContactDto GetContact(int id);
        void UpdateContact(ContactDto contactDto);
        int AddContact(ContactDto contactDto);
        void DeleteContact(int id);
    }
}
