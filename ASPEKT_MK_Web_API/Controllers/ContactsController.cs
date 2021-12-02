using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPEKT_MK_Web_API.Models;
using ASPEKT_MK_Web_API.Models.DTO;

namespace ASPEKT_MK_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContact()
        {
            return await _context.Contact.Select(x => ContactToDTO(x)).ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetContact(int id)
        {
            var contact = await _context.Contact.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return ContactToDTO(contact);
        }

        // PUT: api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, ContactDTO contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(ContactDTO contactDTO)
        {
            var contact = new Contact
            {
                Id = contactDTO.Id,
                Name = contactDTO.Name,
                CountryId = contactDTO.CountryId,
                CompanyId = contactDTO.CompanyId
            };

            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, ContactToDTO(contact));
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }

        private static ContactDTO ContactToDTO(Contact contact) =>
            new ContactDTO
            {
                Id = contact.Id,
                Name = contact.Name,
                CompanyId = contact.CompanyId,
                CountryId = contact.CountryId
            };

        // GET: api/Contacts/Country/2
        [HttpGet("Country/{id}")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactsByCountry(int id)
        {
            var contact = await _context.Contact.Where<Contact>(x => x.CountryId == id).Select(x => ContactToDTO(x)).ToArrayAsync();

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }



        // GET: api/Contacts/Country/2
        [HttpGet("Company/{id}")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetContactsByCompany(int id)
        {
            var contact = await _context.Contact.Where<Contact>(x => x.CountryId == id).Select(x => ContactToDTO(x)).ToArrayAsync();

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }


        // GET: api/Contacts/Country&Country/2
        [HttpGet("Country&Company/{countryId}/{companyId}")]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> FilterContacts(int countryId, int companyId)
        {
            var contact = await _context.Contact.Where<Contact>(x => (x.CompanyId == companyId) && (x.CountryId == countryId)).Select(x => ContactToDTO(x)).ToArrayAsync();

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }


    }
}
