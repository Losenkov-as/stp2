using System.Collections.Generic;

namespace BlazorApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public List<Role> roles { get; set; } = new List<Role>();
        public bool IsDeleting { get; set; }
    }
}