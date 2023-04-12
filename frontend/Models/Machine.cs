
namespace BlazorApp.Models
{
    public class Machine
    {
        public string Id { get; set; }
        public string InventoryNumber { get; set; }
        public bool IsDeleting { get; set; }
        public Build build { get; set; }
        public MachineModel machinemodel { get; set; }
        public Room room { get; set; }
        public MachineType machinetype { get; set; }
    }
}