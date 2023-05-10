using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace TestingReq5.ApplicationTesting;

public class ReceptionistLogicTest
{
    private Mock<IReceptionistDao> receptionistDaoMock;
    private ReceptionistLogic logic;

    [SetUp]
    public void Setup()
    {
        receptionistDaoMock = new Mock<IReceptionistDao>();
        logic = new ReceptionistLogic(receptionistDaoMock.Object);
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
        receptionistDaoMock.Setup(x => x.CreateAsync(It.IsAny<Receptionist>())).ReturnsAsync(expectedReceptionist);
        
        // Act
        var result = await logic.CreateAsync(
            new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(expectedReceptionist.Id, result.Id);
        Assert.AreEqual(expectedReceptionist.Name, result.Name);
        Assert.AreEqual(expectedReceptionist.Password, result.Password);
        Assert.AreEqual(expectedReceptionist.PhoneNumber, result.PhoneNumber);
        receptionistDaoMock.Verify(dao => dao.CreateAsync(It.IsAny<Receptionist>()), Times.Once);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidNameNumber()
    {
        // Arrange
        receptionistDaoMock.Setup(x => x.CreateAsync(It.IsAny<Receptionist>())).ThrowsAsync(new Exception("Name cannot contain numbers!"));

        // Act
        var receptionist = new ReceptionistCreationDto()
        {
            Name = "Ana2",
            Password = "1234",
            PhoneNumber = "50123456"
        };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(receptionist));
        Assert.AreEqual("Name cannot contain numbers!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidNameSmall()
    {
        // Arrange
        receptionistDaoMock.Setup(x => x.CreateAsync(It.IsAny<Receptionist>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var receptionist = new ReceptionistCreationDto()
            {
                Name = "An",
                Password = "1234",
                PhoneNumber = "50123456"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(receptionist));
        Assert.AreEqual("Name too small!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidNameBig()
    {
        // Arrange
        receptionistDaoMock.Setup(x => x.CreateAsync(It.IsAny<Receptionist>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var receptionist = new ReceptionistCreationDto()
            {
                Name = "Anaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Password = "1234",
                PhoneNumber = "50123456"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(receptionist));
        Assert.AreEqual("Name too big!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidPasswordSmall()
    {
        // Arrange
        receptionistDaoMock.Setup(x => x.CreateAsync(It.IsAny<Receptionist>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var receptionist = new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "12",
                PhoneNumber = "50123456"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(receptionist));
        Assert.AreEqual("Password too small!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidPasswordBig()
    {
        // Arrange
        receptionistDaoMock.Setup(x => x.CreateAsync(It.IsAny<Receptionist>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var receptionist = new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "1234567891123456789212345678931234567894123456789512345678961234567897123456789812345678991234567890123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789012345678911234567892123456789312345678941234567895123456789",
                PhoneNumber = "50123456"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(receptionist));
        Assert.AreEqual("Password too big!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidPhoneNumberSmall()
    {
        // Arrange
        receptionistDaoMock.Setup(x => x.CreateAsync(It.IsAny<Receptionist>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var receptionist = new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(receptionist));
        Assert.AreEqual("Phone number too small!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedReceptionist_InvalidPhoneNumberBig()
    {
        // Arrange
        receptionistDaoMock.Setup(x => x.CreateAsync(It.IsAny<Receptionist>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var receptionist = new ReceptionistCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50345678911234567892234567890090876543221345678998765432"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(receptionist));
        Assert.AreEqual("Phone number too big!", ex.Message);
    }
    
    //get method
    
    [Test]
    public async Task GetReceptionistByIdAsync_ReturnsReceptionist()
    {
        var expectedReceptionist = new Receptionist() { 
            Id = 1,
            Name = "Ana",
            Password = "1234",
            PhoneNumber = "50123456"
        };
        receptionistDaoMock.Setup(dao => dao.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(expectedReceptionist);

        var result = await logic.GetByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Id, expectedReceptionist.Id);
        Assert.AreEqual(result.Name, expectedReceptionist.Name);
        Assert.AreEqual(result.Password, expectedReceptionist.Password);
        Assert.AreEqual(result.PhoneNumber, expectedReceptionist.PhoneNumber);
    }

    [Test]
    public async Task GetReceptionistByIdAsync_ReturnsReceptionist_InvalidId()
    {
        receptionistDaoMock.Setup(dao => dao.GetByIdAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Receptionist with ID 10 not found!"));

        var ex = Assert.Throws<Exception>(() => logic.GetByIdAsync(10));
        Assert.AreEqual("Receptionist with ID 10 not found!", ex.Message);
    }
}