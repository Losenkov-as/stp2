using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddMaintenance
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public DateTime DateOfUpdate { get; set; }
        [Required]
        public List<string> Users { get; set; }
        [Required]
        public List<string> Machines { get; set; }
        [Required]
        public List<string> Statuses { get; set; }
        [Required]
        public List<string> Locations { get; set; }

        public AddMaintenance()
        {
            Users = new List<string>();
            Machines = new List<string>();
            Statuses = new List<string>();
            Locations = new List<string>();
        }
    }
}