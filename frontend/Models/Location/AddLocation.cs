using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddLocation
    {
        [Required]
        public string Plot { get; set; }

        [Required]
        public string Workshop { get; set; }

        [Required]
        public int User { get; set; }

    }
}