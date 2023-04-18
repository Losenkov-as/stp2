using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class AddMachineModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string MachineType { get; set; }
    }
}