using ContactBook.API.Exceptions;
using ContactBook.API.Models.Dto;
using ContactBook.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContactBook.API.Controllers
{
    [Route("api/contact")]
    public class СontactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IAuthService _authService;

        public СontactController(IContactService contactService, IAuthService authService)
        {
            _contactService = contactService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _contactService.GetContacts());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetContacts(int id)
        {
            return Ok(await _contactService.GetContact(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody] ContactDto contact)
        {
            if (_authService.CurrentUser.Role != Constants.userRoleAllowsEditing)
            {
                throw new AccessForbiddenException("Current user is not allowed to edit contacts.");
            }

            await _contactService.UpdateContact(contact);
            
            return Ok();
        }

        [HttpPost]
        public async Task<int> AddContact([FromBody] ContactDto contact)
        {
            if (_authService.CurrentUser.Role != Constants.userRoleAllowsEditing)
            {
                throw new AccessForbiddenException("Current user is not allowed to add new contacts.");
            }

            var newContactId = await _contactService.AddContact(contact);

            return newContactId;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_authService.CurrentUser.Role != Constants.userRoleAllowsEditing)
            {
                throw new AccessForbiddenException("Current user is not allowed to delete contacts.");
            }

            await _contactService.DeleteContact(id);

            return Ok();
        }
    }
}
