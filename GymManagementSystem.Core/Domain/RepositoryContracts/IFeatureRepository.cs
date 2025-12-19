using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Feature;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IFeatureRepository
{
    void AddFeature(Feature feature);
    Task<IEnumerable<FeatureResponse>> GetAllFeatures();
}
