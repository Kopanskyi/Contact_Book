using ContactBook.API.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactBook.API.Services
{
    public interface IAuthService
    {
        UserDto CurrentUser { get; }

        void SetCurrentUser(string token);
    }
}
