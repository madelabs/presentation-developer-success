namespace BBQ.Application.DTOs.User;

public class CreateUserDto
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}

public class CreateUserResponseDto : BaseResponseDto { }
