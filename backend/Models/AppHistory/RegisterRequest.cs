using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.AppHistory
{
    public class RegisterRequest
    {
        [Required]
        public int maintenance { get; set; }
    }
}