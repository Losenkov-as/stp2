using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class MachineModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MachineType MachineType { get; set; }
    }
}
