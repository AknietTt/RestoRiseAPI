using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestoRise.Application.DTOs;
using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Application.Interfaces.Services;
using RestoRise.Application.Options;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class UserService: IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOptions<JwtOptions> _options;


    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtOptions> options)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _options = options;
    }
    public async Task<Result<Guid>> Register(UserCreateDto registrationDTo)
    {
        var userRepository = _unitOfWork.GetRepository<User>();
        var user = await userRepository.FirstOrDefault(u => u.Email == registrationDTo.Email);
        if (user != null)
        {
            return Result<Guid>.Failure("Данный пользователь уже зарегистрирован в системе", 409);
        }
        var userSave = _mapper.Map<User>(registrationDTo);
        userSave.Password = BCrypt.Net.BCrypt.HashPassword(userSave.Password, BCrypt.Net.BCrypt.GenerateSalt());
        
        await userRepository.AddAsync(userSave);
        await _unitOfWork.SaveChangesAsync();
        
        return Result<Guid>.Success(userSave.Id, 201);
    }

    public async Task<Result<object>> Authorize(LoginDto loginDto)
    {
        var userRepository = _unitOfWork.GetRepository<User>();
        var user = await userRepository.FirstOrDefault(x => x.Email == loginDto.Email);
        if (user == null)
            return Result<object>.Failure("Данный пользователь не аутентифицирован в системе", 404);
        
        if (BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password) == false)
            return Result<object>.Failure("Данный пользователь не аутентифицирован в системе", 404);
        
        return Result<object>.Success( new
        {
            token=GenerateToken(user),
            userId= user.Id
        });

    }

    public async Task<Result<UserDto>> GetUserInfo(Guid userId)
    {
        var user = await  _unitOfWork.GetRepository<User>().FirstOrDefault(x => x.Id == userId);
        if (user == null)
        {
            var staff = await  _unitOfWork.GetRepository<Staff>().FirstOrDefault(x => x.Id == userId, includeProperties: new []{"Roles"});
            if (staff == null)
            {
                return Result<UserDto>.Failure("Пользователь не найдено", 404);
            }
            return Result<UserDto>.Success( _mapper.Map<UserDto>(staff));
        }

        var result = _mapper.Map<UserDto>(user);
        result.Roles = ["Owner"];
        return Result<UserDto>.Success( result);
    }

    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("email", user.Email),
            new (ClaimTypes.Role , "Owner")
        };

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