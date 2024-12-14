using Microsoft.EntityFrameworkCore;
using Sys.Domain.Entities.Sensor;
using Sys.Presistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.Repository
{
    public class SensorRepository : BaseRepository<Sensor,int>, ISensorRepository
    {
        public SensorRepository(ApplicationDbContext context) : base(context) { }

    }
}
