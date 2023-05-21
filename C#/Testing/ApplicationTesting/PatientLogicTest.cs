using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using Domain.Models;
using Moq;
using NUnit.Framework;

namespace TestingReq5.ApplicationTesting;

public class PatientLogicTest
{
    private Mock<IPatientDao> patientDaoMock;
    private Mock<IRoomDao> roomDaoMock;
    private PatientLogic logic;

    [SetUp]
    public void Setup()
    {
        patientDaoMock = new Mock<IPatientDao>();
        roomDaoMock = new Mock<IRoomDao>();
        logic = new PatientLogic(roomDaoMock.Object, patientDaoMock.Object);
    }
    
    //move method
    
    [Test]
    public async Task MovePatientToGivenRoom_Ok()
    { 
        List<Room> allRooms = new List<Room>();
        
        Patient patientRoom1 = new Patient
        {
            Id = 1,
            Name = "Ana"
        };
        List<Patient> patientsRoom1 = new List<Patient>();
        patientsRoom1.Add(patientRoom1);
        Room room1 = new Room
        {
            Id = 1,
            Name = "First",
            Capacity = 1,
            Availability = "Occupied",
            Patients = patientsRoom1,
            Sensors = new List<Sensor>()
        };
        
        List<Patient> patientsRoom2 = new List<Patient>();
        Patient patientRoom2 = new Patient
        {
            Id = 1,
            Name = "Bob"
        };
        patientsRoom2.Add(patientRoom2);
        Room room2 = new Room
        {
            Id = 2,
            Name = "First",
            Capacity = 3,
            Availability = "Available",
            Patients = patientsRoom2,
            Sensors = new List<Sensor>()
        };
        
        allRooms.Add(room1);
        allRooms.Add(room2);
        roomDaoMock.Setup(dao => dao.GetById(It.IsAny<int>())).ReturnsAsync(room2).Verifiable();
        patientDaoMock.Setup(dao => dao.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(patientRoom1).Verifiable();
        roomDaoMock.Setup(dao => dao.GetAllRoomsAsync()).ReturnsAsync(allRooms).Verifiable();
        roomDaoMock.Setup(dao => dao.RoomUpdateAsync(It.IsAny<Room>())).Verifiable();
        
        await logic.MovePatientToGivenRoom(1, 2);
        roomDaoMock.Verify(dao => dao.GetById(It.IsAny<int>()), Times.Once);
        patientDaoMock.Verify(dao => dao.GetByIdAsync(It.IsAny<int>()), Times.Once);
        roomDaoMock.Verify(dao => dao.GetAllRoomsAsync(), Times.Once);
        roomDaoMock.Verify(dao => dao.RoomUpdateAsync(It.IsAny<Room>()), Times.Once);
    }
    
    [Test]
    public async Task MovePatientToGivenRoom_NotFoundRoomId()
    { 
        Room? expectedRoom = null;
        
        roomDaoMock.Setup(dao => dao.GetById(It.IsAny<int>())).ReturnsAsync(expectedRoom);
        
        var ex = Assert.ThrowsAsync<Exception>(() => logic.MovePatientToGivenRoom(1, 5));
        Assert.AreEqual("Room with ID 5 not found!", ex.Message);
    }
    
    [Test]
    public async Task MovePatientToGivenRoom_RoomFull()
    {
        List<Patient> patients = new List<Patient>();
        Patient patient1 = new Patient
        {
            Id = 2, Name = "John"
        };
        patients.Add(patient1);
        Room expectedRoom = new Room()
        {
            Id = 1,
            Name = "Room",
            Capacity = 1,
            Availability = "Occupied",
            Patients = patients,
            Sensors = new List<Sensor>()
        };
        roomDaoMock.Setup(dao => dao.GetById(It.IsAny<int>())).ReturnsAsync(expectedRoom).Verifiable();
        
        var ex = Assert.ThrowsAsync<Exception>(() => logic.MovePatientToGivenRoom(1, 1));
        Assert.AreEqual("Room is full. Cannot add more patients!", ex.Message);
        roomDaoMock.Verify(dao => dao.GetById(It.IsAny<int>()), Times.Once);
    }
    
    [Test]
    public async Task MovePatientToGivenRoom_PatientIdNotFound()
    {
        List<Patient> patients = new List<Patient>();
        Patient patient1 = new Patient
        {
            Id = 2, Name = "John"
        };
        patients.Add(patient1);
        Room expectedRoom = new Room()
        {
            Id = 1,
            Name = "Room",
            Capacity = 3,
            Availability = "Available",
            Patients = patients,
            Sensors = new List<Sensor>()
        };
        Patient? expectedPatient = null;
        roomDaoMock.Setup(dao => dao.GetById(It.IsAny<int>())).ReturnsAsync(expectedRoom).Verifiable();
        patientDaoMock.Setup(dao => dao.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedPatient).Verifiable();
        var ex = Assert.ThrowsAsync<Exception>(() => logic.MovePatientToGivenRoom(1, 1));
        Assert.AreEqual("Patient with ID {patientId} was not found!", ex.Message);
        roomDaoMock.Verify(dao => dao.GetById(It.IsAny<int>()), Times.Once);
        patientDaoMock.Verify(dao => dao.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }
}