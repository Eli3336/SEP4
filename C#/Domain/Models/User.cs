namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }

    public User(){}

    public User(string name, string password, string phoneNumber)
    {
        Name = name;
        Password = password;
        PhoneNumber = phoneNumber;
    }
}