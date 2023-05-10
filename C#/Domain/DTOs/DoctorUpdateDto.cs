namespace Domain.DTOs;

public class DoctorUpdateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    
    public DoctorUpdateDto(){}

    public DoctorUpdateDto(int id, string? name, string? password, string? phoneNumber)
    {
        Id = id;
        Name = name;
        Password = password;
        PhoneNumber = phoneNumber;
    }
}