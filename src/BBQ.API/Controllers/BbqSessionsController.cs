using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBQ.Application.DTOs;
using BBQ.Application.DTOs.BbqSession;
using BBQ.Application.DTOs.SessionNote;
using BBQ.Application.Services;

namespace BBQ.API.Controllers;

[Authorize]
public class BbqSessionsController : ApiController
{
    private readonly IBbqService _bbqSessionService;

    public BbqSessionsController(IBbqService bbqSessionService)
    {
        _bbqSessionService = bbqSessionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(ApiResult<IEnumerable<BbqSessionResponseDto>>.Success(await _bbqSessionService.GetAllAsync()));
    }

    [HttpGet("{id:guid}/notes")]
    public async Task<IActionResult> GetAllSessionNotesAsync(Guid id)
    {
        return Ok(ApiResult<IEnumerable<SessionNoteResponseDto>>.Success(
            await _bbqSessionService.GetAllByBbqSessionIdAsync(id)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBbqSessionInputDto createBbqSessionInputDto)
    {
        return Ok(ApiResult<CreateBbqSessionResponseDto>.Success(
            await _bbqSessionService.CreateAsync(createBbqSessionInputDto)));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateBbqSessionInputDto updateBbqSessionInputDto)
    {
        return Ok(ApiResult<UpdateBbqSessionResponseDto>.Success(
            await _bbqSessionService.UpdateAsync(id, updateBbqSessionInputDto)));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        return Ok(ApiResult<BaseResponseDto>.Success(await _bbqSessionService.DeleteAsync(id)));
    }
}
