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

  [Test]
    public async Task GetAllAdditionalRequests_ReturnsRequests()
    {
        var requestDaoMock = new Mock<IRequestDao>();
        var expectedRequests = new List<Request>
        {
            new Request { Type = "Additional", Content = "Make sure room 2 has a temp lower than 20"},
            new Request { Type = "Additional", Content = "Make sure room 1 has a temp higher than 20"},
        };
        requestDaoMock.Setup(dao => dao.GetAllRequests())
            .ReturnsAsync(expectedRequests);

        var requestService = new RequestLogic(requestDaoMock.Object);

        var requestValues = await requestService.GetAllAdditionalRequests();

        Assert.IsNotNull(requestValues);
        Assert.AreEqual(expectedRequests.Count, requestValues.Count());
        
    }
}