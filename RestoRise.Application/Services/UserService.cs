using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.BuisnessLogic.Interfaces;
using RestoRise.BuisnessLogic.Options;
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

    public async Task<Result<string>> Authorize(LoginDto loginDto)
    {
        var userRepository = _unitOfWork.GetRepository<User>();
        var user = await userRepository.FirstOrDefault(x => x.Email == loginDto.Email);
        if (user == null)
            return Result<string>.Failure("Данный пользователь не аутентифицирован в системе", 404);
        
        if (BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password) == false)
            return Result<string>.Failure("Данный пользователь не аутентифицирован в системе", 404);
        
        return Result<string>.Success(GenerateToken(user));

    }
    
    private string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        var claims = new List<Claim>
        {
            new("id", user.Id.ToString()),
            new("email", user.Email)
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