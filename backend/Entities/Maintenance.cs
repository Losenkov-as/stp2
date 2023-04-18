using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Maintenance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public DateTime DateOfUpdate { get; set; }
        public Machine Machine { get; set; }
        public Status Status { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
        public string Comment { get; set; }
        public TaskType TaskType { get; set; }


    }
}
