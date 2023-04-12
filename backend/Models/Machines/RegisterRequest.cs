using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Machines
{
    public class RegisterRequest
    {
        [Required]
        public string InventoryNumber { get; set; }
        [Required]
        public string machinetype { get; set; }

        [Required]
        public string build { get; set; }
    }
}