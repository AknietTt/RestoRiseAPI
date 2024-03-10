using AutoMapper;
using RestoRise.BuisnessLogic.DTOs.Branch;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.BuisnessLogic.Interfaces;
using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.Services;

public class BranchService:IBranchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public BranchService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> CreateBranch(BranchCreateDto dto)
    {
        try
        {
            var branchRepository =  _unitOfWork.GetRepository<Branch>();
            var restaurantRepository =  _unitOfWork.GetRepository<Restaurant>();
            var cityRepository = _unitOfWork.GetRepository<City>();
        
            var branch = _mapper.Map<Branch>(dto);
            
            var restaurant =  await restaurantRepository.GetAsync(dto.RestaurantId);
            var city = await cityRepository.GetAsync(dto.CityId);
            
            restaurantRepository.Attach(restaurant);
            cityRepository.Attach(city);

            branch.Restaurant = restaurant;
            branch.City = city;
            
            await branchRepository.AddAsync(branch);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(branch.Id, 201);
        }
        catch (Exception e)
        {
            return Result<Guid>.Failure(e.Message, 500);
        }
        
    }

    public async Task<Result<IEnumerable<BranchOutputDto>>> GetAllBranch()
    {
        var repository = _unitOfWork.GetRepository<Branch>();
        var branches = await repository.GetAsync(includeProperties: new[] { "City", "Restaurant" });
        var branchesDto =  _mapper.Map<IEnumerable<BranchOutputDto>>(branches);
       return Result<IEnumerable<BranchOutputDto>>.Success(branchesDto);
    }

    public async Task<Result<BranchUpdateDto>> UpdateBranch(BranchUpdateDto dto)
    {
        var branchRepository = _unitOfWork.GetRepository<Branch>();
        var cityRepository = _unitOfWork.GetRepository<City>();
        var restaurnatRepository = _unitOfWork.GetRepository<Restaurant>();
        
        var branch = await branchRepository.GetAsync(dto.Id);
        var city = await cityRepository.FirstOrDefault(x=>x.Id == dto.CityId);
        var restaurant = await restaurnatRepository.FirstOrDefault(r =>r.Id ==dto.RestaurantId );
        
        cityRepository.Attach(city);
        restaurnatRepository.Attach(restaurant);

        branch.Restaurant = restaurant;
        branch.City = city;
        branch.Address = dto.Address;
        
        branchRepository.Update(branch);
        await _unitOfWork.SaveChangesAsync();

        return Result<BranchUpdateDto>.Success(dto);
    }

    public async Task<Result<bool>> DeleteBranch(Guid id)
    { 
        await _unitOfWork.GetRepository<Branch>().Delete(id);
        await  _unitOfWork.SaveChangesAsync();
        return Result<bool>.Success(true, 200);
    }
}