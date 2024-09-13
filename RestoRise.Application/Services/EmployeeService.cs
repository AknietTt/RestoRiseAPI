using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestoRise.Application.DTOs;
using RestoRise.Application.DTOs.Staff;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Application.Interfaces.Services;
using RestoRise.Application.Options;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOptions<JwtOptions> _options;

    public EmployeeService(IMapper mapper, IUnitOfWork unitOfWork, IUserRepository userRepository,
        IEmployeeRepository employeeRepository, IOptions<JwtOptions> options)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _options = options;
    }

    public async Task<Result<bool>> AddEmployee(StaffRegisterDto dto)
    {
        var employeeRepository = _unitOfWork.GetRepository<Staff>();
        var roleRepository = _unitOfWork.GetRepository<Role>();
        var branchRepository = _unitOfWork.GetRepository<Branch>();

        var role = await roleRepository.GetAsync(dto.RoleId);
        if (role == null) return Result<bool>.Failure("Invalid RoleId. Role not found.", 404);

        var branch = await branchRepository.GetAsync(dto.BranchId);
        if (branch == null) return Result<bool>.Failure("Invalid BranchId. Branch not found.", 404);

        var staff = _mapper.Map<Staff>(dto);

        staff.Roles = new List<Role> { role };
        staff.Branch = branch;

        await employeeRepository.AddAsync(staff);

        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<StaffOutputDto>>> GetStavesByOwner(Guid ownerId)
    {
        if (!await _userRepository.Any(x => x.Id == ownerId))
            return Result<IEnumerable<StaffOutputDto>>.Failure("owner not found", 404);
        var staves = await _employeeRepository.GetAsync(x => x.Branch.Restaurant.Owner.Id == ownerId,
            includeProperties: new[] { "Branch.Restaurant.Owner", "Roles" });
        var result = _mapper.Map<IEnumerable<StaffOutputDto>>(staves);
        return Result<IEnumerable<StaffOutputDto>>.Success(result);
    }

    public async Task<Result<StaffOutputDto>> GetStaffById(Guid staffId)
    {
        if (!await _employeeRepository.Any(x => x.Id == staffId))
            return Result<StaffOutputDto>.Failure("owner not found", 404);
        var staves =
            await _employeeRepository.FirstOrDefault(x => x.Id == staffId,
                new[] { "Branch.Restaurant.Owner", "Roles" });
        var result = _mapper.Map<StaffOutputDto>(staves);
        return Result<StaffOutputDto>.Success(result);
    }

    public async Task<Result<bool>> UpdateStaff(StaffUpdateDto dto)
    {
        var staffRepository = _unitOfWork.GetRepository<Staff>();
        var roleRepository = _unitOfWork.GetRepository<Role>();
        var branchRepository = _unitOfWork.GetRepository<Branch>();

        var staff = await staffRepository.FirstOrDefault(x => x.Id == dto.Id, new[] { "Roles", "Branch" });
        if (staff == null) return Result<bool>.Failure("Staff not found", 404);

        // Update basic staff information
        staff = _mapper.Map<Staff>(dto);
        // Update roles
        var newRoles = await roleRepository.GetAsync(r => dto.Roles.Contains(r.Id));
        if (newRoles.Count() != dto.Roles.Count) return Result<bool>.Failure("One or more roles are invalid", 400);
        staff.Roles = newRoles.ToList();

        // Update branch
        var branch = await branchRepository.GetAsync(dto.Branch);
        if (branch == null) return Result<bool>.Failure("Invalid BranchId. Branch not found.", 404);
        staff.Branch = branch;

        staffRepository.Update(staff);
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<RoleDto>>> GetRoles()
    {
        var roles = await _unitOfWork.GetRepository<Role>().GetAsync();
        var result = _mapper.Map<IEnumerable<RoleDto>>(roles);
        return Result<IEnumerable<RoleDto>>.Success(result);
    }

    public async Task<Result<bool>> DeleteStaff(Guid staffId)
    {
        var staffRepository = _unitOfWork.GetRepository<Staff>();
        var res = await staffRepository.Delete(staffId);
        if (!res) return Result<bool>.Failure("Staff not found", 404);
        _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true, 404);
    }

    public async Task<Result<object>> Authorize(LoginDto loginDto)
    {
        var staffRepository = _unitOfWork.GetRepository<Staff>();
        var staff = await staffRepository.FirstOrDefault(x => x.Email == loginDto.Email, includeProperties:new[] { "Roles" });
        if (staff == null)
            return Result<object>.Failure("Данный пользователь не аутентифицирован в системе", 404);
        
        if (BCrypt.Net.BCrypt.Verify(loginDto.Password, staff.Password) == false)
            return Result<object>.Failure("Данный пользователь не аутентифицирован в системе", 404);
        
        return Result<object>.Success( new
        {
            token=GenerateToken(staff),
            userId= staff.Id,
            roles = staff.Roles.Select(x=>x.Name)
        });
    }
    
    private string GenerateToken(Staff staff)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        var claims = new List<Claim>
        {
            new("id", staff.Id.ToString()),
            new("email", staff.Email),
        };

        foreach (var role in staff.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _options.Value.Issuer,
            audience: _options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

}