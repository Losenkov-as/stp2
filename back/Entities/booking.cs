using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class booking
    {
        public int Id { get; set; }
        public int number { get; set; }
        public string status { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        public User user { get; set; }
        public int UserId { get; set; }
    }
}
