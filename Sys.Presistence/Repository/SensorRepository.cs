using Microsoft.EntityFrameworkCore;
using Sys.Domain.Entities.Sensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.Repository
{
    public class SensorRepository : BaseRepository<Sensor,int>, ISensorRepository
    {
        public SensorRepository(DbContext context) : base(context) { }

    }
}
