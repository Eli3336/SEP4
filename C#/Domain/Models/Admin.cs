namespace Domain.Models;

public class Admin : User
{
    public Admin(string name, string password, string phoneNumber)
        :base(name, password, phoneNumber, "administrator") {}
}