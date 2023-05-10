using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebAPI.Controllers;
namespace TestingReq5.WebAPITesting;

public class DoctorsControllerTest
{
    private Mock<IDoctorLogic> doctorLogicMock;
    private DoctorsController controller;

    [SetUp]
    public void Setup()
    {
        doctorLogicMock = new Mock<IDoctorLogic>();
        controller = new DoctorsController(doctorLogicMock.Object);
    }
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor()
    {
        // Arrange
        var expectedDoctor = new Doctor() { 
            Name = "Ana",
            Password = "1234",
            PhoneNumber = "50123456"
        };
        doctorLogicMock.Setup(x => x.CreateAsync(It.IsAny<DoctorCreationDto>())).ReturnsAsync(expectedDoctor);
        
        // Act
        var result = await controller.CreateAsync(
            new DoctorCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.IsInstanceOf<CreatedResult>(result.Result);
        var okResult = (CreatedResult)result.Result;
        Assert.AreSame(expectedDoctor, okResult.Value);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidNameSmall()
    {
        // Arrange
        doctorLogicMock.Setup(x => x.CreateAsync(It.IsAny<DoctorCreationDto>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var result = await controller.CreateAsync(
            new DoctorCreationDto()
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
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidNameBig()
    {
        // Arrange
        doctorLogicMock.Setup(x => x.CreateAsync(It.IsAny<DoctorCreationDto>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var result = await controller.CreateAsync(
            new DoctorCreationDto()
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
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidPasswordSmall()
    {
        // Arrange
        doctorLogicMock.Setup(x => x.CreateAsync(It.IsAny<DoctorCreationDto>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var result = await controller.CreateAsync(
            new DoctorCreationDto()
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
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidPasswordBig()
    {
        // Arrange
        doctorLogicMock.Setup(x => x.CreateAsync(It.IsAny<DoctorCreationDto>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var result = await controller.CreateAsync(
            new DoctorCreationDto()
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
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidPhoneNumberSmall()
    {
        // Arrange
        doctorLogicMock.Setup(x => x.CreateAsync(It.IsAny<DoctorCreationDto>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var result = await controller.CreateAsync(
            new DoctorCreationDto()
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
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidPhoneNumberBig()
    {
        // Arrange
        doctorLogicMock.Setup(x => x.CreateAsync(It.IsAny<DoctorCreationDto>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var result = await controller.CreateAsync(
            new DoctorCreationDto()
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