using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;

namespace TestingReq5.WebAPITesting;

public class ReceptionistsControllerTest
{
    private Mock<IReceptionistLogic> receptionistLogicMock;
    private ReceptionistsController controller;

    [SetUp]
    public void Setup()
    {
        receptionistLogicMock = new Mock<IReceptionistLogic>();
        controller = new ReceptionistsController(receptionistLogicMock.Object);
    }
    
    //create method
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist()
    {
        // Arrange
        var expectedReceptionist = new Receptionist() { 
            Name = "Ana",
            Password = "1234",
            PhoneNumber = "50123456"
        };
        receptionistLogicMock.Setup(x => x.CreateAsync(It.IsAny<ReceptionistCreationDto>())).ReturnsAsync(expectedReceptionist);
        
        // Act
        var result = await controller.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.IsInstanceOf<CreatedResult>(result.Result);
        var okResult = (CreatedResult)result.Result;
        Assert.AreSame(expectedReceptionist, okResult.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidNameNumber()
    {
        // Arrange
        receptionistLogicMock.Setup(x => x.CreateAsync(It.IsAny<ReceptionistCreationDto>())).ThrowsAsync(new Exception("Name cannot contain numbers!"));

        // Act
        var result = await controller.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "Ana2",
                Password = "1234",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var badResult = (ObjectResult)result.Result;
        Assert.AreEqual(500, badResult.StatusCode);
        Assert.IsNull(result.Value);
    }

    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidNameSmall()
    {
        // Arrange
        receptionistLogicMock.Setup(x => x.CreateAsync(It.IsAny<ReceptionistCreationDto>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var result = await controller.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "An",
                Password = "1234",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var badResult = (ObjectResult)result.Result;
        Assert.AreEqual(500, badResult.StatusCode);
        Assert.IsNull(result.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidNameBig()
    {
        // Arrange
        receptionistLogicMock.Setup(x => x.CreateAsync(It.IsAny<ReceptionistCreationDto>())).ThrowsAsync(new Exception("Name too big!"));

        // Act
        var result = await controller.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "Anaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Password = "1234",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var badResult = (ObjectResult)result.Result;
        Assert.AreEqual(500, badResult.StatusCode);
        Assert.IsNull(result.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidPasswordSmall()
    {
        // Arrange
        receptionistLogicMock.Setup(x => x.CreateAsync(It.IsAny<ReceptionistCreationDto>())).ThrowsAsync(new Exception("Password too small!"));

        // Act
        var result = await controller.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "12",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var badResult = (ObjectResult)result.Result;
        Assert.AreEqual(500, badResult.StatusCode);
        Assert.IsNull(result.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidPasswordBig()
    {
        // Arrange
        receptionistLogicMock.Setup(x => x.CreateAsync(It.IsAny<ReceptionistCreationDto>())).ThrowsAsync(new Exception("Password too big!"));

        // Act
        var result = await controller.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "1234567891123456789212345678931234567894123456789512345678961234567897123456789812345678991234567890123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789012345678911234567892123456789312345678941234567895123456789",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var badResult = (ObjectResult)result.Result;
        Assert.AreEqual(500, badResult.StatusCode);
        Assert.IsNull(result.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidPhoneNumberSmall()
    {
        // Arrange
        receptionistLogicMock.Setup(x => x.CreateAsync(It.IsAny<ReceptionistCreationDto>())).ThrowsAsync(new Exception("Phone number too small!"));

        // Act
        var result = await controller.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50"
            });

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var badResult = (ObjectResult)result.Result;
        Assert.AreEqual(500, badResult.StatusCode);
        Assert.IsNull(result.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidPhoneNumberBig()
    {
        // Arrange
        receptionistLogicMock.Setup(x => x.CreateAsync(It.IsAny<ReceptionistCreationDto>())).ThrowsAsync(new Exception("Phone number too big!"));

        // Act
        var result = await controller.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50345678911234567892234567890090876543221345678998765432"
            });

        // Assert
        Assert.IsInstanceOf<ObjectResult>(result.Result);
        var badResult = (ObjectResult)result.Result;
        Assert.AreEqual(500, badResult.StatusCode);
        Assert.IsNull(result.Value);
    }
}