using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddRoom
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Build { get; set; }
    }
}