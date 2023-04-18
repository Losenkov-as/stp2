using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class AddTaskType
    {
        [Required]
        public string Name { get; set; }

    }
}