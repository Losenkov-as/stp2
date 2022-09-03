using System.Collections.Generic;

namespace BlazorApp.Models
{
    public class Status
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Machine> Machines { get; set; } = new List<Machine>();
        public bool IsDeleting { get; set; }
    }
}