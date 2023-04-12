using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.MachineModel
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

        public int machinetype { get; set; } 
    }
}