using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBQ.Application.Common.DTO;
using BBQ.Application.UseCases.SessionNote.CreateSessionNote;
using BBQ.Application.UseCases.SessionNote.DeleteSessionNote;
using BBQ.Application.UseCases.SessionNote.UpdateSessionItem;

namespace BBQ.API.Controllers;

[Authorize]
public class SessionNotesController : ApiController
{
    private readonly ISender _mediator;

    public SessionNotesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateSessionNoteCommand command)
    {
        return Ok(ApiResult<CreateSessionNoteResponseDto>.Success(
            await _mediator.Send(command)));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateSessionNoteInputDto updateSessionNoteInputDto)
    {
        var command = new UpdateSessionNoteCommand(id, updateSessionNoteInputDto.ActivityDescription, updateSessionNoteInputDto.Note,
            updateSessionNoteInputDto.PitTemperature);
        
        return Ok(ApiResult<UpdateSessionNoteResponseDto>.Success(
            await _mediator.Send(command)));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(DeleteSessionNoteCommand command)
    {
        return Ok(ApiResult<BaseResponseDto>.Success(await _mediator.Send(command)));
    }
}
