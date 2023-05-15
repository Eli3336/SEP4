using Application.DaoInterfaces;
using Application.Logic;
using Domain.DTOs;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace TestingReq5.ApplicationTesting;

public class RequestLogicTest
{
    [Test]
    public async Task CreateAsync_ReturnsCreatedRequest()
    {
        var requestDaoMock = new Mock<IRequestDao>();
        var expectedRequest = new Request()
        {    Type = "Move",
            Content = "Test",
        };
        requestDaoMock.Setup(dao => dao.CreateAsync(It.IsAny<Request>()))
            .ReturnsAsync(expectedRequest);

        var requestService = new RequestLogic(requestDaoMock.Object);

        var requestCreationDto = new RequestCreationDto()
        { 
            Type = "Additional",
            Content = "Temp"
        };

        var createdSensor = await requestService.CreateAsync(requestCreationDto);

        Assert.IsNotNull(createdSensor);
        Assert.AreEqual(expectedRequest.Type, createdSensor.Type);
        Assert.AreEqual(expectedRequest.Content, createdSensor.Content);
        
    }

}