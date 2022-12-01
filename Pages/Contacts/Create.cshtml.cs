using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesContacts.Data;
using RazorPagesContacts.Models;

namespace RazorPagesContacts.Pages.Contacts
{
    //[Authorize(Roles = "Administrator,SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly RazorPagesContacts.Data.RazorPagesContactsContext _context;

        public CreateModel(RazorPagesContacts.Data.RazorPagesContactsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("SuperAdmin")))
            {
                ModelState.AddModelError("", "User hasn't permissions to use this page !!!!");
            }
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            if(_emailValidation.Count() > 0)
            {
                ModelState.AddModelError("", "E-Mail already exists !!!!");
                return Page();
            }
                        
            if (!ModelState.IsValid || _context.Contact == null || Contact == null)
            {
                return Page();
            }

            _context.Contact.Add(Contact);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
