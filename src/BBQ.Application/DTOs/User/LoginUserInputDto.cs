namespace BBQ.Application.DTOs.User;

public class LoginUserInputDto
{
    public string Username { get; set; }

    public string Password { get; set; }
}

public class LoginResponseDto
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Token { get; set; }
}
