using BBQ.Application.Common.DTO;
using BBQ.DataAccess.ValueObjects;

namespace BBQ.Application.UseCases.SessionNote.CreateSessionNote;

public class CreateSessionNoteInputDto
{
    public Guid BbqSessionId { get; set; }

    public string ActivityDescription { get; set; }

    public string Note { get; set; }
    
    public PitTemperature PitTemperature { get; set; }
    
    public MeatTemperature MeatTemperature { get; set; }
}
