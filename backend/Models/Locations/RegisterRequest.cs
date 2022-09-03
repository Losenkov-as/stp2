using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Locations
{
    public class RegisterRequest
    {
        [Required]
        public string Room { get; set; }

        [Required]
        public string Build { get; set; }

    }
}