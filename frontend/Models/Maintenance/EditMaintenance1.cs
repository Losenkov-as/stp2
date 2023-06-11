using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class EditMaintenance1
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Comment { get; set; }

        public EditMaintenance1(Maintenance maintenance)
        {

            User = maintenance.User.Id;
            Comment = maintenance.Comment;
        }
    }
}