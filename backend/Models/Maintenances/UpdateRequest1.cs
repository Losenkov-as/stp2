using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Maintenance
{
    public class UpdateRequest1
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Comment { get; set; }
        public bool IsDeleting { get; set; }

    }
}