using AutoMapper;
using FluentValidation;
using BBQ.Application.DTOs;
using BBQ.Application.DTOs.BbqSession;
using BBQ.Application.Exceptions;
using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Repositories;
using BBQ.DataAccess.Services;

namespace BBQ.Application.Services;

public class BbqSessionService : IBbqSessionService
{
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;
    private readonly IBbqSessionRepository _bbqSessionRepository;
    private readonly IValidator<CreateBbqSessionInputDto> _createBbqSessionValidator;
    private readonly IValidator<UpdateBbqSessionInputDto> _updateBbqSessionValidator;

    public BbqSessionService(IBbqSessionRepository bbqSessionRepository, IMapper mapper, IClaimService claimService, IValidator<CreateBbqSessionInputDto> createBbqSessionValidator, IValidator<UpdateBbqSessionInputDto> updateBbqSessionValidator)
    {
        _bbqSessionRepository = bbqSessionRepository;
        _mapper = mapper;
        _claimService = claimService;
        _createBbqSessionValidator = createBbqSessionValidator;
        _updateBbqSessionValidator = updateBbqSessionValidator;
    }

    public async Task<IEnumerable<BbqSessionResponseDto>> GetAllAsync()
    {
        var currentUserId = _claimService.GetUserId();

        var bbqSessions = await _bbqSessionRepository.GetAllAsync(tl => tl.CreatedBy == currentUserId);

        return _mapper.Map<IEnumerable<BbqSessionResponseDto>>(bbqSessions);
    }

    public async Task<CreateBbqSessionResponseDto> CreateAsync(CreateBbqSessionInputDto createBbqSessionInputDto)
    {
        await _createBbqSessionValidator.ValidateAndThrowAsync(createBbqSessionInputDto);
        
        var bbqSession = _mapper.Map<BbqSession>(createBbqSessionInputDto);

        bbqSession.CreatedBy = _claimService.GetUserId();
        bbqSession.CreatedOn = DateTime.Now;
        
        var addedBbqSession = await _bbqSessionRepository.AddAsync(bbqSession);

        return new CreateBbqSessionResponseDto
        {
            Id = bbqSession.Id
        };
    }

    public async Task<UpdateBbqSessionResponseDto> UpdateAsync(Guid id, UpdateBbqSessionInputDto updateBbqSessionInputDto)
    {
        await _updateBbqSessionValidator.ValidateAndThrowAsync(updateBbqSessionInputDto);
        
        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == id);

        var userId = _claimService.GetUserId();

        if (userId != bbqSession.CreatedBy)
            throw new BadRequestException("The selected list does not belong to you");

        bbqSession.Description = updateBbqSessionInputDto.Description;

        return new UpdateBbqSessionResponseDto
        {
            Id = (await _bbqSessionRepository.UpdateAsync(bbqSession)).Id
        };
    }

    public async Task<BaseResponseDto> DeleteAsync(Guid id)
    {
        var bbqSession = await _bbqSessionRepository.GetFirstAsync(tl => tl.Id == id);

        return new BaseResponseDto
        {
            Id = (await _bbqSessionRepository.DeleteAsync(bbqSession)).Id
        };
    }
}
