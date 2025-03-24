using MediatR;
using BBQ.Application.Common.Exceptions;
using BBQ.DataAccess.Repositories;
using BBQ.DataAccess.Services;

namespace BBQ.Application.UseCases.BbqSession.UpdateBbqSession;

public record UpdateBbqSessionCommand(
    Guid Id,
    string Description,
    string Result) : IRequest<UpdateBbqSessionResponseDto>;

public class UpdateBbqSessionCommandHandler : IRequestHandler<UpdateBbqSessionCommand, UpdateBbqSessionResponseDto>
{
    private readonly IBbqSessionRepository _bbqSessionRepository;
    private readonly IClaimService _claimService;

    public UpdateBbqSessionCommandHandler(IBbqSessionRepository bbqSessionRepository, IClaimService claimService)
    {
        _bbqSessionRepository = bbqSessionRepository;
        _claimService = claimService;
    }


    public async Task<UpdateBbqSessionResponseDto> Handle(UpdateBbqSessionCommand request,
        CancellationToken cancellationToken = default)
    {
        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == request.Id);

        var userId = _claimService.GetUserId();

        if (userId != bbqSession.CreatedBy)
            throw new BadRequestException("The selected list does not belong to you");

        bbqSession.Description = request.Description;
        bbqSession.Result = request.Result;

        return new UpdateBbqSessionResponseDto
        {
            Id = (await _bbqSessionRepository.UpdateAsync(bbqSession)).Id
        };
    }
}
