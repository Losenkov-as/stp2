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
        public DateTime DateOfEnd { get; set; }
        [Required]
        public int User { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string Comment { get; set; }

        public EditMaintenance()
        {
            //DateOfUpdate = maintenance.DateOfUpdate;
            //Machine = maintenance.Machine;
            //Status = maintenance.Status;
            //Location = maintenance.Location;
            //User = maintenance.User;
        }
    }
}