using Domain.Models;

namespace Domain.DTOs;

public class DoctorCreationDto
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
   

    public DoctorCreationDto(string name, string password, string phoneNumber)
    {
        Name = name;
        Password = password;
        PhoneNumber = phoneNumber;
    }
    public DoctorCreationDto() {}
}