using BBQ.Application.Common.DTO;

namespace BBQ.Application.UseCases.SessionNote.CreateSessionNote;

public class CreateSessionNoteInputDto
{
    public Guid BbqSessionId { get; set; }

    public string ActivityDescription { get; set; }

    public string Note { get; set; }
    
    public decimal PitTemperature { get; set; }
    
    public decimal MeatTemperature { get; set; }
}

public class CreateSessionNoteResponseDto : BaseResponseDto { }
