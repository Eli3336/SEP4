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
        var roomDaoMock = new Mock<IRoomDao>();
        var expectedSensor = new Sensor
        {    Id = 1,
            Type = "Temperature", 
            UpBreakpoint = 2,
            DownBreakpoint = 2,
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1,Value = 1, TimeStamp = new DateTime()},
                new SensorValue { ValueId = 1,Value = 1, TimeStamp = new DateTime() },
                new SensorValue {ValueId = 1,Value = 1, TimeStamp = new DateTime()}
            }
        };
        sensorDaoMock.Setup(dao => dao.CreateAsync(It.IsAny<Sensor>()))
            .ReturnsAsync(expectedSensor);

        var sensorService = new SensorLogic(sensorDaoMock.Object, roomDaoMock.Object);

        var sensorCreationDto = new SensorCreationDto
        { 
            Type = "Temperature",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1,Value = 1, TimeStamp = new DateTime()},
                new SensorValue { ValueId = 1,Value = 1, TimeStamp = new DateTime() },
                new SensorValue {ValueId = 1,Value = 1, TimeStamp = new DateTime()}
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
        var roomDaoMock = new Mock<IRoomDao>();
        var timestamp = new DateTime();
        var expectedSensorValues1 = new List<SensorValue>
        {
            new SensorValue { ValueId = 1,Value = 1, TimeStamp = timestamp},
            new SensorValue { ValueId = 2,Value = 2, TimeStamp = timestamp},
        };
        var expectedSensorValues2 = new List<SensorValue>
        {
            new SensorValue { ValueId = 3,Value = 1, TimeStamp = timestamp},
            new SensorValue { ValueId = 4,Value = 4, TimeStamp = timestamp},
        };
        var expectedSensorValues3 = new List<SensorValue>
        {
            new SensorValue { ValueId = 5,Value = 1, TimeStamp = timestamp},
            new SensorValue { ValueId = 6,Value = 6, TimeStamp = timestamp},
        };
        var sensor1 = new Sensor
        {
            Id = 1,
            Type = "Temperature",
            Values = expectedSensorValues1
        };
        var sensor2 = new Sensor
        {
            Id = 2,
            Type = "Humidity",
            Values = expectedSensorValues2
        };
        var sensor3 = new Sensor
        {
            Id = 3,
            Type = "CO2",
            Values = expectedSensorValues3
        };
        var expectedSensors = new List<Sensor>();
        expectedSensors.Add(sensor1);
        expectedSensors.Add(sensor2);
        expectedSensors.Add(sensor3);
        var expectedRoom = new Room
        {
            Id = 1,
            Name = "Room1",
            Capacity = 1,
            Availability = "Available",
            Patients = new List<Patient>(),
            Sensors = expectedSensors
        };
        var expectedSensorValues = new List<SensorValue>
        {
            new SensorValue { ValueId = 2, Value = 2, TimeStamp = timestamp },
            new SensorValue { ValueId = 4, Value = 4, TimeStamp = timestamp },
            new SensorValue { ValueId = 6, Value = 6, TimeStamp = timestamp }
        };
        roomDaoMock.Setup(dao => dao.GetById(It.IsAny<int>()))
            .ReturnsAsync(expectedRoom);
        sensorDaoMock.Setup(dao => dao.GetById(1))
            .ReturnsAsync(sensor1);
        sensorDaoMock.Setup(dao => dao.GetById(2))
                    .ReturnsAsync(sensor2);
        sensorDaoMock.Setup(dao => dao.GetById(3))
            .ReturnsAsync(sensor3);

        var sensorService = new SensorLogic(sensorDaoMock.Object, roomDaoMock.Object);

        var sensorValues = await sensorService.GetSensorsValuesAsync(1);

        Assert.IsNotNull(sensorValues);
        Assert.AreEqual(expectedSensorValues.Count, sensorValues.Count());
        Assert.IsTrue(expectedSensorValues.All(expected => sensorValues.Any(actual =>
            actual.ValueId == expected.ValueId &&
            actual.Value == expected.Value &&
            actual.TimeStamp == expected.TimeStamp)));
    }

    [Test]
    public async Task GetLogOfSensorValuesAsync_ReturnsSensorValueLog()
    {
        // Arrange
        var sensorDaoMock = new Mock<ISensorDao>();
        var roomDaoMock = new Mock<IRoomDao>();
        var expectedSensorValueLog = new List<SensorValue>
        {
            new SensorValue { ValueId = 1, Value = 20, TimeStamp = DateTime.Now.AddMinutes(-1) },
            new SensorValue { ValueId = 1, Value = 25, TimeStamp = DateTime.Now },
        };
        sensorDaoMock.Setup(dao => dao.GetLogOfSensorValuesAsync(It.IsAny<int?>()))
            .ReturnsAsync(expectedSensorValueLog);

        var sensorService = new SensorLogic(sensorDaoMock.Object, roomDaoMock.Object);

        // Act
        var sensorValueLog = await sensorService.GetLogOfSensorValuesAsync(1);

        // Assert
        Assert.IsNotNull(sensorValueLog);
        Assert.AreEqual(expectedSensorValueLog.Count, sensorValueLog.Count());
        Assert.IsTrue(expectedSensorValueLog.All(expected => sensorValueLog.Any(actual =>
            actual.ValueId == expected.ValueId &&
            actual.Value == expected.Value &&
            actual.TimeStamp == expected.TimeStamp)));
    }
}