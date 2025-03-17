using AutoMapper;
using FluentValidation;
using BBQ.Application.DTOs;
using BBQ.Application.DTOs.SessionNote;
using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Repositories;

namespace BBQ.Application.Services;

public class SessionNoteService : ISessionNoteService
{
    private readonly IMapper _mapper;
    private readonly ISessionNotesRepository _sessionNotesRepository;
    private readonly IBbqSessionRepository _bbqSessionRepository;
    private readonly IValidator<CreateSessionNoteInputDto> _createSessionNoteValidator;
    private readonly IValidator<UpdateSessionNoteInputDto> _updateSessionNoteValidator;
    
    public SessionNoteService(ISessionNotesRepository sessionNotesRepository,
        IBbqSessionRepository bbqSessionRepository,
        IMapper mapper,
        IValidator<CreateSessionNoteInputDto> createSessionNoteValidator,
        IValidator<UpdateSessionNoteInputDto> updateSessionNoteValidator)
    {
        _sessionNotesRepository = sessionNotesRepository;
        _bbqSessionRepository = bbqSessionRepository;
        _mapper = mapper;
        _createSessionNoteValidator = createSessionNoteValidator;
        _updateSessionNoteValidator = updateSessionNoteValidator;
    }

    public async Task<IEnumerable<SessionNoteResponseDto>> GetAllByBbqSessionIdAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
 
        var sessionNotes = await _sessionNotesRepository.GetAllAsync(ti => ti.Session.Id == id);

        return _mapper.Map<IEnumerable<SessionNoteResponseDto>>(sessionNotes);
    }

    public async Task<CreateSessionNoteResponseDto> CreateAsync(CreateSessionNoteInputDto createSessionNoteInputDto,
        CancellationToken cancellationToken = default)
    {
        await _createSessionNoteValidator.ValidateAndThrowAsync(createSessionNoteInputDto, cancellationToken);
        
        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == createSessionNoteInputDto.BbqSessionId);
        var sessionNote = _mapper.Map<SessionNote>(createSessionNoteInputDto);

        sessionNote.Session = bbqSession;

        return new CreateSessionNoteResponseDto
        {
            Id = (await _sessionNotesRepository.AddAsync(sessionNote)).Id
        };
    }

    public async Task<UpdateSessionNoteResponseDto> UpdateAsync(Guid id, UpdateSessionNoteInputDto updateSessionNoteInputDto,
        CancellationToken cancellationToken = default)
    {
        await _updateSessionNoteValidator.ValidateAndThrowAsync(updateSessionNoteInputDto, cancellationToken);
        
        var sessionNote = await _sessionNotesRepository.GetFirstAsync(ti => ti.Id == id);

        _mapper.Map(updateSessionNoteInputDto, sessionNote);

        return new UpdateSessionNoteResponseDto
        {
            Id = (await _sessionNotesRepository.UpdateAsync(sessionNote)).Id
        };
    }

    public async Task<BaseResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sessionNote = await _sessionNotesRepository.GetFirstAsync(ti => ti.Id == id);

        return new BaseResponseDto
        {
            Id = (await _sessionNotesRepository.DeleteAsync(sessionNote)).Id
        };
    }
}
