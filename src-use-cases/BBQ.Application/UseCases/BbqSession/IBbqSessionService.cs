using BBQ.Application.Common.DTO;
using BBQ.Application.UseCases.BbqSession.CreateBbqSession;
using BBQ.Application.UseCases.BbqSession.GetAll;
using BBQ.Application.UseCases.BbqSession.UpdateBbqSession;

namespace BBQ.Application.UseCases.BbqSession;

public interface IBbqSessionService
{
    Task<CreateBbqSessionResponseDto> CreateAsync(CreateBbqSessionInputDto createBbqSessionInputDto);

    Task<BaseResponseDto> DeleteAsync(Guid id);

    Task<IEnumerable<BbqSessionResponseDto>> GetAllAsync();

    Task<UpdateBbqSessionResponseDto> UpdateAsync(Guid id, UpdateBbqSessionInputDto updateBbqSessionInputDto);
}
