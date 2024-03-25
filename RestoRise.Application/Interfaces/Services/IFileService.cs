using RestoRise.Domain.Common;


namespace RestoRise.Application.Interfaces.Services;

public interface IFileService
{
    Task<Result<string>> AddImage(string image);
}