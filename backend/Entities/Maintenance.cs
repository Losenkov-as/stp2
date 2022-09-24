using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Maintenance
    {
        public int Id { get; set; }
        public DateTime DateOfUpdate { get; set; }
        public Machine Machine { get; set; }
        public Status Status { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }

    }
}
