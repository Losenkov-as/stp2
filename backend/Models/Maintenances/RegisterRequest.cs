using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Maintenance
{
    public class RegisterRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public DateTime DateOfUpdate { get; set; }
        [Required]
        public string Machine { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Location { get; set; }
        public bool IsDeleting { get; set; }

    }
}