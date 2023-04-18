using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Machine
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        
        public Room Room { get; set; }
        public MachineModel MachineModel { get; set; }

        [JsonIgnore]
        public List<Maintenance> Maintenances { get; set; }

    }
}
