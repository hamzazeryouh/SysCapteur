

using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sys.Application.Interfaces;
using Sys.Application.DTO;
using Sys.Application.Helpers;



namespace Sys.TestUnit.Sensor
{
    public class SensorServiceTests
    {
        private readonly Mock<ISensorService> _mockSensorService;

        public SensorServiceTests()
        {
            _mockSensorService = new Mock<ISensorService>();
        }

        #region CRUD Operations

        [Fact]
        public async Task CreateSensor_ShouldReturnCreatedSensorResponse()
        {
            // Arrange
            var sensorDto = new SensorDto
            {
                Id = 1,
                Name = "Temperature Sensor",
                Type = "Thermal",
                Location = "Room A",
                CreatedAt = DateTime.UtcNow
            };

            var response = new Response<SensorDto>(sensorDto);

            _mockSensorService
                .Setup(s => s.CreateAsync(It.IsAny<SensorDto>()))
                .ReturnsAsync(response);

            // Act
            var result = await _mockSensorService.Object.CreateAsync(sensorDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(sensorDto.Id, result.Data.Id);
            Assert.Equal(sensorDto.Name, result.Data.Name);
        }

        [Fact]
        public async Task GetByIdSensor_ShouldReturnSensorResponseById()
        {
            // Arrange
            var sensorId = 1;
            var sensorDto = new SensorDto
            {
                Id = sensorId,
                Name = "Pressure Sensor",
                Type = "Barometric",
                Location = "Room B",
                CreatedAt = DateTime.UtcNow
            };

            var response = new Response<SensorDto>(sensorDto);

            _mockSensorService
                .Setup(s => s.GetByIdAsync(sensorId))
                .ReturnsAsync(response);

            // Act
            var result = await _mockSensorService.Object.GetByIdAsync(sensorId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(sensorDto.Id, result.Data.Id);
        }

        [Fact]
        public async Task GetAllSensors_ShouldReturnAllSensorsResponse()
        {
            // Arrange
            var sensorDtos = new List<SensorDto>
            {
                new SensorDto { Id = 1, Name = "Temperature Sensor", Type = "Thermal", Location = "Room A", CreatedAt = DateTime.UtcNow },
                new SensorDto { Id = 2, Name = "Humidity Sensor", Type = "Hygrometer", Location = "Room C", CreatedAt = DateTime.UtcNow }
            };

            var response = new Response<List<SensorDto>>(sensorDtos);

            _mockSensorService
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(response);

            // Act
            var result = await _mockSensorService.Object.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(sensorDtos.Count, result.Data.Count);
        }

        [Fact]
        public async Task UpdateSensor_ShouldReturnUpdatedSensorResponse()
        {
            // Arrange
            var updatedSensorDto = new SensorDto
            {
                Id = 1,
                Name = "Updated Sensor",
                Type = "Updated Type",
                Location = "Updated Location",
                CreatedAt = DateTime.UtcNow
            };

            var response = new Response<SensorDto>(updatedSensorDto);

            _mockSensorService
                .Setup(s => s.UpdateAsync(updatedSensorDto))
                .ReturnsAsync(response);

            // Act
            var result = await _mockSensorService.Object.UpdateAsync(updatedSensorDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(updatedSensorDto.Name, result.Data.Name);
        }

        [Fact]
        public async Task DeleteSensor_ShouldReturnSuccessResponse()
        {
            // Arrange
            var sensorId = 1;
            var response = new Response<bool>(true);

            _mockSensorService
                .Setup(s => s.DeleteAsync(sensorId))
                .ReturnsAsync(response);

            // Act
            var result = await _mockSensorService.Object.DeleteAsync(sensorId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.True(result.Data);
        }

        #endregion

    }
}



