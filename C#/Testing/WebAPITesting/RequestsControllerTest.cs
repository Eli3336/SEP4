using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;

namespace TestingReq5.WebAPITesting;

public class RequestsControllerTest
{
    private Mock<IRequestLogic> requestLogicMock;
    private RequestsController controller;

    [SetUp]
    public void Setup()
    {
        requestLogicMock = new Mock<IRequestLogic>();
        controller = new RequestsController(requestLogicMock.Object);
    }
    [Test]
    public async Task CreateAsync_ReturnsCreatedRequest_TypeMove()
    {
        // Arrange
        var expectedRequest = new Request { 
           Type = "Move",
           Content = "Move patient 1 to room 3"
        };
        requestLogicMock.Setup(x => x.CreateAsync(It.IsAny<RequestCreationDto>())).ReturnsAsync(expectedRequest);
        
        // Act
        var result = await controller.CreateAsync(
            new RequestCreationDto
            {
                Type = "Move",
                Content = "Move patient 1 to room 3"
            });

        // Assert
        Assert.IsInstanceOf<CreatedResult>(result.Result);
        var okResult = (CreatedResult)result.Result;
        Assert.AreSame(expectedRequest, okResult.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedRequest_TypeAdditional()
    {
        // Arrange
        var expectedRequest = new Request { 
            Type = "Additional",
            Content = "Make sure room 2 has a minimum of 20 degrees"
        };
        requestLogicMock.Setup(x => x.CreateAsync(It.IsAny<RequestCreationDto>())).ReturnsAsync(expectedRequest);
        
        // Act
        var result = await controller.CreateAsync(
            new RequestCreationDto
            {
                Type = "Additional",
                Content = "Make sure room 2 has a minimum of 20 degrees"
            });

        // Assert
        Assert.IsInstanceOf<CreatedResult>(result.Result);
        var okResult = (CreatedResult)result.Result;
        Assert.AreSame(expectedRequest, okResult.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedRequest_TypeInvalid()
    {
        // Arrange
        requestLogicMock.Setup(x => x.CreateAsync(It.IsAny<RequestCreationDto>())).ThrowsAsync(new Exception("Invalid request type! Request type must be 'Move' or 'Additional'."));
        
        // Act
        var result = await controller.CreateAsync(
            new RequestCreationDto
            {
                Type = "string",
                Content = "Make sure room 2 has a minimum of 20 degrees"
            });

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var badResult = (ObjectResult)result.Result!;
        Assert.AreEqual(500, badResult.StatusCode);
        Assert.IsNull(result.Value);
        Assert.AreEqual("Invalid request type! Request type must be 'Move' or 'Additional'.", badResult.Value);
    }
}