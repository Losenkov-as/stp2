using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddMachine
    {
        [Required]
        public string InventoryNumber { get; set; }
        [Required]
        public string machinemodel { get; set; }
        [Required]
        public string room { get; set; }

    }
}