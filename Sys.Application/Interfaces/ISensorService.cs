using Sys.Application.DTO;
using Sys.Domain.Entities.Sensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    public interface ISensorService : IBaseService<Sensor,int,SensorDto>
    {
        
    }
}
