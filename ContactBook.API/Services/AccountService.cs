using ContactBook.API.Models.Dto;
using ContactBook.API.Repositories;
using System;

namespace ContactBook.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository repository)
        {
            _accountRepository = repository;
        }

        public UserDto GetUser(string email, string pass)
        {
            var user = _accountRepository.GetUser(email, pass);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Incorrect email or password");
            }

            var userDto = new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Token = user.Token
            };

            return userDto;
        }
    }
}
