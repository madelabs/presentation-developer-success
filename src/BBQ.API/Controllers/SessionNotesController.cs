using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBQ.Application.DTOs;
using BBQ.Application.DTOs.SessionNote;
using BBQ.Application.Services;

namespace BBQ.API.Controllers;

[Authorize]
public class SessionNotesController : ApiController
{
    private readonly IBbqService _bbqService;

    public SessionNotesController(IBbqService sessionNoteService)
    {
        _bbqService = sessionNoteService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateSessionNoteInputDto createSessionNoteInputDto)
    {
        return Ok(ApiResult<CreateSessionNoteResponseDto>.Success(
            await _bbqService.CreateAsync(createSessionNoteInputDto)));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateSessionNoteInputDto updateSessionNoteInputDto)
    {
        return Ok(ApiResult<UpdateSessionNoteResponseDto>.Success(
            await _bbqService.UpdateAsync(id, updateSessionNoteInputDto)));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(ApiResult<BaseResponseDto>.Success(await _bbqService.DeleteAsync(id)));
    }
}
