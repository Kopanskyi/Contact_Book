using ContactBook.API.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactBook.API.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ListContactDto>> GetContacts();
        Task<ContactDto> GetContact(int id);
        Task UpdateContact(ContactDto contactDto);
        Task<int> AddContact(ContactDto contactDto);
        Task DeleteContact(int id);
    }
}
