using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddMachine
    {
        [Required]
        public string InventoryNumber { get; set; }

    }
}