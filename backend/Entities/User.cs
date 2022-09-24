using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Patronymic { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
        public List<Role> Roles { get; set; }
        [JsonIgnore]
        public List<Maintenance> Maintenances { get; set; }


    }
}