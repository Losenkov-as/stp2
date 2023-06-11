using System.Collections.Generic;

namespace BlazorApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Patronymic { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }

        public static string current;
        public string Token { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public bool IsDeleting { get; set; }
    }
}