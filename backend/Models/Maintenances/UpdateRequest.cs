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
        public DateTime DateOfEnd { get; set; }
        [Required]
        public int User { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string Comment { get; set; }
        public bool IsDeleting { get; set; }

    }
}