using ContactBook.API.Models;
using System.Collections.Generic;

namespace ContactBook.API.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
        bool UpdateContact(Contact contact);
        int AddContact(Contact contact);
        bool DeleteContact(int id);
    }
}
