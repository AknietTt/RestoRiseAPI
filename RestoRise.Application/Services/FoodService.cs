using RestoRise.Application.Interfaces.Repositories;
using RestoRise.Application.Interfaces.Services;

namespace RestoRise.BuisnessLogic.Services;

public class FoodService: IFoodService
{
    private readonly IUnitOfWork _unitOfWork;

    public FoodService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
}