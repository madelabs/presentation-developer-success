using BBQ.Application.Common.DTO;
using BBQ.DataAccess.ValueObjects;

namespace BBQ.Application.UseCases.SessionNote.GetAllByList;

public class SessionNoteResponseDto : BaseResponseDto
{
    public string ActivityDescription { get; set; }

    public string Note { get; set; }

    public PitTemperature PitTemperature { get; set; }
    
    public MeatTemperature MeatTemperature { get; set; }
    
    public string CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
