using MediatR;
using BBQ.DataAccess.Repositories;
using BBQ.DataAccess.Services;

namespace BBQ.Application.UseCases.BbqSession.GetAll;

public record GetAllBbqSessionQuery() : IRequest<IEnumerable<BbqSessionResponseDto>>;

public class GetAllBbqSessionQueryHandler : IRequestHandler<GetAllBbqSessionQuery, IEnumerable<BbqSessionResponseDto>>
{
    private readonly IClaimService _claimService;
    private readonly IBbqSessionRepository _bbqSessionRepository;

    public GetAllBbqSessionQueryHandler(IClaimService claimService, IBbqSessionRepository bbqSessionRepository)
    {
        _claimService = claimService;
        _bbqSessionRepository = bbqSessionRepository;
    }


    public async Task<IEnumerable<BbqSessionResponseDto>> Handle(GetAllBbqSessionQuery request,
        CancellationToken cancellationToken = default)
    {
        var currentUserId = _claimService.GetUserId();

        var bbqSessions = await _bbqSessionRepository.GetAllAsync(tl => tl.CreatedBy == currentUserId);

        var result = new List<BbqSessionResponseDto>();
        foreach (var bbqSession in bbqSessions)
        {
            result.Add(new BbqSessionResponseDto()
            {
                Id = bbqSession.Id,
                Description = bbqSession.Description,
                Result = bbqSession.Result,
                CreatedBy = bbqSession.CreatedBy,
                CreatedOn = bbqSession.CreatedOn,
                UpdatedBy = bbqSession.UpdatedBy,
                UpdatedOn = bbqSession.UpdatedOn
            });
        }
        
        return result;
    }
}
