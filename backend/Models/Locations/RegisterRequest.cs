using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Locations
{
    public class RegisterRequest
    {
        [Required]
        public string Room { get; set; }

        [Required]
        public string Build { get; set; }
        
        [Required]
        public int User { get; set; }
    }
}