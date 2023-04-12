using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Builds
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

    }
}