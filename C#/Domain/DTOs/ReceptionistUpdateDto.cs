namespace Domain.DTOs;

public class ReceptionistUpdateDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
   

    public ReceptionistUpdateDto(string name,  string phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
    }
    public ReceptionistUpdateDto() {}
}