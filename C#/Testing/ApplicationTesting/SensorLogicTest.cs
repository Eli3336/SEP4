using Application.DaoInterfaces;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace TestingReq5.ApplicationTesting;

public class SensorLogicTest
{
    private Mock<ISensorDao> sensorDaoMock;
    private SensorLogic sensorService;

    [SetUp]
    public void Setup()
    {
        sensorDaoMock = new Mock<ISensorDao>();
        sensorService = new SensorLogic(sensorDaoMock.Object);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedSensor()
    {
        
        var expectedSensor = new Sensor
        {
            Id = 1,
            Type = "Temperature",
            UpBreakpoint = 2,
            DownBreakpoint = 2,
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.CreateAsync(It.IsAny<Sensor>()))
            .ReturnsAsync(expectedSensor);

        var sensorCreationDto = new SensorCreationDto
        {
            Type = "Temperature",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
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
        var expectedSensorValues = new List<SensorValue>
        {
            new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
            new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
        };
        sensorDaoMock.Setup(dao => dao.GetSensorsValuesAsync(It.IsAny<int?>()))
            .ReturnsAsync(expectedSensorValues);

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
        var expectedSensorValueLog = new List<SensorValue>
        {
            new SensorValue { ValueId = 1, Value = 20, TimeStamp = DateTime.Now.AddMinutes(-1) },
            new SensorValue { ValueId = 1, Value = 25, TimeStamp = DateTime.Now },
        };
        sensorDaoMock.Setup(dao => dao.GetLogOfSensorValuesAsync(It.IsAny<int?>()))
            .ReturnsAsync(expectedSensorValueLog);

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




    [Test]
    public async Task SensorUpdateAsync_Ok()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 25,
            DownBreakpoint =21,
            Type = "Temperature",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();
        sensorDaoMock.Setup(dao => dao.SensorUpdateAsync(It.IsAny<Sensor>())).Verifiable();

        await sensorService.SensorUpdateAsync(1, 25, 21);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
        sensorDaoMock.Verify(dao => dao.SensorUpdateAsync(It.IsAny<Sensor>()), Times.Once);
    }

    [Test]
    public async Task SensorUpdateAsync_NotFoundId()
    {
        Sensor? expectedSensor = null;

        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor);

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 25, 21));
        Assert.AreEqual("Sensor with ID 1 not found!", ex.Message);
    }

    [Test]
    public async Task SensorUpdateAsync_TempTooLow()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 25,
            DownBreakpoint =21,
            Type = "Temperature",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 25, -100));
        Assert.AreEqual("The Temperature is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task SensorUpdateAsync_TempTooHigh()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 25,
            DownBreakpoint =21,
            Type = "Temperature",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 100, 21));
        Assert.AreEqual("The Temperature is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task SensorUpdateAsync_TempUpBreakpointSmaller()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 25,
            DownBreakpoint =21,
            Type = "Temperature",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1,
            21,25));
        Assert.AreEqual("The Temperature is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task SensorUpdateAsync_HumidityTooLow()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 40,
            DownBreakpoint =20,
            Type = "Humidity",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 40, 1));
        Assert.AreEqual("The humidity is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task SensorUpdateAsync_HumidityTooHigh()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 40,
            DownBreakpoint =20,
            Type = "Humidity",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 100,20));
        Assert.AreEqual("The humidity is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }
    
    [Test]
    public async Task SensorUpdateAsync_HumidityUpBreakpointSmaller()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 40,
            DownBreakpoint =20,
            Type = "Humidity",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 20,40));
        Assert.AreEqual("The humidity is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }
    
    
    [Test]
    public async Task SensorUpdateAsync_CO2TooLow()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 2000,
            DownBreakpoint =1000,
            Type = "CO2",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 2000, 100));
        Assert.AreEqual("The CO2 level is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task SensorUpdateAsync_CO2TooHigh()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 2000,
            DownBreakpoint =1000,
            Type = "CO2",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 20000,1000));
        Assert.AreEqual("The CO2 level is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }
    
    [Test]
    public async Task SensorUpdateAsync_CO2UpBreakpointSmaller()
    {
        Sensor expectedSensor = new Sensor
        {
            Id = 1,
            UpBreakpoint = 2000,
            DownBreakpoint =1000,
            Type = "CO2",
            Values = new List<SensorValue>
            {
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() },
                new SensorValue { ValueId = 1, Value = 1, TimeStamp = new DateTime() }
            }
        };
        sensorDaoMock.Setup(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>())).ReturnsAsync(expectedSensor)
            .Verifiable();

        var ex = Assert.ThrowsAsync<Exception>(() => sensorService.SensorUpdateAsync(1, 1000,2000));
        Assert.AreEqual("The CO2 level is invalid!", ex.Message);
        sensorDaoMock.Verify(dao => dao.GetByIdToUpdateAsync(It.IsAny<int>()), Times.Once);
    }
}
    