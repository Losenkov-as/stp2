using System;
using System.Collections.Generic;

namespace BlazorApp.Models
{
    public class Maintenance
    {
        public string Id { get; set; }
        public DateTime DateOfUpdate { get; set; }
        public User User { get; set; } 
        public Machine Machine { get; set; }
        public Status Status { get; set; }
        public Location Location { get; set; } 
        public bool IsDeleting { get; set; }
    }
}