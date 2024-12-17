using AutoMapper;
using Sys.Application.DTO;
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
    public class SensorService : BaseService<Sensor, int, SensorDto>, ISensorService
    {
        public SensorService(IUnitOfWork unitOfWork , IMapper mapper) : base(unitOfWork, unitOfWork.GetRepository<Sensor, int>(), mapper) { }
    }

}
