using ContactBook.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactBook.API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Dictionary<string, User> _users;

        public AccountRepository()
        {
            _users = new List<User>
            {
                new User
                {
                    FullName = "John",
                    Role = "admin",
                    Email = "admin@mail.com",
                    Password = "admin",
                    Token = "59d1d01e92a9e7b718c852aa4929a2b0020ae98b10403dba4e74f953457ae03d"
                },
                new User
                {
                    FullName = "Tom",
                    Role = "user",
                    Email = "user@mail.com",
                    Password = "user",
                    Token = "7456816cd435a7ed93478318476208e8d36f18667b18799fb69e8a5c56ccb19a"
                }
            }.ToDictionary(u => u.Token);
        }

        public User GetUser(string email, string pass)
        {            
            return _users.Values.FirstOrDefault(u => u.Email == email && u.Password == pass);
        }

        public User GetUser(string token)
        {
            return _users.Values.FirstOrDefault(u => u.Token == token);
        }
    }
}
