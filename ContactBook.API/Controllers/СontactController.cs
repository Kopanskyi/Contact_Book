using ContactBook.API.Exceptions;
using ContactBook.API.Models.Dto;
using ContactBook.API.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetContacts()
        {
            return Ok(_contactService.GetContacts());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetContacts(int id)
        {
            return Ok(_contactService.GetContact(id));
        }

        [HttpPut]
        public IActionResult UpdateContact([FromBody] ContactDto contact)
        {
            if (_authService.CurrentUser.Role != Constants.userRoleAllowsEditing)
            {
                throw new AccessForbiddenException("Current user is not allowed to edit contacts.");
            }

            _contactService.UpdateContact(contact);
            
            return Ok();
        }

        [HttpPost]
        public int AddContact([FromBody] ContactDto contact)
        {
            if (_authService.CurrentUser.Role != Constants.userRoleAllowsEditing)
            {
                throw new AccessForbiddenException("Current user is not allowed to add new contacts.");
            }

            var newContactId = _contactService.AddContact(contact);

            return newContactId;
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteContact(int id)
        {
            if (_authService.CurrentUser.Role != Constants.userRoleAllowsEditing)
            {
                throw new AccessForbiddenException("Current user is not allowed to delete contacts.");
            }

            _contactService.DeleteContact(id);

            return Ok();
        }
    }
}
