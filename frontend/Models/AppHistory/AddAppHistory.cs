using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class AddAppHistory
    {

        [Required]
        public int maintenance { get; set; }

    }
}