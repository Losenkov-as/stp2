using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class AddMachineType
    {
        [Required]
        public string Name { get; set; }

    }
}