using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class EditLocation
    {
        [Required]
        public string Room { get; set; }
        public string Build { get; set; }

        //[Required]
        public string User { get; set; }
        public EditLocation() { }

        public EditLocation(Location location)
        {
            Room = location.Room;
            Build = location.Build;
            User = location.User.Id;
        }
    }
}