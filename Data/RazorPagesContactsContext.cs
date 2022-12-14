using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using RazorPagesContacts.Models;

namespace RazorPagesContacts.Data
{
    public class RazorPagesContactsContext : DbContext
    {
        public RazorPagesContactsContext (DbContextOptions<RazorPagesContactsContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesContacts.Models.Contact> Contact { get; set; } = default!;
    }

}
