using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Statuses
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

    }
}