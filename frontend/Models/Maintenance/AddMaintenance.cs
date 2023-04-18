using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.Account
{
    public class AddMaintenance
    {   
        public DateTime DateOfUpdate { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Machine { get; set; }

        [Required]
        public string Status { get; set; }
        [Required]
        public string Location { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public string TaskType { get; set; }

        //public AddMaintenance()
        //{
        //    //User = null;
        //    //Machine = null;
        //    //Status = null;
        //    //Location = null;
        //}
    }
}