using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;

namespace TestingReq5.WebAPITesting;
using NUnit.Framework;
using Sandbox;

[TestFixture]
public class SensorsControllerTest
{
   
    private  Mock<ISensorLogic> sensorLogicMock;
    private  SensorsController controller;

    [SetUp]
    public void Setup()
    {
        sensorLogicMock = new Mock<ISensorLogic>();
        controller = new SensorsController(sensorLogicMock.Object);
    }
    

    [Test]
    public async Task CreateAsync_ReturnsCreatedSensor()
    {
        // Arrange
        var expectedSensor = new Sensor
        {    Id = 1,
            Type = "Temperature", 
            UpBreakpoint = 2,
            DownBreakpoint = 2,
            Values = new List<SensorValue>
            {
                new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime()},
                new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime() },
                new SensorValue {valueId = 1,value = 1, timeStamp = new DateTime()}
            }
        };
        sensorLogicMock.Setup(x => x.CreateAsync(It.IsAny<SensorCreationDto>())).ReturnsAsync(expectedSensor);

        // Act
        var result = await controller.CreateAsync(new SensorCreationDto
        {
            Type = "Temperature",
            Values = new List<SensorValue>
            {
                new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime()},
                new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime() },
                new SensorValue {valueId = 1,value = 1, timeStamp = new DateTime()}
            }
        });

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = (CreatedAtActionResult)result.Result;
        Assert.AreEqual(expectedSensor, createdResult.Value);
    }

    [Test]
    public async Task GetSensorsValuesAsync_ReturnsSensorsValues()
    {
        // Arrange
        var expectedValues = new List<SensorValue>
        {
            new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime()},
            new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime() }
        };
        sensorLogicMock.Setup(x => x.GetSensorsValuesAsync(It.IsAny<int?>())).ReturnsAsync(expectedValues);

        // Act
        var result = await controller.GetSensorsValuesAsync(roomId: 1);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        Assert.AreEqual(expectedValues, okResult.Value);
    }

    [Test]
    public async Task GetLogOfSensorValuesAsync_ReturnsSensorLog()
    {
        // Arrange
        var expectedLog = new List<SensorValue>
        {
            new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime()},
            new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime() }
        };
        sensorLogicMock.Setup(x => x.GetLogOfSensorValuesAsync(It.IsAny<int?>())).ReturnsAsync(expectedLog);

        // Act
        var result = await controller.GetLogOfSensorValuesAsync(sensorId: 1);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        Assert.AreEqual(expectedLog, okResult.Value);
    }
}

