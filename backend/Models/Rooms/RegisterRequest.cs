using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Rooms
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Build { get; set; }
    }
}