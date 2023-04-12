using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.MachineType
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

    }
}