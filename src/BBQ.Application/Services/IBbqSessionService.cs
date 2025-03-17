using BBQ.Application.DTOs;
using BBQ.Application.DTOs.BbqSession;

namespace BBQ.Application.Services;

public interface IBbqSessionService
{
    Task<CreateBbqSessionResponseDto> CreateAsync(CreateBbqSessionInputDto createBbqSessionInputDto);

    Task<BaseResponseDto> DeleteAsync(Guid id);

    Task<IEnumerable<BbqSessionResponseDto>> GetAllAsync();

    Task<UpdateBbqSessionResponseDto> UpdateAsync(Guid id, UpdateBbqSessionInputDto updateBbqSessionInputDto);
}
