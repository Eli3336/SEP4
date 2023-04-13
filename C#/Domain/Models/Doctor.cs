namespace Domain.Models;

public class Doctor : User
{
    public List<Request> Requests { get; set; }

    public Doctor(string name, string password, string phoneNumber)
        : base(name, password, phoneNumber)
    {
        Requests = new List<Request>();
    }

}