using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class CancelMaintenance 
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Comment { get; set; }

        public CancelMaintenance(Maintenance maintenance)
        {

            User = maintenance.User.Id;
            Comment = maintenance.Comment;
        }
    }
}