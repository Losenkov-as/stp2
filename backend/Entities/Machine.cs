using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Machine
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }

        public ICollection<Maintenance> Maintenances { get; set; }

    }
}
