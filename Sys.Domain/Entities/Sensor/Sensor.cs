using Sys.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Entities.Sensor
{
    public class Sensor : Entity<int>
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
