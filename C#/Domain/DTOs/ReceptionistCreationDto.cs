namespace Domain.DTOs;

public class ReceptionistCreationDto
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
   

    public ReceptionistCreationDto(string name, string password, string phoneNumber)
    {
        Name = name;
        Password = password;
        PhoneNumber = phoneNumber;
    }
    public ReceptionistCreationDto() {}
}