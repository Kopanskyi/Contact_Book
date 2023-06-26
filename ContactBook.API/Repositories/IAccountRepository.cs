using ContactBook.API.Models;
using ContactBook.API.Repositories.BaseRepositories;

namespace ContactBook.API.Repositories
{
    public interface IAccountRepository : IRepository<User, int>
    {
        User GetUser(string email, string pass);
        User GetUser(string token);
    }
}
