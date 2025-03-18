using AutoMapper;
using FluentValidation;
using BBQ.Application.Common.DTO;
using BBQ.Application.UseCases.SessionNote.CreateSessionNote;
using BBQ.Application.UseCases.SessionNote.GetAllByList;
using BBQ.Application.UseCases.SessionNote.UpdateSessionNote;
using BBQ.DataAccess.Repositories;

namespace BBQ.Application.UseCases.SessionNote;

public class SessionNoteService : ISessionNoteService
{
    private readonly IMapper _mapper;
    private readonly ISessionNotesRepository _sessionNotesRepository;
    private readonly IBbqSessionsRepository _bbqSessionsRepository;

    private const int MinimumNoteLength = 5;
    private const int MaximumNoteLength = 50;
    private const int MinimumActivityDescriptionLength = 5;
    private const int MaximumActivityDescriptionLength = 100;

    public SessionNoteService(ISessionNotesRepository sessionNotesRepository,
        IBbqSessionsRepository bbqSessionsRepository,
        IMapper mapper)
    {
        _sessionNotesRepository = sessionNotesRepository;
        _bbqSessionsRepository = bbqSessionsRepository;
        _mapper = mapper;
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
        if (createSessionNoteInputDto.Note.Length < MinimumNoteLength)
        {
            throw new ValidationException($"Session Note should have minimum of {MinimumNoteLength} characters");
        }

        if (createSessionNoteInputDto.Note.Length > MaximumNoteLength)
        {
            throw new ValidationException($"\"Session Note should have maximum of {MaximumNoteLength} characters");
        }

        if (createSessionNoteInputDto.ActivityDescription.Length < MinimumActivityDescriptionLength)
        {
            throw new ValidationException($"Session Activity Description should have minimum of {MinimumActivityDescriptionLength} characters");
        }

        if (createSessionNoteInputDto.ActivityDescription.Length > MaximumActivityDescriptionLength)
        {
            throw new ValidationException($"\"Session Activity Description should have maximum of {MaximumActivityDescriptionLength} characters");
        }

        var bbqSession = await _bbqSessionsRepository.GetFirstAsync(tl => tl.Id == createSessionNoteInputDto.BbqSessionId);
        var sessionNote = _mapper.Map<DataAccess.Entities.SessionNote>(createSessionNoteInputDto);

        sessionNote.Session = bbqSession;

        return new CreateSessionNoteResponseDto
        {
            Id = (await _sessionNotesRepository.AddAsync(sessionNote)).Id
        };
    }

    public async Task<UpdateSessionNoteResponseDto> UpdateAsync(Guid id, UpdateSessionNoteInputDto updateSessionNoteInputDto,
        CancellationToken cancellationToken = default)
    {
        if (updateSessionNoteInputDto.Note.Length < MinimumNoteLength)
        {
            throw new ValidationException($"Session Note should have minimum of {MinimumNoteLength} characters");
        }

        if (updateSessionNoteInputDto.Note.Length > MaximumNoteLength)
        {
            throw new ValidationException($"\"Session Note should have maximum of {MaximumNoteLength} characters");
        }

        if (updateSessionNoteInputDto.ActivityDescription.Length < MinimumActivityDescriptionLength)
        {
            throw new ValidationException($"Session Activity Description should have minimum of {MinimumActivityDescriptionLength} characters");
        }

        if (updateSessionNoteInputDto.ActivityDescription.Length > MaximumActivityDescriptionLength)
        {
            throw new ValidationException($"\"Session Activity Description should have maximum of {MaximumActivityDescriptionLength} characters");
        }

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
