using Sys.Application.Interfaces;
using Sys.Domain.Entities.Sensor;
using Sys.Presistence.DataAccess;
using Sys.Presistence.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.Services
{
    public class SensorService : BaseService<Sensor, int>, ISensorService
    {
        public SensorService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.GetRepository<Sensor, int>()) { }
    }

}
