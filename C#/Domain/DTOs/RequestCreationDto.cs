namespace Domain.DTOs;

public class RequestCreationDto
{
    public string Type { get; set; }
    public string Content { get; set; }
    
    public RequestCreationDto(string type, string content)
    {
        Type = type;
        Content = content;
    }
    public RequestCreationDto() {}
}