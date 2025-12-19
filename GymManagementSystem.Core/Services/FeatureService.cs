using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
namespace GymManagementSystem.Core.Services;
public class FeatureService : IFeatureService
{
    private readonly IFeatureRepository _featureRepository;
    public FeatureService(IFeatureRepository featureRepository)
    {
        _featureRepository = featureRepository;
    }

    public async Task<Result<IEnumerable<FeatureResponse>>> GetFeaturesForSelect()
    {
        IEnumerable<FeatureResponse> dto = await _featureRepository.GetAllFeatures();
        return Result<IEnumerable<FeatureResponse>>.Success(dto,StatusCodeEnum.Ok);
    }
}
