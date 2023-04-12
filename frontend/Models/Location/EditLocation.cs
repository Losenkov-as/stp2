using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class EditLocation
    {
        [Required]
        public string Plot { get; set; }
        public string Workshop { get; set; }

        //[Required]
        public string User { get; set; }
        public EditLocation() { }

        public EditLocation(Location location)
        {
            Plot = location.Plot;
            Workshop = location.Workshop;
            User = location.User.Id;
        }
    }
}