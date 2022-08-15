using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Room { get; set; }
        public string Build { get; set; }
        public User User { get; set; }

        public ICollection<Maintenance> Maintenances { get; set; }
    }
}
