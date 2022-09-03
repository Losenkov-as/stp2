using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class EditStatus
    {
        [Required]
        public string Name { get; set; }

        public EditStatus(Status status)
        {
            Name = status.Name;
        }
    }
}