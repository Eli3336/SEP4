namespace Domain.DTOs;

public class SearchUserParameterDto
{
    public string? UsernameContains { get; set; }

    public SearchUserParameterDto(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
}