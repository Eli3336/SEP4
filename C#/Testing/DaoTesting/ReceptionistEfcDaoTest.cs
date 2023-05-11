using EfcDataAccess;
using Moq;
using NUnit.Framework;

namespace TestingReq5.DaoTesting;

public class ReceptionistEfcDaoTest
{
    private Mock<HospitalContext> context;
    private ReceptionistEfcDao dao;

    [SetUp]
    public void Setup()
    {
        context = new Mock<HospitalContext>();
        dao = new ReceptionistEfcDao(context.Object);
    }
    
}