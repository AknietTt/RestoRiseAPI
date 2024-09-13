using RestoRise.Application.DTOs.Branch;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;
public interface IBranchService
{
    Task<Result<Guid>> CreateBranch(BranchCreateDto dto);
    Task<Result<IEnumerable<BranchOutputDto>>> GetAllBranch();
    Task<Result<BranchUpdateDto>> UpdateBranch(BranchUpdateDto dto);
    Task<Result<bool>> DeleteBranch(Guid id);
    Task<Result<IEnumerable<BranchOutputDto>>> GetByRestaurant(Guid restaurantId);
    Task<Result<BranchUpdateDto>> GetBranchById(Guid id);
    Task<Result<IEnumerable<BranchOutputDto>>> GetByOwner(Guid ownerId);
}