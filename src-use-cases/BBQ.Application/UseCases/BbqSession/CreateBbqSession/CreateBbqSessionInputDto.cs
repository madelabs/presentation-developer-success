using BBQ.Application.Common.DTO;

namespace BBQ.Application.UseCases.BbqSession.CreateBbqSession;

public class CreateBbqSessionInputDto
{
    public string Description { get; set; }
}

public class CreateBbqSessionResponseDto : BaseResponseDto { }
