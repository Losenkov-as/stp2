namespace BlazorApp.Models
{
    public class Location
    {
        public string Id { get; set; }
        public string Room { get; set; }
        public string Build { get; set; }
        public User User { get; set; }
        public bool IsDeleting { get; set; }
    }
}