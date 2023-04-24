using Application.DaoInterfaces;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace TestingReq5.ApplicationTesting;

public class SensorLogicTest
{
    [Test]
    public async Task CreateAsync_ReturnsCreatedSensor()
    {
        var sensorDaoMock = new Mock<ISensorDao>();
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
        sensorDaoMock.Setup(dao => dao.CreateAsync(It.IsAny<Sensor>()))
            .ReturnsAsync(expectedSensor);

        var sensorService = new SensorLogic(sensorDaoMock.Object);

        var sensorCreationDto = new SensorCreationDto
        { 
            Type = "Temperature",
            Values = new List<SensorValue>
            {
                new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime()},
                new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime() },
                new SensorValue {valueId = 1,value = 1, timeStamp = new DateTime()}
            }
        };

        var createdSensor = await sensorService.CreateAsync(sensorCreationDto);

        Assert.IsNotNull(createdSensor);
        Assert.AreEqual(expectedSensor.Type, createdSensor.Type);
        Assert.AreEqual(expectedSensor.Values.Count, createdSensor.Values.Count);
        Assert.IsTrue(expectedSensor.Values.SequenceEqual(createdSensor.Values));
    }

    [Test]
    public async Task GetSensorsValuesAsync_ReturnsSensorsValues()
    {
        var sensorDaoMock = new Mock<ISensorDao>();
        var expectedSensorValues = new List<SensorValue>
        {
            new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime()},
            new SensorValue { valueId = 1,value = 1, timeStamp = new DateTime() },
        };
        sensorDaoMock.Setup(dao => dao.GetSensorsValuesAsync(It.IsAny<int?>()))
            .ReturnsAsync(expectedSensorValues);

        var sensorService = new SensorLogic(sensorDaoMock.Object);

        var sensorValues = await sensorService.GetSensorsValuesAsync(1);

        Assert.IsNotNull(sensorValues);
        Assert.AreEqual(expectedSensorValues.Count, sensorValues.Count());
        Assert.IsTrue(expectedSensorValues.All(expected => sensorValues.Any(actual =>
            actual.valueId == expected.valueId &&
            actual.value == expected.value &&
            actual.timeStamp == expected.timeStamp)));
    }

    [Test]
    public async Task GetLogOfSensorValuesAsync_ReturnsSensorValueLog()
    {
        // Arrange
        var sensorDaoMock = new Mock<ISensorDao>();
        var expectedSensorValueLog = new List<SensorValue>
        {
            new SensorValue { valueId = 1, value = 20, timeStamp = DateTime.Now.AddMinutes(-1) },
            new SensorValue { valueId = 1, value = 25, timeStamp = DateTime.Now },
        };
        sensorDaoMock.Setup(dao => dao.GetLogOfSensorValuesAsync(It.IsAny<int?>()))
            .ReturnsAsync(expectedSensorValueLog);

        var sensorService = new SensorLogic(sensorDaoMock.Object);

        // Act
        var sensorValueLog = await sensorService.GetLogOfSensorValuesAsync(1);

        // Assert
        Assert.IsNotNull(sensorValueLog);
        Assert.AreEqual(expectedSensorValueLog.Count, sensorValueLog.Count());
        Assert.IsTrue(expectedSensorValueLog.All(expected => sensorValueLog.Any(actual =>
            actual.valueId == expected.valueId &&
            actual.value == expected.value &&
            actual.timeStamp == expected.timeStamp)));
    }

}