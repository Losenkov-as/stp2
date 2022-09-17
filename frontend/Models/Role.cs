using System.Collections.Generic;

namespace BlazorApp.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public bool IsDeleting { get; set; }
    }
}