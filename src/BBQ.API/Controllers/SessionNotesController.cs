using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBQ.Application.DTOs;
using BBQ.Application.DTOs.SessionNote;
using BBQ.Application.Services;

namespace BBQ.API.Controllers;

[Authorize]
public class SessionNotesController : ApiController
{
    private readonly ISessionNoteService _sessionNoteService;

    public SessionNotesController(ISessionNoteService sessionNoteService)
    {
        _sessionNoteService = sessionNoteService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateSessionNoteInputDto createSessionNoteInputDto)
    {
        return Ok(ApiResult<CreateSessionNoteResponseDto>.Success(
            await _sessionNoteService.CreateAsync(createSessionNoteInputDto)));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateSessionNoteInputDto updateSessionNoteInputDto)
    {
        return Ok(ApiResult<UpdateSessionNoteResponseDto>.Success(
            await _sessionNoteService.UpdateAsync(id, updateSessionNoteInputDto)));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(ApiResult<BaseResponseDto>.Success(await _sessionNoteService.DeleteAsync(id)));
    }
}
