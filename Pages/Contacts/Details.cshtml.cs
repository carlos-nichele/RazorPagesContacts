using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesContacts.Data;
using RazorPagesContacts.Models;

namespace RazorPagesContacts.Pages.Contacts
{
    //[Authorize(Roles = "Administrator,SuperAdmin")]
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesContacts.Data.RazorPagesContactsContext _context;

        public DetailsModel(RazorPagesContacts.Data.RazorPagesContactsContext context)
        {
            _context = context;
        }

      public Contact Contact { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Contact == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            else 
            {
                Contact = contact;
            }
            return Page();
        }
    }
}
