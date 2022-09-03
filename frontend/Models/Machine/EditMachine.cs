using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class EditMachine
    {
        [Required]
        public string InventoryNumber { get; set; }

        public EditMachine(Machine machine)
        {
            InventoryNumber = machine.InventoryNumber;
        }
    }
}