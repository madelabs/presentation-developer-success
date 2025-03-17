using MediatR;
using BBQ.Application.UseCases.BbqSession.CreateBbqSession.Events;
using BBQ.DataAccess.Repositories;

namespace BBQ.Application.UseCases.BbqSession.CreateBbqSession;

public record CreateBbqSessionCommand(
    string Description,
    Guid UserId,
    Guid TenantId) : IRequest<CreateBbqSessionResponseDto>;

public class CreateBbqSessionCommandHandler : IRequestHandler<CreateBbqSessionCommand, CreateBbqSessionResponseDto>
{
    private readonly IBbqSessionRepository _bbqSessionRepository;

    public CreateBbqSessionCommandHandler(IBbqSessionRepository bbqSessionRepository)
    {
        _bbqSessionRepository = bbqSessionRepository;
    }


    public async Task<CreateBbqSessionResponseDto> Handle(CreateBbqSessionCommand request,
        CancellationToken cancellationToken = default)
    {
        var bbqSession = new DataAccess.Entities.BbqSession()
        {
                Description = request.Description,
                TenantId = request.TenantId,
                UserId = request.UserId
        };

        bbqSession.AddDomainEvent(new BbqSessionCreated(bbqSession));
        var addedBbqSession = await _bbqSessionRepository.AddAsync(bbqSession);

        return new CreateBbqSessionResponseDto
        {
            Id = bbqSession.Id
        };
    }
}
