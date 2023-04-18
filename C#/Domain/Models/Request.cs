namespace Domain.Models;

public class Request
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Content { get; set; }
    
    public Request(){}

    public Request(string type, string content)
    {
        Type = type;
        Content = content;
    }
}