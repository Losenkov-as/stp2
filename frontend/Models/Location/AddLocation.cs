using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddLocation
    {
        [Required]
        public string Room { get; set; }

        [Required]
        public string Build { get; set; }

        [Required]
        public int User { get; set; }

    }
}