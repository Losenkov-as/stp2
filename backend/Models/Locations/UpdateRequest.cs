using WebApi.Entities;

namespace WebApi.Models.Locations
{
    public class UpdateRequest
    {
        public string Plot { get; set; }
        public string Workshop { get; set; }
        public int User { get; set; }
    }
}