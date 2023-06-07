using ContactBook.API.Models.Dto;
using ContactBook.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.API.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService service)
        {
            _accountService = service;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LogIn([FromBody] LoginDto loginDto)
        {
            var userDto = _accountService.GetUser(loginDto.Email, loginDto.Password);
            return Ok(userDto);
        }
    }
}
