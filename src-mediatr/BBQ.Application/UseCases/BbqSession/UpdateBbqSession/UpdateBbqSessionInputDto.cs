using BBQ.Application.Common.DTO;
using BBQ.Application.ValueObjects;

namespace BBQ.Application.UseCases.BbqSession.UpdateBbqSession;

public class UpdateBbqSessionInputDto
{
    public string Description { get; set; }
    
    public string Result { get; set; }
    public UserId UserId { get; set; }
    public TenantId TenantId { get; set; }
}

public class UpdateBbqSessionResponseDto : BaseResponseDto { }
