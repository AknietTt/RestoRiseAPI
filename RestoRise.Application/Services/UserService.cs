using AutoMapper;
using RestoRise.BuisnessLogic.DTOs;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.BuisnessLogic.Interfaces;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class UserService: IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
}