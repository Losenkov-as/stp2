using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Machines
{
    public class RegisterRequest
    {
        [Required]
        public string InventoryNumber { get; set; }
        [Required]
        public string machinemodel { get; set; }

        [Required]
        public string room { get; set; }
    }
}