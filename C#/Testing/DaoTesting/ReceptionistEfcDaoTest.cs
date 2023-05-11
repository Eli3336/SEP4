using EfcDataAccess;
using Moq;
using NUnit.Framework;

namespace TestingReq5.DaoTesting;

public class ReceptionistEfcDaoTest
{
    private Mock<> x;
    private ReceptionistEfcDao dao;

    [SetUp]
    public void Setup()
    {
        x = new Mock<>();
        dao = new ReceptionistEfcDao(x.Object);
    }
}