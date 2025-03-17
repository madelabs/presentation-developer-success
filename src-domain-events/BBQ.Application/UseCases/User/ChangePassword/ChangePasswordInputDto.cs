namespace BBQ.Application.UseCases.User.ChangePassword;

public class ChangePasswordInputDto
{
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }
}
