using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Phonebook.Models;
using Phonebook.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Phonebook.Contollers.Api
{
    [Route("api/contacts")]
    public class ContactController : Controller
    {
        private IPhonebookRepository _repository;
        private ILogger<ContactController> _logger;

        public ContactController(IPhonebookRepository repository, ILogger<ContactController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{searchText?}")]
        public IActionResult GetContacts(string searchText = null)
        {
            try
            {
                var results = _repository.GetContactsBySearchText(searchText);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get contacts:", ex);
            }
            return BadRequest("Something went wrong with getting contacts");
        }

        [HttpGet("details")]
        public IActionResult GetContactDetails([FromUri]int contactId)
        {
            try
            {
                var currentContact = _repository.GetContactById(contactId);
                if(currentContact==null)
                    return NotFound("Contact with contactId = " + contactId + " is not found");
                else
                   return Ok(currentContact);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get contact details:", ex);
            }
            return BadRequest("Something went wrong with getting contact details");
        }

        [HttpDelete("")]
        public IActionResult DeleteContact([FromUri] int contactId)
        {
            try
            {
                var contactToBeDeleted = _repository.GetContactById(contactId);

                if (contactToBeDeleted == null)
                    return NotFound("Contact with contactId = " + contactId + " is not found");

                if (_repository.DeleteContact(contactId))
                    return Ok(contactToBeDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete contact:", ex);
            }
            return BadRequest("Something went wrong with deleting contact");
        }

        [HttpPost("")]
        public IActionResult AddContact([FromBody]ContactViewModel newContact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var contactModelStateErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(contactModelStateErrors);
                }

                if (_repository.AddContact(newContact))
                    return Created($"api/contacts/{newContact.ContactName}", newContact);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add contact:", ex);
            }
            return BadRequest("Something went wrong with adding contact");
        }

        [HttpPut("")]
        public IActionResult EditContact([FromBody]ContactViewModel updatedContact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var contactModelStateErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(contactModelStateErrors);
                }
                  
                if (_repository.UpdateContact(updatedContact))
                    return Ok(updatedContact);
                else
                    return NotFound("Contact with contactId = " + updatedContact.ContactId + " is not found");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to edit Contact:", ex);
            }
            return BadRequest("Something went wrong with editing contact");
        }
    }
}