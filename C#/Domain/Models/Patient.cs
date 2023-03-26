namespace Domain.Models;

public class Patient
{
    public string Name { get; set; }

    public Patient(){}

    public Patient(string name)
    {
        Name = name;
    }
}