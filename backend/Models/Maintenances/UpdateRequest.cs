using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Maintenance
{
    public class UpdateRequest
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
        public User User { get; set; }
        [Required]
        public Location Location { get; set; }
        public bool IsDeleting { get; set; }

    }
}