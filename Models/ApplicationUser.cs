using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Finances.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string? FirstName { get; set; } = string.Empty;

        
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        public string? LastName { get; set; } = string.Empty;

        
        [StringLength(100, ErrorMessage = "City cannot be longer than 100 characters")]
        public string? City { get; set; } = string.Empty;

       
        [StringLength(100, ErrorMessage = "Country cannot be longer than 100 characters")]
        public string? Country { get; set; } = string.Empty;

        
        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(15, ErrorMessage = "Telephone number cannot be longer than 15 characters")]
        public string? TelephoneNumber { get; set; } = string.Empty;

    }
}
