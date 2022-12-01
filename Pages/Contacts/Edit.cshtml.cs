using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesContacts.Data;
using RazorPagesContacts.Models;

namespace RazorPagesContacts.Pages.Contacts
{
    //[Authorize(Roles = "Administrator,SuperAdmin")]
    public class EditModel : PageModel
    {
        private readonly RazorPagesContacts.Data.RazorPagesContactsContext _context;

        public EditModel(RazorPagesContacts.Data.RazorPagesContactsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact =  await _context.Contact.FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            Contact = contact;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            // Ensures only a contact is recorded
            var _contactValidation = _context.Contact.Where(e => e.Ncontact.Equals(this.Contact.Ncontact));

            if (_contactValidation.Count() > 0)
            {
                ModelState.AddModelError("", "Contact already exists !!!!");
                return Page();
            }

            // Ensures only an email is recorded
            var _emailValidation = _context.Contact.Where(e => e.Email.Equals(this.Contact.Email));

            if (_emailValidation.Count() > 0)
            {
                ModelState.AddModelError("", "E-Mail already exists !!!!");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(Contact.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContactExists(int id)
        {
          return (_context.Contact?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
