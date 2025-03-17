namespace BBQ.Application.DTOs.SessionNote;

public class UpdateSessionNoteInputDto
{
    public Guid BbqSessionId { get; set; }
 
    public string ActivityDescription { get; set; }
    
    public string Note { get; set; }

    public decimal PitTemperature { get; set; }
}

public class UpdateSessionNoteResponseDto : BaseResponseDto { }
