using Application.DaoInterfaces;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using EfcDataAccess;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace TestingReq5.ApplicationTesting;
using NUnit.Framework;

public class RoomLogicTest
{
    [Test]
    public async Task create_Async_is_Created()
    {
        var toSensor = new List<Sensor>
        {
            new Sensor { Id = 1,Type = "Temperature", UpBreakpoint = 2,DownBreakpoint = 2,Values = new List<SensorValue>()},
            new Sensor { Id = 2,Type = "Humidity", UpBreakpoint = 2,DownBreakpoint = 2,Values = new List<SensorValue>() },
            new Sensor { Id = 3,Type = "CO2", UpBreakpoint = 2,DownBreakpoint = 2,Values = new List<SensorValue>() }
        };
            var roomToCreate = new RoomCreationDto
        {
            Name = "Test Room",
            Capacity = 3,
            Availability = "Available",
            Sensors = toSensor
        };
        var mockRoomDao = new Mock<IRoomDao>();
        var createdRoom = new Room
        {
            Id = 1,
            Name = roomToCreate.Name,
            Capacity = roomToCreate.Capacity,
            Availability = roomToCreate.Availability,
            Sensors = roomToCreate.Sensors
        };
        mockRoomDao.Setup(dao => dao.CreateAsync(It.IsAny<Room>())).ReturnsAsync(createdRoom);
        
        var roomService = new RoomLogic(mockRoomDao.Object);
        
        var result = await roomService.CreateAsync(roomToCreate);
        
        Assert.NotNull(result);
        Assert.AreEqual(createdRoom.Id, result.Id);
        Assert.AreEqual(createdRoom.Name, result.Name);
        Assert.AreEqual(createdRoom.Capacity, result.Capacity);
        Assert.AreEqual(createdRoom.Availability, result.Availability);
        Assert.AreEqual(createdRoom.Sensors, result.Sensors);
        mockRoomDao.Verify(dao => dao.CreateAsync(It.IsAny<Room>()), Times.Once);
    }
    
    [Test]
    public async Task get_All_Names_Pass()
    {
        var rooms = new List<Room>
        {
            new Room { Name = "Room 1" },
            new Room { Name = "Room 2" },
            new Room { Name = "Room 3" }
        };
    
        var roomDaoMock = new Mock<IRoomDao>();
        roomDaoMock.Setup(dao => dao.GetAllNames())
            .Returns(rooms.Select(room => room.Name));

        var roomService = new RoomLogic(roomDaoMock.Object);
        
        var allRoomNames = roomService.GetAllNames();
        
        Assert.AreEqual(3, allRoomNames.Count());
        Assert.IsTrue(rooms.Select(room => room.Name).SequenceEqual(allRoomNames));
    }
}