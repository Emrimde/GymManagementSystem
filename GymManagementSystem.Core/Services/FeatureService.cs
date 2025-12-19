using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
namespace GymManagementSystem.Core.Services;
public class FeatureService : IFeatureService
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IUnitOfWork _unitOfWork;
    public FeatureService(IFeatureRepository featureRepository, IUnitOfWork unitOfWork)
    {
        _featureRepository = featureRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> CreateFeatureAsync(FeatureAddRequest featureAddRequest)
    {
        Feature feature = featureAddRequest.ToFeature();    
        _featureRepository.AddFeature(feature);
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<FeatureResponse>>> GetAllFeaturesAsync()
    {
        IEnumerable<FeatureResponse> dto = await _featureRepository.GetAllFeatures();
        return Result<IEnumerable<FeatureResponse>>.Success(dto,StatusCodeEnum.Ok);
    }
}
