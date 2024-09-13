using RestoRise.Application.DTOs;
using RestoRise.Application.DTOs.Staff;
using RestoRise.Domain.Common;

namespace RestoRise.Application.Interfaces.Services;

public interface IEmployeeService
{
    Task<Result<bool>> AddEmployee(StaffRegisterDto dto);
    Task<Result<IEnumerable<StaffOutputDto>>> GetStavesByOwner(Guid ownerId);
    Task<Result<StaffOutputDto>> GetStaffById(Guid staffId);
    Task<Result<bool>> UpdateStaff(StaffUpdateDto dto);
    Task<Result<IEnumerable<RoleDto>>> GetRoles();
    Task<Result<bool>> DeleteStaff(Guid staffId);
    Task<Result<object>> Authorize(LoginDto loginDto);
}