namespace Domain.Models;

public class Receptionist : User
{
    public Receptionist(string name, string password, string phoneNumber)
        :base(name, password, phoneNumber, "receptionist") {}
    
    public Receptionist(): base("bob", "bob", "000000000","receptionist"){}
}