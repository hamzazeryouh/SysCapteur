using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.DTO.Auth
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
    }
}
