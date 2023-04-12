using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddMachine
    {
        [Required]
        public string InventoryNumber { get; set; }
        [Required]
        public string machinetype { get; set; }
        [Required]
        public string build { get; set; }
    }
}