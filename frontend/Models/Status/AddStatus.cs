using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddStatus
    {
        [Required]
        public string Name { get; set; }

    }
}