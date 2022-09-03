using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Machines
{
    public class RegisterRequest
    {
        [Required]
        public string InventoryNumber { get; set; }

    }
}