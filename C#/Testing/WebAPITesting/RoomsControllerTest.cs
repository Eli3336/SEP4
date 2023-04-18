using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;

namespace TestingReq5.WebAPITesting;

public class RoomsControllerTest
{
    private Mock<IRoomLogic> roomLogicMock;
    private RoomsController controller;

    [SetUp]
    public void Setup()
    {
        roomLogicMock = new Mock<IRoomLogic>();
        controller = new RoomsController(roomLogicMock.Object);
    }
    [Test]
    public async Task CreateAsync_ReturnsCreatedRoom()
    {
        // Arrange
        var toSensor = new List<Sensor>
        {
            new Sensor { Id = 1,Type = "Temperature", UpBreakpoint = 2,DownBreakpoint = 2,Values = new List<SensorValue>()},
            new Sensor { Id = 2,Type = "Humidity", UpBreakpoint = 2,DownBreakpoint = 2,Values = new List<SensorValue>() },
            new Sensor { Id = 3,Type = "CO2", UpBreakpoint = 2,DownBreakpoint = 2,Values = new List<SensorValue>() }
        };
        var roomLogicMock = new Mock<IRoomLogic>();
        var expectedRoom = new Room { 
            Name = "Test Room",
            Capacity = 10,
            Availability = "Available",
            Sensors = toSensor
        };
        roomLogicMock.Setup(x => x.CreateAsync(It.IsAny<RoomCreationDto>())).ReturnsAsync(expectedRoom);
        var controller = new RoomsController(roomLogicMock.Object);

        // Act
        var result = await controller.CreateAsync(
            new RoomCreationDto
            {
                Name = "Test Room",
                Capacity = 10,
                Availability = "Available",
                Sensors = toSensor
            });

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
        var createdResult = (CreatedAtActionResult)result.Result;
        Assert.AreSame(expectedRoom, createdResult.Value);
    }


    [Test]
    public async Task GetAllRoomsName_ReturnsListOfRoomNames()
    {
        // Arrange
        var roomNames = new List<string> { "Room 1", "Room 2", "Room 3" };
        roomLogicMock.Setup(mock => mock.GetAllNames())
            .Returns(roomNames);

        // Act
        var result = await controller.GetAllRoomsName();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = (OkObjectResult)result.Result;
        Assert.AreEqual(roomNames, okResult.Value);
    }
}

