using BBQ.Application.Common.DTO;

namespace BBQ.Application.UseCases.BbqSession.UpdateBbqSession;

public class UpdateBbqSessionInputDto
{
    public string Description { get; set; }
    
    public string Result { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
}

public class UpdateBbqSessionResponseDto : BaseResponseDto { }
