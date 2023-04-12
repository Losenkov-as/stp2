using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Locations
{
    public class RegisterRequest
    {
        [Required]
        public string Plot { get; set; }

        [Required]
        public string Workshop { get; set; }
        
        [Required]
        public int User { get; set; }
    }
}