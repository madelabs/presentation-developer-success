﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBQ.Application.Common.DTO;
using BBQ.Application.UseCases.BbqSession;
using BBQ.Application.UseCases.BbqSession.CreateBbqSession;
using BBQ.Application.UseCases.BbqSession.GetAll;
using BBQ.Application.UseCases.BbqSession.UpdateBbqSession;
using BBQ.Application.UseCases.SessionNote;
using BBQ.Application.UseCases.SessionNote.GetAllByList;

namespace BBQ.API.Controllers;

[Authorize]
public class BbqSessionsController : ApiController
{
    private readonly ISessionNoteService _sessionNoteService;
    private readonly IBbqSessionService _bbqSessionService;

    public BbqSessionsController(IBbqSessionService bbqSessionService, ISessionNoteService sessionNoteService)
    {
        _bbqSessionService = bbqSessionService;
        _sessionNoteService = sessionNoteService;
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
            await _sessionNoteService.GetAllByBbqSessionIdAsync(id)));
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
