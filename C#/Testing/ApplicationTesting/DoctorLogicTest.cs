using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace TestingReq5.ApplicationTesting;

public class DoctorLogicTest
{
    private Mock<IDoctorDao> doctorDaoMock;
    private DoctorLogic logic;

    [SetUp]
    public void Setup()
    {
        doctorDaoMock = new Mock<IDoctorDao>();
        logic = new DoctorLogic(doctorDaoMock.Object);
    }
    
    //create method
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor()
    {
        // Arrange
        var expectedDoctor = new Doctor() { 
            Name = "Ana",
            Password = "1234",
            PhoneNumber = "50123456"
        };
        doctorDaoMock.Setup(x => x.CreateAsync(It.IsAny<Doctor>())).ReturnsAsync(expectedDoctor);
        
        // Act
        var result = await logic.CreateAsync(
            new DoctorCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50123456"
            });

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(expectedDoctor.Id, result.Id);
        Assert.AreEqual(expectedDoctor.Name, result.Name);
        Assert.AreEqual(expectedDoctor.Password, result.Password);
        Assert.AreEqual(expectedDoctor.PhoneNumber, result.PhoneNumber);
        doctorDaoMock.Verify(dao => dao.CreateAsync(It.IsAny<Doctor>()), Times.Once);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidNameNumber()
    {
        // Arrange
        doctorDaoMock.Setup(x => x.CreateAsync(It.IsAny<Doctor>())).ThrowsAsync(new Exception("Name cannot contain numbers!"));

        // Act
        var doctor = new DoctorCreationDto()
        {
            Name = "Ana2",
            Password = "1234",
            PhoneNumber = "50123456"
        };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(doctor));
        Assert.AreEqual("Name cannot contain numbers!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidNameSmall()
    {
        // Arrange
        doctorDaoMock.Setup(x => x.CreateAsync(It.IsAny<Doctor>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var doctor = new DoctorCreationDto()
            {
                Name = "An",
                Password = "1234",
                PhoneNumber = "50123456"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(doctor));
        Assert.AreEqual("Name too small!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidNameBig()
    {
        // Arrange
        doctorDaoMock.Setup(x => x.CreateAsync(It.IsAny<Doctor>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var doctor = new DoctorCreationDto()
            {
                Name = "Anaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Password = "1234",
                PhoneNumber = "50123456"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(doctor));
        Assert.AreEqual("Name too big!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidPasswordSmall()
    {
        // Arrange
        doctorDaoMock.Setup(x => x.CreateAsync(It.IsAny<Doctor>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var doctor = new DoctorCreationDto()
            {
                Name = "Ana",
                Password = "12",
                PhoneNumber = "50123456"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(doctor));
        Assert.AreEqual("Password too small!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidPasswordBig()
    {
        // Arrange
        doctorDaoMock.Setup(x => x.CreateAsync(It.IsAny<Doctor>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var doctor = new DoctorCreationDto()
            {
                Name = "Ana",
                Password = "1234567891123456789212345678931234567894123456789512345678961234567897123456789812345678991234567890123456789112345678921234567893123456789412345678951234567896123456789712345678981234567899123456789012345678911234567892123456789312345678941234567895123456789",
                PhoneNumber = "50123456"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(doctor));
        Assert.AreEqual("Password too big!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidPhoneNumberSmall()
    {
        // Arrange
        doctorDaoMock.Setup(x => x.CreateAsync(It.IsAny<Doctor>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var doctor = new DoctorCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(doctor));
        Assert.AreEqual("Phone number too small!", ex.Message);
    }
    
    [Test]
    public async Task CreateAsync_ReturnsCreatedDoctor_InvalidPhoneNumberBig()
    {
        // Arrange
        doctorDaoMock.Setup(x => x.CreateAsync(It.IsAny<Doctor>())).ThrowsAsync(new Exception("Name too small!"));

        // Act
        var doctor = new DoctorCreationDto()
            {
                Name = "Ana",
                Password = "1234",
                PhoneNumber = "50345678911234567892234567890090876543221345678998765432"
            };

        // Assert
        var ex = Assert.Throws<Exception>(() => logic.CreateAsync(doctor));
        Assert.AreEqual("Phone number too big!", ex.Message);
    }
    
    //get method

    [Test]
    public async Task GetDoctorByIdAsync_ReturnsDoctor()
    {
        var expectedDoctor = new Doctor()
        {
            Id = 1,
            Name = "Ana",
            Password = "1234",
            PhoneNumber = "50123456"
        };
            
        doctorDaoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedDoctor);

        var result = await logic.GetByIdAsync(1);
        
        Assert.IsNotNull(result);
        Assert.AreEqual(result.Id, expectedDoctor.Id);
        Assert.AreEqual(result.Name, expectedDoctor.Name);
        Assert.AreEqual(result.Password, expectedDoctor.Password);
        Assert.AreEqual(result.PhoneNumber, expectedDoctor.PhoneNumber);
    }
    
    [Test]
    public async Task GetDoctorByIdAsync_ReturnsDoctor_InvalidId()
    {
        doctorDaoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("Doctor with ID 10 not found!"));

        var ex = Assert.ThrowsAsync<Exception>((() => logic.GetByIdAsync(10)));
        Assert.AreEqual("Doctor with ID 10 not found!", ex.Message);
    }
}