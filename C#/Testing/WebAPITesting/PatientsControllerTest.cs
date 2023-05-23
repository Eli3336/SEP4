using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;

namespace TestingReq5.WebAPITesting;

public class PatientsControllerTest
{
    private Mock<IPatientLogic> patientLogicMock;
    private PatientsController controller;

    [SetUp]
    public void Setup()
    {
        patientLogicMock = new Mock<IPatientLogic>();
        controller = new PatientsController(patientLogicMock.Object);
    }

    //move method
    
    [Test]
    public async Task ReceptionistUpdateAsync_ReturnsOkStatus()
    {
        // Arrange
        patientLogicMock.Setup(mock => mock.MovePatientToGivenRoom(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

        // Act
        var result = await controller.MovePatientToGivenRoom(1, 3);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
        patientLogicMock.Verify(logic => logic.MovePatientToGivenRoom(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
    
    [Test]
    public async Task ReceptionistUpdateAsync_ReturnsException()
    {
        // Arrange
        patientLogicMock.Setup(mock => mock.MovePatientToGivenRoom(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("Patient with ID 10 not found!")).Verifiable();

        // Act
        var result = await controller.MovePatientToGivenRoom(10, 3);

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result);
        var badResult = (ObjectResult)result;
        Assert.AreEqual(500, badResult.StatusCode); 
        patientLogicMock.Verify(logic => logic.MovePatientToGivenRoom(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }
}