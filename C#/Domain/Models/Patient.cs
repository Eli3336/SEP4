namespace Domain.Models;

public class Patient
{
     public int Id { get; set; }
    public string Name { get; set; }
    
    public Patient(){}

    public Patient(string name)
    {
        Name = name;
    }
}