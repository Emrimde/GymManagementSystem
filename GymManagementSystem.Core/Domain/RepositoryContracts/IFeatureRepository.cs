using GymManagementSystem.Core.DTO.Feature;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IFeatureRepository
{
    Task<IEnumerable<FeatureResponse>> GetAllFeatures();
}
