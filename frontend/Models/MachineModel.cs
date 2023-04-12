
namespace BlazorApp.Models
{
    public class MachineModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public MachineType machinetype { get; set;}
        public bool IsDeleting { get; set; }
    }
}