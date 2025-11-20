using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.Services;

public class GymClassService : IGymClassService
{
    private readonly IRepository<GymClass> _gymClassRepo;
    public GymClassService(IRepository<GymClass> gymClassRepo)
    {
        _gymClassRepo = gymClassRepo;
    }

    public async Task<Result<GymClassInfoResponse>> CreateAsync(GymClassAddRequest entity, CancellationToken cancellationToken)
    {
       
       GymClass gymClass = await _gymClassRepo.CreateAsync(entity.ToGymClass(),cancellationToken);
        return Result<GymClassInfoResponse>.Success(gymClass.ToGymInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<GymClassResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<GymClass> gymCLasses = await _gymClassRepo.GetAllAsync(cancellationToken);
        return Result<IEnumerable<GymClassResponse>>.Success(gymCLasses.Select(item => item.ToGymResponse()),StatusCodeEnum.Ok);
    }

    public Task<Result<GymClassDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<GymClassInfoResponse>> UpdateAsync(Guid id, GymClassUpdateRequest entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
