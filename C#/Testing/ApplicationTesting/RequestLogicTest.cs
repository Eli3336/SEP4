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
    public async Task GetAllAdditionalRequests_ReturnsRequestsToMove()
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