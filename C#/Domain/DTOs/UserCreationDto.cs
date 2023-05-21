namespace Shared.DTOs;

public class UserCreationDto
{
    public string Name { get;set;}
    public string PhoneNumber { get; set; }
    public string UserName { get;set;}
    public string Password { get; set; }

    public UserCreationDto(string name, string phoneNumber, string username, string password)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        UserName = username;
        Password = password;
    }
}