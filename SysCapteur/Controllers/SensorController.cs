using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sys.Application.DTO;
using Sys.Application.Interfaces;
using Sys.Domain.Entities.Sensor;

namespace SysCapteur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorById(int id)
        {
            var response = await _sensorService.GetByIdAsync(id);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Error); 
            }

            return Ok(response); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSensors()
        {
            var response = await _sensorService.GetAllAsync();

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Error); 
            }

            return Ok(response); 
        }

    
        [HttpPost]
        public async Task<IActionResult> CreateSensor([FromBody] SensorDto sensor)
        {
            if (sensor == null)
            {
                return BadRequest("Invalid sensor data.");
            }

            var response = await _sensorService.CreateAsync(sensor);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Error); 
            }

            return CreatedAtAction(nameof(GetSensorById), new { id = response.Data.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSensor(int id, [FromBody] SensorDto sensor)
        {
            if (sensor == null || sensor.Id != id)
            {
                return BadRequest("Sensor ID mismatch or invalid data.");
            }

            var response = await _sensorService.UpdateAsync(sensor);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Error); 
            }

            return Ok(response); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var response = await _sensorService.DeleteAsync(id);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Error); 
            }

            return NoContent(); 
        }
    }
}

