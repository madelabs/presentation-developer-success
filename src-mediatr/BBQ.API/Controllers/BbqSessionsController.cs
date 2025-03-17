using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BBQ.Application.Common.DTO;
using BBQ.Application.UseCases.BbqSession.CreateBbqSession;
using BBQ.Application.UseCases.BbqSession.DeleteBbqSession;
using BBQ.Application.UseCases.BbqSession.GetAll;
using BBQ.Application.UseCases.BbqSession.UpdateBbqSession;
using BBQ.Application.UseCases.SessionNote.GetAllByList;

namespace BBQ.API.Controllers;

[Authorize]
public class BbqSessionsController : ApiController
{
    private readonly ISender _mediator;

    public BbqSessionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(ApiResult<IEnumerable<BbqSessionResponseDto>>.Success(await _mediator.Send(new GetAllBbqSessionQuery())));
    }

    [HttpGet("{id:guid}/notes")]
    public async Task<IActionResult> GetAllSessionNotesAsync(GetAllByBbqSessionQuery query)
    {
        return Ok(ApiResult<IEnumerable<SessionNoteResponseDto>>.Success(
            await _mediator.Send(query)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBbqSessionCommand command)
    {
        return Ok(ApiResult<CreateBbqSessionResponseDto>.Success(
            await _mediator.Send(command)));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateBbqSessionInputDto updateBbqSessionInputDto)
    {
        var command = new UpdateBbqSessionCommand(id, updateBbqSessionInputDto.Description, updateBbqSessionInputDto.Result, updateBbqSessionInputDto.UserId,
            updateBbqSessionInputDto.TenantId);
        
        return Ok(ApiResult<UpdateBbqSessionResponseDto>.Success(
            await _mediator.Send(command)));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(DeleteBbqSessionCommand command)
    {
        return Ok(ApiResult<BaseResponseDto>.Success(await _mediator.Send(command)));
    }
}
