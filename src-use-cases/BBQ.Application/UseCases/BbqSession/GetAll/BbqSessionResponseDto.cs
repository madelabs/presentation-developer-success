using BBQ.Application.Common.DTO;

namespace BBQ.Application.UseCases.BbqSession.GetAll;

public class BbqSessionResponseDto : BaseResponseDto
{
    public string Description { get; set; }
    
    public string Result { get; set; }
    
    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
