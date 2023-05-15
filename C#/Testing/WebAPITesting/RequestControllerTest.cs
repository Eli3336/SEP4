using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;

namespace TestingReq5.WebAPITesting;

public class RequestControllerTest
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
    public async Task CreateAsync_ReturnsCreatedRequest()
    {
        // Arrange
       
        var expectedRequest = new Request() { 
            Type = "eddssf",
            Content = "Etefw4",
        };
        requestLogicMock.Setup(x => x.CreateAsync(It.IsAny<RequestCreationDto>())).ReturnsAsync(expectedRequest);
        
        // Act
        var result = await controller.CreateAsync(
            new RequestCreationDto()
            {
                Type = "Edede",
                Content = "Etefw4",
            });

        // Assert
        Assert.IsInstanceOf<CreatedResult>(result.Result);
        var okResult = (CreatedResult)result.Result;
        Assert.AreSame(expectedRequest, okResult.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedRequestInvalid()
    {
        // Arrange
       
        var expectedRequest = new Request() { 
            Type = "Move",
            Content = "Etefw4",
        };
        requestLogicMock.Setup(x => x.CreateAsync(It.IsAny<RequestCreationDto>())).ReturnsAsync(expectedRequest);
        
        // Act
        var result = await controller.CreateAsync(
            new RequestCreationDto()
            {
                Type = "Move",
                Content = "Etefw4",
            });

        // Assert
        Assert.IsNull(result.Value);
    }
}