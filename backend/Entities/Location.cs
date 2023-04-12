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
        public string Plot { get; set; }
        public string Workshop { get; set; }                   //мб лучше так: public ICollection<User> Users { get; set; } 
        public User User { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public List<Maintenance> Maintenances { get; set; }
    }
}
