namespace Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }

    public User(){}

    public User(string name, string password, string phoneNumber, string role)
    {
        Name = name;
        Password = password;
        PhoneNumber = phoneNumber;
        Role = role;
    }
}