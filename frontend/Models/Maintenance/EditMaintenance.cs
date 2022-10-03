using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class EditMaintenance
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public DateTime DateOfUpdate { get; set; }
        [Required]
        public Machine Machine { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public Location Location { get; set; }
        [Required]
        public User User { get; set; }

        public EditMaintenance(Maintenance maintenance)
        {
            //DateOfUpdate = maintenance.DateOfUpdate;
            //Machine = maintenance.Machine;
            //Status = maintenance.Status;
            //Location = maintenance.Location;
            //User = maintenance.User;
        }
    }
}