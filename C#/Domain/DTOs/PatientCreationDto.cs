namespace Domain.DTOs;

public class PatientCreationDto
{
    public string Name { get; set; }

    public PatientCreationDto(){}

    public PatientCreationDto(string name)
    {
        Name = name;
    }
}