namespace BBQ.Application.DTOs.User;

public class ConfirmEmailInputDto
{
    public string UserId { get; set; }

    public string Token { get; set; }
}

public class ConfirmEmailResponseDto
{
    public bool Confirmed { get; set; }
}
