namespace Domain.Models;

public class Patient
{
    public string Name { get; set; }

    public int Id;

    public Patient(){}

    public Patient(string name, int id)
    {
        Name = name;
        this.Id = id;
    }
}