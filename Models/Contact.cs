using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPagesContacts.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RazorPagesContacts.Models
{
    
    public class Contact
    {
        public int Id { get; set; }
        [StringLength(60,MinimumLength = 5)]
        public string? Name { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Please only digits !!!!")]
        [StringLength(9, MinimumLength = 9)]
        [DisplayName("Contact")]
        [Required]
        public string? Ncontact { get; set; }
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",ErrorMessage = "Please enter a valid e-mail !!!!")]
        [DisplayName("E-Mail")]
        [Required]
        public string Email { get; set; }


    }
        
}
