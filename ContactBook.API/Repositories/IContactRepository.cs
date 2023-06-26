using ContactBook.API.Models;
using ContactBook.API.Repositories.BaseRepositories;

namespace ContactBook.API.Repositories
{
    public interface IContactRepository : IRepository<Contact, int>
    {
        //IEnumerable<Contact> GetContacts();
        //Contact GetContact(int id);
        //bool UpdateContact(Contact contact);
        //int AddContact(Contact contact);
        //bool DeleteContact(int id);
    }
}
