using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Room { get; set; }
        public string Build { get; set; }                   //мб лучше так: public ICollection<User> Users { get; set; } 
        [JsonIgnore]
        public List<Maintenance> Maintenances { get; set; }
    }
}
