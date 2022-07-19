using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddUser
    {
        public AddUser() {
            Roles = new string[] { };
        }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        public string Username { get; set; }
        [Required, MinLength(1)]
        public string[] Roles { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The Password field must be a minimum of 6 characters")]
        public string Password { get; set; }
    }
}