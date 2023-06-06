using ContactBook.API.Models;

namespace ContactBook.API.Repositories
{
    public interface IAccountRepository
    {
        User GetUser(string email, string pass);
        User GetUser(string token);
    }
}
