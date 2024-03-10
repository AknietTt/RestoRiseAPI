using RestoRise.BuisnessLogic.DTOs.Branch;
using RestoRise.Domain.Common;

namespace RestoRise.BuisnessLogic.Interfaces;

public interface IBranchService
{
    Task<Result<Guid>> CreateBranch(BranchCreateDto dto);
    Task<Result<IEnumerable<BranchOutputDto>>> GetAllBranch();
    Task<Result<BranchUpdateDto>> UpdateBranch(BranchUpdateDto dto);
    Task<Result<bool>> DeleteBranch(Guid id);
}