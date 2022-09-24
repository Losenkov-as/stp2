using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Users
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Patronymic { get; set; }
        
        [Required]
        public string Department { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public List<string> Roles { get; set; }

        [Required]
        public string Password { get; set; }
    }
}