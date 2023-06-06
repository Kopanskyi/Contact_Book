using ContactBook.API.Models.Dto;
using ContactBook.API.Repositories;
using System;

namespace ContactBook.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountRepository _accountRepository;
        public UserDto CurrentUser { get; private set; }

        public AuthService(IAccountRepository repository)
        {
            _accountRepository = repository;
            CurrentUser = new UserDto();
        }

        public void SetCurrentUser(string token)
        {
            var user = _accountRepository.GetUser(token);

            if (user == null)
            {
                throw new UnauthorizedAccessException("User with this token is not found.");
            }

            CurrentUser = new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Token = user.Token
            };
        }
    }
}
