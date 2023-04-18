using System;
using System.Collections.Generic;

namespace BlazorApp.Models
{
    public class AppHistory
    {
        public int Id { get; set; }
        public Maintenance Maintenance { get; set; }
        public bool IsDeleting { get; set; }
    }
}