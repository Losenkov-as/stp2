using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Roles
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }


    }
}