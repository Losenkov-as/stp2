using WebApi.Entities;

namespace WebApi.Models.Locations
{
    public class UpdateRequest
    {
        public string Room { get; set; }
        public string Build { get; set; }
        public int User { get; set; }
    }
}