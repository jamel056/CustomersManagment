using CustomersManagment.Models;
using CustomersManagment.Requests;
using CustomersManagment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomersManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<List<Contact>> GetAll() =>
            _contactService.GetAll();

        [HttpGet("{id:length(24)}", Name = "Getcontact")]
        public ActionResult<Contact> Get(int id)
        {
            var contact = _contactService.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        [HttpPost]
        public ActionResult<Contact> Create(ContactRequest request)
        {
            var response = _contactService.Create(request);
            if (response == null) return Ok("the same contact is already found");

            return Ok(response);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(int id, ContactRequest request)
        {
            var contactFromDb = _contactService.Get(id);

            if (contactFromDb == null)
            {
                return NotFound();
            }

            _contactService.Update(id, request);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(int id)
        {
            var contact = _contactService.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            _contactService.Remove(contact.Id);

            return NoContent();
        }

        [HttpPost]
        [Route("AddField")]
        public void AddField([FromQuery]string fieldName)
        {
            _contactService.AddField(fieldName);
        }

        [HttpPost]
        [Route("Filter")]
        public ActionResult Filter([FromBody]List<FilterRequest> request)
        {
            var response = _contactService.Filter(request);

            return Ok(response);
        }
    }
}
