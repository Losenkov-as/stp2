using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.TaskType
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

    }
}