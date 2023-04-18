using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class AddBuild
    {

        [Required]
        public string Name { get; set; }

    }
}