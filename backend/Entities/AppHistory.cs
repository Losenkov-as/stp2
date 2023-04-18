using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class AppHistory
    {
        public int Id { get; set; }
        public Maintenance maintenance { get; set; }
        public bool IsDeleting { get; set; }
    }
}