using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class EditMaintenance
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime DateOfEnd { get; set; }
        [Required]
        public string User { get; set; }

        [Required]
        public string Comment { get; set; }

        public EditMaintenance(Maintenance maintenance)
        {
            DateOfEnd = maintenance.DateOfUpdate;
            User = maintenance.User.Id;
            Comment = maintenance.Comment;
        }
    }
}