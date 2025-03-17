using MediatR;
using BBQ.Application.Common.DTO;
using BBQ.DataAccess.Repositories;
using BBQ.DataAccess.Services;

namespace BBQ.Application.UseCases.BbqSession.DeleteBbqSession;

public record DeleteBbqSessionCommand(
    Guid Id) : IRequest<BaseResponseDto>;

public class DeleteBbqSessionCommandHandler : IRequestHandler<DeleteBbqSessionCommand, BaseResponseDto>
{
    private readonly IBbqSessionRepository _bbqSessionRepository;
    private readonly IClaimService _claimService;

    public DeleteBbqSessionCommandHandler(IBbqSessionRepository bbqSessionRepository, IClaimService claimService)
    {
        _bbqSessionRepository = bbqSessionRepository;
        _claimService = claimService;
    }
    
    public async Task<BaseResponseDto> Handle(DeleteBbqSessionCommand request,
        CancellationToken cancellationToken = default)
    {
        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == request.Id);

        return new BaseResponseDto
        {
            Id = (await _bbqSessionRepository.DeleteAsync(bbqSession)).Id
        };
    }
}
