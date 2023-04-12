namespace BlazorApp.Models
{
    public class Location
    {
        public string Id { get; set; }
        public string Plot { get; set; }
        public string Workshop { get; set; }
        public User User { get; set; }
        public bool IsDeleting { get; set; }
    }
}