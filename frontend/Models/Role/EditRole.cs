using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class EditRole
    {
        [Required]
        public string Name { get; set; }

        public EditRole() { }

        public EditRole(Role role)
        {
            Name = role.Name;
        }
    }
}